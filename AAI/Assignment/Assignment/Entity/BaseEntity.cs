﻿using Assignment.Movement;
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
		protected List<BaseSteering> SteeringBehaviours = new List<BaseSteering>();
		public EntityType Type { get; protected set; }

		public double Direction;
		public double Speed;
		public double MaxSpeed;
		public double MaxForce;
		public double DirectionMaxChange = 0.3;

		public Location Location;

		public BaseEntity()
		{
			Location = new Location(10, 20);
			Direction = Math.PI * 0.5;
			Speed = 1;
			MaxSpeed = 6;
			MaxForce = 100;
		}

		public abstract void Update(int tick);

		public abstract void Render(Graphics g);

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

			force.Amount = Math.Min(force.Amount, MaxForce);

			ApplySteeringForce(force);
		}

		private void ApplySteeringForce(SteeringForce force)
		{
			/*
			float x = (float) (Math.Cos(force.Direction) * force.Amount + Location.X);
			float y = (float) (Math.Sin(force.Direction) * force.Amount + Location.Y);
			g.DrawLine(Pens.DarkGreen, (float) Location.X, (float) Location.Y, x, y);
			*/

			UpdateDirection(force);

			// todo nmn
			Speed = Math.Max(Speed - 0.3, 0);
			Speed += force.Amount * 0.1;// inertia
			Speed = Math.Min(Speed, MaxSpeed);

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

		private void UpdateDirection(SteeringForce force)
		{
			while (Math.Abs(force.Direction - (Direction + Math.PI * 2)) < Math.Abs(force.Direction - Direction))
				Direction += Math.PI * 2;

			while (Math.Abs(force.Direction - (Direction - Math.PI * 2)) < Math.Abs(force.Direction - Direction))
				Direction -= Math.PI * 2;

			Direction += Math.Min(Math.Max(force.Direction - Direction, -DirectionMaxChange), DirectionMaxChange);

		}

		private SteeringForce CalculateSteeringForceDithering()
		{
			SteeringForce force = new SteeringForce();

			foreach (var behavior in SteeringBehaviours)
			{
				if (GameWorld.Instance.Random.NextDouble() < behavior.Priority)
				{
					force += behavior.Calculate(this);
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
				}
			}

			return force;
		}

		private SteeringForce CalculateSteeringForceTruncatedSum()
		{
			SteeringForce force = new SteeringForce();

			foreach (var behavior in SteeringBehaviours)
			{
				force += behavior.Calculate(this);
			}

			return force;
		}
	}
}
