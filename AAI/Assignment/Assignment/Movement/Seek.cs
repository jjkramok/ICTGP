using Assignment.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Movement
{
	class Seek : BaseSteering
	{
		public BaseEntity SeekEntity;
		public double offset;

		public override SteeringForce Calculate()
		{
			throw new NotImplementedException();
		}
	}
}
