using Assignment.Entity;
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
		public double radius;

		public override SteeringForce Calculate()
		{
			throw new NotImplementedException();
		}
	}
}
