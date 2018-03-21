using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Fuzzy
{
	/*      MinHigh    MaxHigh
	 * |     -------------
	 * |    /             \
	 * |   /               \
	 * |  /                 \
	 * +-----------------------
	 *  MinLow             MaxLow
	 **/

	public class GraphSection
	{
		public string Name;
		public double MinLow;
		public double MinHigh;
		public double MaxLow;
		public double MaxHigh;

		public GraphSectionType Type;

		public GraphSection(string name, GraphSectionType type)
		{
			Name = name;
			Type = type;
		}

		public double ValueForLocation(double value)
		{
			switch (Type)
			{
				case GraphSectionType.LeftShoulder:
					if (value > MaxLow)
						return 0;
					if (value <= MaxHigh)
						return 1;
					return (value - MaxHigh) / (MaxLow - MaxHigh);
				case GraphSectionType.RightShoulder:
					if (value < MinLow)
						return 0;
					if (value >= MinHigh)
						return 1;
					return (value - MaxHigh) / (MinLow - MinHigh);
				case GraphSectionType.Center:
					if (value > MaxLow || value < MinLow)
						return 0;
					if (value >= MinHigh && value <= MaxHigh)
						return 1;
					if(value < MinHigh)
						return (value - MaxHigh) / (MinLow - MinHigh);
					if(value > MaxHigh)
						return (value - MaxHigh) / (MaxLow - MaxHigh);
					break;
			}
			Console.WriteLine($"GraphSection could not return value {Name}, should not happen");
			return 0;
		}

		public enum GraphSectionType
		{
			LeftShoulder,
			RightShoulder,
			Center
		}
	}
}
