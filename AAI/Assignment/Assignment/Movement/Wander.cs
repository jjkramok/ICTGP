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
	public class Wander : BaseSteering
	{
		private double wanderDirection = Math.PI * 2;
		public double DirectionChangeMax = 0.8;
		public double CircleSize = 50;
		public double CircleOffset = 25;

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

			double force = Utility.Distance(new Location(circleDotX, circleDotY), entity.Location);

			double direction = Math.Atan((circleDotY - entity.Location.Y) / (circleDotX - entity.Location.X));
			direction = circleDotX < entity.Location.X ? direction + Math.PI : direction;

			return new SteeringForce(direction, force);
		}

		public override void Render(Graphics g, BaseEntity entity)
		{
			double circleX = Math.Cos(entity.Direction) * CircleOffset + entity.Location.X;
			double circleY = Math.Sin(entity.Direction) * CircleOffset + entity.Location.Y;

			double circleDotX = Math.Cos(wanderDirection) * CircleSize + circleX;
			double circleDotY = Math.Sin(wanderDirection) * CircleSize + circleY;

			g.DrawEllipse(Pens.Brown, (float) circleX - (float) CircleSize / 2, (float) circleY - (float) CircleSize / 2, (float) CircleSize, (float) CircleSize);

			g.FillEllipse(Brushes.BlueViolet, (float) circleDotX - 10, (float) circleDotY - 10, 20, 20);

			g.DrawLine(Pens.Red, (float) circleX, (float) circleY, (float) circleDotX, (float) circleDotY);
		}
	}
}
