using Assignment.Movement;
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
	public abstract class BaseEntity
	{
		// should always be ordered by prioity.
		protected List<BaseSteering> SteeringBehaviours;

		public EntityType Type { get; protected set; }

		// Current values.
		public double Direction;
		public double Speed;
		public Location Location;

		// Default settings.
		public readonly double MaxSpeedDefault;
		public double MaxSpeed;
		public double MaxForce;
		public double DirectionMaxChange = 1;

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
			MaxSpeedDefault = 6;
			MaxSpeed = MaxSpeedDefault;

			MaxForce = 100;

			// todo fix default state
			PreviousState = "";
			State = "patrol";
		}

		public abstract void Update(int tick);

		public abstract void Render(Graphics g);

		public void AddBehaviour(BaseSteering behaviour)
		{
			SteeringBehaviours.Add(behaviour);
			SteeringBehaviours = SteeringBehaviours.OrderBy(x => x.Priority).ToList();
		}

		public void RemoveAllBehaviours()
		{
			SteeringBehaviours.Clear();
		}

		public BaseSteering GetBehaviourByType(BaseSteering behaviour)
		{
			foreach(var item in SteeringBehaviours)
			{
				if(item.GetType().Name == behaviour.GetType().Name)
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
				behaviour.Render(g, this);
				info += $" {behaviour.GetType().Name}\n";
			}

			g.DrawString(info, new Font(FontFamily.GenericSansSerif, 10), Brushes.Black, (int) Location.X + 5, (int) Location.Y + 5);
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
			/*
			float x = (float) (Math.Cos(force.Direction) * force.Amount + Location.X);
			float y = (float) (Math.Sin(force.Direction) * force.Amount + Location.Y);
			g.DrawLine(Pens.DarkGreen, (float) Location.X, (float) Location.Y, x, y);
			*/

			force = UpdateDirection(force);

			// todo nmn
			Speed = Utility.BoundValueMin(Speed - 0.3, 0);
			Speed += force.Amount * 0.1;// inertia
			Speed = Utility.BoundValueMax(Speed, MaxSpeed);

			Location.X += Math.Cos(Direction) * Speed;
			Location.Y += Math.Sin(Direction) * Speed;

			FixEntityOnEdge();
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
				Direction += Math.PI * 2;

			while (Math.Abs(force.Direction - (Direction - Math.PI * 2)) < Math.Abs(force.Direction - Direction))
				Direction -= Math.PI * 2;

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
