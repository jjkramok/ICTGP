using Assignment.World;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Obstacle
{
	public abstract class BaseObstacle : BaseObject
	{
		public List<ObstacleCircle> CollisionCircles;

		public BaseObstacle(Location location, double rotation)
		{
			Location = location;
			Direction = rotation;

			CollisionCircles = new List<ObstacleCircle>();
		}
		public abstract void Render(Graphics g);
	}
}
