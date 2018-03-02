using Assignment.Entity;
using Assignment.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Movement
{
	class Flee : BaseSteering
	{
		public EntityType FleeFrom;
		public double Radius;
		public double Multiplier = 1000;

		public Flee() : base()
		{
			Priority = 0.8;
		}

		public override SteeringForce Calculate(BaseEntity entity)
		{
			var fleeFromEntities = GameWorld.Instance.EntitiesInArea(entity.Location, Radius);
			SteeringForce force = new SteeringForce();
			int forcesCount = 0;
			foreach (var fleeFromEntity in fleeFromEntities)
			{
				if (fleeFromEntity.Type == FleeFrom)
				{
					var direction = Utilities.Utilities.Direction(entity.Location, fleeFromEntity.Location) - Math.PI;
					var distance = Utilities.Utilities.Distance(entity.Location, fleeFromEntity.Location);
					force += new SteeringForce(direction, (1 / distance) * Multiplier);

					forcesCount++;
				}
			}
			if (forcesCount == 0)
				return force;


			force = force / forcesCount;
			return force;
		}
	}
}
