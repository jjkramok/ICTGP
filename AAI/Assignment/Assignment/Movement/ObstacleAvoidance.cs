using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Entity;
using Assignment.World;
using Assignment.Obstacle;

namespace Assignment.Movement
{
	class ObstacleAvoidance : BaseSteering
	{
		private readonly double offsetMargin = 10;

		public override SteeringForce Calculate(BaseEntity entity)
		{
			var force = new SteeringForce();

			var obstacles = GameWorld.Instance.ObstaclesInArea(entity.Location, 40);

			foreach (var obstacle in obstacles)
			{
				var distance = Utilities.Utilities.Distance(obstacle.Location, entity.Location);
				var angle = Utilities.Utilities.Direction(entity.Location, obstacle.Location);

				var angleDiff = angle - entity.Direction;

				var offset = Math.Asin(Math.Abs(angleDiff)) * distance;

				if (offset < offsetMargin + obstacle.Radius)
				{
					force += new SteeringForce(entity.Direction + angleDiff, 100 / distance);
				}
			}

			return force;
		}
	}
}
