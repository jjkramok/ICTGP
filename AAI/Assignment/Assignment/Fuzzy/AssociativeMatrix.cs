using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Fuzzy
{
	public class AssociativeMatrix
	{
		public string GraphName;
		public Dictionary<string, double> MaxValues;

		public AssociativeMatrix(string graphName)
		{
			GraphName = graphName;
			MaxValues = new Dictionary<string, double>();
		}
	}
}
