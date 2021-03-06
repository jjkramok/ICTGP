﻿using Assignment.Movement;
using Assignment.State;
using Assignment.Utilities;
using Assignment.World;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Entity
{
	public abstract class BaseEntity : BaseObject
	{
		// should always be ordered by prioity.
		protected List<BaseSteering> SteeringBehaviours;

		public EntityType Type { get; protected set; }

		// Current values.
		public double Speed;

		// Default settings.
		public double MaxSpeed;
		public double MaxForce;
		public double DirectionMaxChange = 1;
		public double SlowDownSpeed = 0.3;

		// State behaviour variables.
		public string PreviousState;
		public string State;

		// Fuzzy logic variables.
		public double Food = 100;
		public double QuickEnergy = 100;
		public double SlowEnergy = 200;



		public BaseEntity()
		{
			SteeringBehaviours = new List<BaseSteering>();
			Location = new Location(10, 20);
			Direction = Math.PI * 0.5;
			Speed = 1;
			MaxSpeed = 6;

			MaxForce = 100;

			PreviousState = "";
		}

		public void Update(long tick)
		{
			StateMachine.Execute(this);
			CalculateSteeringForce();
		}

		public virtual bool Render(Graphics g)
        {
            int size = 10;
            Image sprite = ImageManager.Instance.GetImage(GetType().Name, Direction);
            if (sprite != null)
            {
                g.DrawImage(sprite, (int)Location.X - (size / 2), (int)Location.Y - (size / 2), size, size);
                return true;
            }
            return false;
        }

		public void AddBehaviour(BaseSteering behaviour)
		{
			SteeringBehaviours.Add(behaviour);
			SteeringBehaviours = SteeringBehaviours.OrderBy(x => x.Priority).ToList();
		}

		public void RemoveAllBehaviours()
		{
			SteeringBehaviours.Clear();
		}

		public BaseSteering GetBehaviourByType(string behaviour)
		{
			foreach (var item in SteeringBehaviours)
			{
				if (item.GetType().Name == behaviour)
				{
					return item;
				}
			}

			return null;
		}

		public void RenderDebug(Graphics g)
		{
			string info = $"State: {State} \n";

			foreach (var behaviour in SteeringBehaviours)
			{
				info += $" {behaviour.GetType().Name}\n";
			}
			info += $"QuickEnergy:\t{QuickEnergy}\n";
			info += $"SlowEnergy:\t{SlowEnergy}\n";
			info += $"Food:\t\t{Food}\n";


			g.DrawString(info, new Font(FontFamily.GenericSansSerif, 10), Brushes.Black, (int) Location.X + 5, (int) Location.Y + 5);
		}

		public void RenderSteering(Graphics g)
		{
			foreach (var behaviour in SteeringBehaviours)
			{
				behaviour.Render(g, this);
			}
		}

		protected void CalculateSteeringForce()
		{
			SteeringForce force = null;
			switch (GameWorld.Instance.SteeringForceCalculationType)
			{
				case SteeringForceCalculationType.Dithering:
					force = CalculateSteeringForceDithering();
					break;
				case SteeringForceCalculationType.Priorization:
					force = CalculateSteeringForcePriorization();
					break;
				case SteeringForceCalculationType.WeightedTruncatedSum:
					force = CalculateSteeringForceTruncatedSum();
					break;
				default:
					throw new Exception("Current SteeringForceCalculationType is invalid.");
			}

			force.Amount = Utility.BoundValueMax(force.Amount, MaxForce);

			ApplySteeringForce(force);
		}

		private void ApplySteeringForce(SteeringForce force)
		{
			force = UpdateDirection(force);
			QuickEnergy = Utility.BoundValue(QuickEnergy, 0.05, 1);

			Speed = Utility.BoundValueMin(Speed - SlowDownSpeed, 0);
			Speed += force.Amount * 0.1;// inertia
			Speed = Utility.BoundValueMax(Speed, MaxSpeed * QuickEnergy);

			Location.X += Math.Cos(Direction) * Speed;
			Location.Y += Math.Sin(Direction) * Speed;

			FixEntityOnEdge();
		}

		public void RemoveBehaviour(Type type)
		{
			for(int i = SteeringBehaviours.Count -1; i>=0; i--)
			{
				if(SteeringBehaviours[i].GetType().Name == type.Name)
				{
                    SteeringBehaviours[i].Dispose();
					SteeringBehaviours.RemoveAt(i);
				}
			}
		}

		private void FixEntityOnEdge()
		{
			if (Location.X < 2)
			{
				Location.X = 20;
				Direction = 0;
			}
			if (Location.Y < 2)
			{
				Location.Y = 20;
				Direction = Math.PI * 0.5;
			}
			if (Location.X > GameWorld.Instance.Width - 2)
			{
				Location.X = GameWorld.Instance.Width - 20;
				Direction = Math.PI;
			}
			if (Location.Y > GameWorld.Instance.Height - 2)
			{
				Location.Y = GameWorld.Instance.Height - 20;
				Direction = Math.PI * 1.5;
			}
		}

		private SteeringForce UpdateDirection(SteeringForce force)
		{
			if (force.Amount == 0)
			{
				return force;
			}

			while (Math.Abs(force.Direction - (Direction + Math.PI * 2)) < Math.Abs(force.Direction - Direction))
				force.Direction -= Math.PI * 2;

			while (Math.Abs(force.Direction - (Direction - Math.PI * 2)) < Math.Abs(force.Direction - Direction))
				force.Direction += Math.PI * 2;

			var directionChangeMax = DirectionMaxChange * 1 / Speed;
			var directionDiff = force.Direction - Direction;
			Direction += Utility.BoundValue(directionDiff, -directionChangeMax, directionChangeMax);

			force.Amount = force.Amount * (1 - directionDiff / Math.PI);

			return force;
		}

		private SteeringForce CalculateSteeringForceDithering()
		{
			SteeringForce force = new SteeringForce();

			for (int i = 0; i < SteeringBehaviours.Count; i++)
			{
				if (GameWorld.Instance.Random.NextDouble() < SteeringBehaviours[i].Priority)
				{
					force += SteeringBehaviours[i].Calculate(this);

					if (SteeringBehaviours[i].BehaviorDone)
					{
						SteeringBehaviours.RemoveAt(i);
						i--;
					}
				}
			}

			return force;
		}

		private SteeringForce CalculateSteeringForcePriorization()
		{
			int behaviorsCalculationCount = 3;
			SteeringForce force = new SteeringForce();

			for (int i = 0; i < behaviorsCalculationCount && i < SteeringBehaviours.Count; i++)
			{
				force += SteeringBehaviours[i].Calculate(this);
				if (SteeringBehaviours[i].BehaviorDone)
				{
					SteeringBehaviours.RemoveAt(i);
					i--;
				}
			}

			return force;
		}

		private SteeringForce CalculateSteeringForceTruncatedSum()
		{
			SteeringForce force = new SteeringForce();

			for (int i = 0; i < SteeringBehaviours.Count; i++)
			{
				force += SteeringBehaviours[i].Calculate(this);
				if (SteeringBehaviours[i].BehaviorDone)
				{
					SteeringBehaviours.RemoveAt(i);
					i--;
				}
			}

			return force;
		}
	}
}
