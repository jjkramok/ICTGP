using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Fuzzy
{
	public class RuleSet
	{
		public List<Rule> Rules;
		public List<string> InputGraphs;
		public List<string> OutputGraphs;

		public RuleSet()
		{
			Rules = new List<Rule>();
			InputGraphs = new List<string>();
			OutputGraphs = new List<string>();
		}

		public double Calculate(Dictionary<string, double> values)
		{
			// todo implement
			return 0;
		}

		public class Rule
		{
			public List<Tuple<string, string>> Inputs;
			public List<Tuple<string, string>> Output;
		}
	}
}
