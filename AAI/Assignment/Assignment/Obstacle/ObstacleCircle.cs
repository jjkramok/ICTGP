using Assignment.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Obstacle
{
	public class ObstacleCircle
	{
		public Location Location;
		public double Radius;

		public ObstacleCircle(Location location, double radius)
		{
			Location = location;
			Radius = radius;
		}
	}
}
