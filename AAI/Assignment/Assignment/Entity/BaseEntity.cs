using Assignment.Movement;
using Assignment.World;
using System;
using System.Collections.Generic;
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

		public Location Location;

		public BaseEntity()
		{
			Location = new Location(10, 20);
			Direction = Math.PI * 0.5;
			Speed = 1;
			MaxSpeed = 5;
			MaxForce = 4;
		}

		public abstract void Update(int tick);

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

			if (force.Amount > MaxForce)
			{
				force.Amount = MaxForce;
			}

			ApplySteeringForce(force);
		}

		private void ApplySteeringForce(SteeringForce force)
		{
			Direction = force.Direction;
			// todo nmn
			Speed += force.Amount * 0.1;// inertia


			Location.X = (Location.X + (Math.Cos(Direction) * Speed));
			Location.Y = (Location.Y + (Math.Sin(Direction) * Speed));
		}

		private SteeringForce CalculateSteeringForceDithering()
		{
			SteeringForce force = new SteeringForce();

			foreach(var behavior in SteeringBehaviours)
			{
				if(GameWorld.Instance.Random.NextDouble() > behavior.Priority)
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

			for(int i = 0; i < behaviorsCalculationCount && i < SteeringBehaviours.Count; i++)
			{
				force += SteeringBehaviours[i].Calculate(this);
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
