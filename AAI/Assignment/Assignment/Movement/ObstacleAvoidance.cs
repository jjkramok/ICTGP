using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Entity;
using Assignment.World;
using Assignment.Obstacle;
using System.Drawing;

namespace Assignment.Movement
{
	class ObstacleAvoidance : BaseSteering
	{
		public double offsetMargin = 10;
		public double avoidanceFactor = 100;

		public override SteeringForce Calculate(BaseEntity entity)
		{
			var force = new SteeringForce();

			var obstacles = GameWorld.Instance.ObstaclesInArea(entity.Location, 50);

			double totalForce = 0;

			int forceCounterL = 0;
			int forceCounterR = 0;

			foreach (var obstacle in obstacles)
			{
				var distance = Utilities.Utilities.Distance(obstacle.Location, entity.Location);
				var angle = Utilities.Utilities.Direction(entity.Location, obstacle.Location);

				var angleDiff = angle - entity.Direction;

				g.DrawLine(Pens.Yellow, (float) entity.Location.X, (float) entity.Location.Y, (float) obstacle.Location.X, (float) obstacle.Location.Y);

				var offset = Math.Sin(angleDiff) * distance;

				//g.DrawString($"angle: {Math.Round(angle * 180 / Math.PI)}\nangledif: {Math.Round(angleDiff * 180 / Math.PI)}", new Font(FontFamily.GenericSerif, 10), Brushes.Yellow, (float) entity.Location.X, (float) entity.Location.Y);

				while (angleDiff > Math.PI)
					angleDiff -= Math.PI * 2;

				while (angleDiff < -Math.PI)
					angleDiff += Math.PI * 2;

				// ignore obstacles behind the entity
				if (Math.Abs(angleDiff) < Math.PI / 2)
				{
					if (Math.Abs(offset) < offsetMargin + obstacle.Radius)
					{
						g.FillEllipse(Brushes.Red, (float) obstacle.Location.X - 10, (float) obstacle.Location.Y - 10, 20, 20);

						var amountToSteer = obstacle.Radius - Math.Abs(offset) + offsetMargin;
						var steeringNeed = distance;

						if (offset < 0)
						{
							force += new SteeringForce(entity.Direction + Math.PI / 2, avoidanceFactor / steeringNeed * amountToSteer);
							forceCounterL++;
						}
						else
						{
							force += new SteeringForce(entity.Direction - Math.PI / 2, avoidanceFactor / steeringNeed * amountToSteer);
							forceCounterR++;
						}
						totalForce += force.Amount;
					}
				}
			}

			if (forceCounterL + forceCounterR > 1)
			{
				return new SteeringForce(entity.Direction + Math.PI, totalForce);
			}
			return force;
		}
	}
}
