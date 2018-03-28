using Assignment.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Assignment.Obstacle
{
	public class ObstacleCircle : BaseObject
	{
		public double Radius;

		public ObstacleCircle(Location location, double radius)
		{
			Location = location;
			Radius = radius;
		}
	}
}
