using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Entity;
using Assignment.World;
using System.Drawing;
using Assignment.Utilities;

namespace Assignment.Movement
{
	public class Arrive : BaseSteering
	{
		public Location ArriveLocation;
		public double DistanceDone = 10;
		public double MaxSpeed = 2;
		public double Force = 50;
		public double StopDistance = 5;

		public Arrive() : base()
		{
			Priority = 0.7;
		}

		public override SteeringForce Calculate(BaseEntity entity)
		{
			if(ArriveLocation == null)
			{
				BehaviorDone = true;
				return new SteeringForce();
			}

			var distance = Utility.Distance(entity.Location, ArriveLocation);
			var direction = Utility.Direction(entity.Location, ArriveLocation);

			if (distance < DistanceDone && entity.Speed < MaxSpeed)
			{
				BehaviorDone = true;
				return new SteeringForce();
			}
			if (distance < StopDistance * entity.Speed / entity.SlowDownSpeed)
			{
				return new SteeringForce();
			}

			return new SteeringForce(direction, Math.Min(distance, Force));
		}

		public override void Render(Graphics g, BaseEntity entity)
		{
			g.DrawLine(Pens.Red, (int) ArriveLocation.X, (int) ArriveLocation.Y, (int) entity.Location.X, (int) entity.Location.Y);
		}
	}
}
