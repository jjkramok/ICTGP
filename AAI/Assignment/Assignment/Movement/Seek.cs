using Assignment.Entity;
using Assignment.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Movement
{
	class Seek : BaseSteering
	{
		public BaseEntity ChaseEntity;
		public double MaxDistance;

		public override SteeringForce Calculate(BaseEntity entity)
		{
			var distance = Utilities.Utilities.Distance(entity.Location, ChaseEntity.Location);
			if (distance > MaxDistance)
			{
				return new SteeringForce();
			}

			var direction = Utilities.Utilities.Direction(entity.Location, ChaseEntity.Location);

			// todo nmn
			return new SteeringForce(direction, 5);
		}
	}
}
