using Assignment.Movement;
using Assignment.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Entity
{
	class BaseEntity
	{
		protected List<BaseSteering> SteeringBehaviours = new List<BaseSteering>();
		public EntityType Type { get; }

		public double Direction;
		public double Speed;
		public double MaxSpeed;

		public Location Location;

		public BaseEntity()
		{
			Location = new Location(10, 20);
			Direction = Math.PI * 0.5;
			Speed = 1;
			MaxSpeed = 5;
		}

		public void Update(int tick) { }

		public void CalculateSteeringForce()
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

			ApplySteeringForce(force);
		}

		private void ApplySteeringForce(SteeringForce force)
		{

		}

		private SteeringForce CalculateSteeringForceDithering()
		{
			return new SteeringForce();
		}

		private SteeringForce CalculateSteeringForcePriorization()
		{
			return new SteeringForce();
		}

		private SteeringForce CalculateSteeringForceTruncatedSum()
		{
			return new SteeringForce();
		}
	}
}
