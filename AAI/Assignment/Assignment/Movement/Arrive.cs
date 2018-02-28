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

		public override SteeringForce Calculate(BaseEntity entity)
		{
			var distance = Utilities.Utilities.Distance(entity.Location, ArriveLocation);
			var direction = Utilities.Utilities.Direction(entity.Location, ArriveLocation);

			g.FillEllipse(Brushes.Red, (float) ArriveLocation.X - 5, (float) ArriveLocation.Y - 5, 10, 10);
			g.DrawString($"{Math.Round(direction * 180 / Math.PI)}", new Font(FontFamily.GenericSansSerif, 10), Brushes.Red, (float) entity.Location.X, (float) entity.Location.Y);

			if (distance < 5)
			{
				BehaviorDone = true;
				return new SteeringForce();
			}

			// todo nmn
			return new SteeringForce(direction, Math.Min(distance, 10));
		}
	}
}
