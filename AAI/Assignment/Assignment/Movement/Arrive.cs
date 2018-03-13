using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Entity;
using Assignment.World;
using System.Drawing;

namespace Assignment.Movement
{
	public class Arrive : BaseSteering
	{
		public Location ArriveLocation;

		public Arrive() : base()
		{
			Priority = 0.7;
		}

		public override SteeringForce Calculate(BaseEntity entity)
		{
			var distance = Utilities.Utilities.Distance(entity.Location, ArriveLocation);
			var direction = Utilities.Utilities.Direction(entity.Location, ArriveLocation);

			if (distance < 25)
			{
				BehaviorDone = true;
				return new SteeringForce();
			}

			// todo nmn
			return new SteeringForce(direction, Math.Min(distance, 10));
		}

		public override void Render(Graphics g, BaseEntity entity)
		{
			g.DrawLine(Pens.Red, (int) ArriveLocation.X, (int) ArriveLocation.Y, (int) entity.Location.X, (int) entity.Location.Y);
		}
	}
}
