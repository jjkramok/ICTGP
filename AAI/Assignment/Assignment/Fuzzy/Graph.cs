using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Fuzzy
{
	public class Graph
	{
		public string Name;
		public double MinValue;
		public double MaxValue;

		public List<GraphSection> Sections;

		public Graph(string name)
		{
			Name = name;
		}
	}
}
