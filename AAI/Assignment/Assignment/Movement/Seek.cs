using Assignment.Entity;
using Assignment.Utilities;
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

		public Seek() : base()
		{
			Priority = 0.4;
		}

		public override SteeringForce Calculate(BaseEntity entity)
		{
			var distance = Utility.Distance(entity.Location, ChaseEntity.Location);
			if (distance > MaxDistance)
			{
				return new SteeringForce();
			}

			var direction = Utility.Direction(entity.Location, ChaseEntity.Location);

			// todo nmn
			return new SteeringForce(direction, 5);
		}
	}
}
