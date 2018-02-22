using Assignment.World;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Obstacle
{
	public abstract class BaseObstacle
	{
		public Location Location;
		public double Rotation;
		public List<ObstacleCircle> CollisionCircles;

		public BaseObstacle(Location location, double rotation)
		{
			Location = location;
			Rotation = rotation;

			CollisionCircles = new List<ObstacleCircle>();
		}

		public abstract void Render(Graphics g);
	}
}
