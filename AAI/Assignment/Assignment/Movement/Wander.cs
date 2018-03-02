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
	public class Wander : BaseSteering
	{
		private double wanderDirection = Math.PI * 2;
		public double DirectionChangeMax = 0.8;
		public double CircleSize = 5;
		public double CircleOffset = 10;

		public Wander() : base()
		{
			Priority = 0.3;
		}

		public override SteeringForce Calculate(BaseEntity entity)
		{
			wanderDirection += (GameWorld.Instance.Random.NextDouble() - 0.5) * DirectionChangeMax;

			double circleX = Math.Cos(entity.Direction) * CircleOffset + entity.Location.X;
			double circleY = Math.Sin(entity.Direction) * CircleOffset + entity.Location.Y;
			
			double circleDotX = Math.Cos(wanderDirection) * CircleSize + circleX;
			double circleDotY = Math.Sin(wanderDirection) * CircleSize + circleY;

			double force = Utilities.Utilities.Distance(new Location(circleDotX, circleDotY), entity.Location);

			double direction = Math.Atan((circleDotY - entity.Location.Y) / (circleDotX - entity.Location.X));
			direction = circleDotX < entity.Location.X ? direction + Math.PI : direction;

			/*
			if (Render)
			{
				g.DrawEllipse(Pens.Brown, (float) circleX - (float) CircleSize / 2, (float) circleY - (float) CircleSize / 2, (float) CircleSize, (float) CircleSize);

				g.FillEllipse(Brushes.BlueViolet, (float) circleDotX - 4, (float) circleDotY - 4, 8, 8);

				g.DrawLine(Pens.Black, (float) circleX, (float) circleY, (float) circleDotX, (float) circleDotY);
			}
			*/
			return new SteeringForce(direction, force);
		}
	}
}
