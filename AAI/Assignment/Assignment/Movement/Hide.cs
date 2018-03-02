using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Entity;

namespace Assignment.Movement
{
	class Hide : BaseSteering
	{
		public Hide():base()
		{
			Priority = 0.6;
		}

		public override SteeringForce Calculate(BaseEntity entity)
		{
			throw new NotImplementedException();
		}
	}
}
