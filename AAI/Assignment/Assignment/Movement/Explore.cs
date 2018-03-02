using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Entity;

namespace Assignment.Movement
{
	class Explore : BaseSteering
	{
		public Explore() : base()
		{
			Priority = 0.5;
		}
		public override SteeringForce Calculate(BaseEntity entity)
		{
			throw new NotImplementedException();
		}
	}
}
