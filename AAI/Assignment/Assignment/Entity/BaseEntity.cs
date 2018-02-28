using Assignment.Movement;
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
		public EntityType Type { get; }

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

		protected void CalculateSteeringForce(Graphics g)
		{
			SteeringForce force = null;
			switch (GameWorld.Instance.SteeringForceCalculationType)
			{
				case SteeringForceCalculationType.Dithering:
					force = CalculateSteeringForceDithering(g);
					break;
				case SteeringForceCalculationType.Priorization:
					force = CalculateSteeringForcePriorization(g);
					break;
				case SteeringForceCalculationType.WeightedTruncatedSum:
					force = CalculateSteeringForceTruncatedSum(g);
					break;
				default:
					throw new Exception("Current SteeringForceCalculationType is invalid.");
			}

			force.Amount = Math.Min(force.Amount, MaxForce);

			ApplySteeringForce(force, g);
		}

		private void ApplySteeringForce(SteeringForce force, Graphics g)
		{

			float x = (float) (Math.Cos(force.Direction) * force.Amount + Location.X);
			float y = (float) (Math.Sin(force.Direction) * force.Amount + Location.Y);
			//g.DrawLine(Pens.DarkGreen, (float) Location.X, (float) Location.Y, x, y);

			Direction += Math.Min(Math.Max(force.Direction - Direction, -DirectionMaxChange), DirectionMaxChange);
			// todo nmn
			Speed = Math.Max(Speed - 0.3, 0);
			Speed += force.Amount * 0.1;// inertia
			Speed = Math.Min(Speed, MaxSpeed);

			Location.X = (Location.X + (Math.Cos(Direction) * Speed));
			Location.Y = (Location.Y + (Math.Sin(Direction) * Speed));

			Location.X = Math.Max(Math.Min(Location.X, GameWorld.Instance.Width - 0.001), 0);
			Location.Y = Math.Max(Math.Min(Location.Y, GameWorld.Instance.Height - 0.001), 0);

		}

		private SteeringForce CalculateSteeringForceDithering(Graphics g)
		{
			SteeringForce force = new SteeringForce();

			foreach (var behavior in SteeringBehaviours)
			{
				if (GameWorld.Instance.Random.NextDouble() > behavior.Priority)
				{
					force += behavior.Calculate(this);
				}
			}

			return force;
		}

		private SteeringForce CalculateSteeringForcePriorization(Graphics g)
		{
			int behaviorsCalculationCount = 2;
			SteeringForce force = new SteeringForce();

			for (int i = 0; i < behaviorsCalculationCount && i < SteeringBehaviours.Count; i++)
			{
				SteeringBehaviours[i].g = g;

				force += SteeringBehaviours[i].Calculate(this);
				if (SteeringBehaviours[i].BehaviorDone)
				{
					SteeringBehaviours.RemoveAt(i);
				}
			}

			return force;
		}

		private SteeringForce CalculateSteeringForceTruncatedSum(Graphics g)
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
