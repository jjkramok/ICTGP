using Assignment.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Movement
{
	public abstract class BaseSteering
	{
		public double Priority;
		public bool BehaviorDone = false;

		public BaseSteering()
		{

		}

		public abstract SteeringForce Calculate(BaseEntity entity);

		public abstract void Render(Graphics g, BaseEntity entity);
	}
}
