using Assignment.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Utilities
{
	static class Utility
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
			if(to.X > from.X)
				return Math.Atan((from.Y - to.Y) / (from.X - to.X));
			else
				return Math.Atan((from.Y - to.Y) / (from.X - to.X)) + Math.PI;
		}

		public static double BoundValue(double value, double min, double max)
		{
			return Math.Min(Math.Max(value, min), max);
		}

		public static double BoundValueMin(double value, double min)
		{
			return Math.Max(value, min);
		}

		public static double BoundValueMax(double value, double max)
		{
			return Math.Min(value, max);
		}
	}
}
