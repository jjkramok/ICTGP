using Assignment.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
	static class Utilities
	{
		public static double Distance(Location location1, Location location2)
		{
			return Math.Sqrt(
					(location1.X - location2.X) * (location1.X - location2.X) +
					(location1.Y - location2.Y) * (location1.Y - location2.Y)
				);
		}

		public static double Direction(Location from, Location to)
		{
			return Math.Tan((from.Y - to.Y) / (from.X - to.X));
		}
	}
}
