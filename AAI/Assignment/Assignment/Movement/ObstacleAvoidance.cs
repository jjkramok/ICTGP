using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Entity;
using Assignment.World;
using Assignment.Obstacle;
using System.Drawing;
using Assignment.Utilities;

namespace Assignment.Movement
{
	class ObstacleAvoidance : BaseSteering
	{
		public double offsetMargin = 10;
		public double avoidanceFactor = 100;
		public double searchArea = 50;

		private List<ObstacleCircle> avoidedObstacles;

		public ObstacleAvoidance() : base()
		{
			Priority = 1;
		}

		public override SteeringForce Calculate(BaseEntity entity)
		{
			avoidedObstacles = new List<ObstacleCircle>();

			var force = new SteeringForce();

			var obstacles = GameWorld.Instance.ObstaclesInArea(entity.Location, searchArea);

			double totalForce = 0;

			int forceCounterL = 0;
			int forceCounterR = 0;

			foreach (var obstacle in obstacles)
			{
				var distance = Utility.Distance(obstacle.Location, entity.Location);
				var angle = Utility.Direction(entity.Location, obstacle.Location);

				var angleDiff = angle - entity.Direction;

				var offset = Math.Sin(angleDiff) * distance;

				while (angleDiff > Math.PI)
					angleDiff -= Math.PI * 2;

				while (angleDiff < -Math.PI)
					angleDiff += Math.PI * 2;

				// ignore obstacles behind the entity
				if (Math.Abs(angleDiff) < Math.PI / 2)
				{
					if (Math.Abs(offset) < offsetMargin + obstacle.Radius)
					{
						avoidedObstacles.Add(obstacle);

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
				force = new SteeringForce(forceCounterL > forceCounterR ? entity.Direction + Math.PI / 2 : entity.Direction - Math.PI / 2, totalForce);
			}

			return force;
		}

		public override void Render(Graphics g, BaseEntity entity)
		{
			foreach(var obstacle in avoidedObstacles)
			{
				g.DrawLine(Pens.DarkRed, (int) entity.Location.X, (int) entity.Location.Y, (int) obstacle.Location.X, (int) obstacle.Location.Y);
			}
		}
	}
}
