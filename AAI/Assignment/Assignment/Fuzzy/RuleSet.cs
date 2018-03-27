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
		#region Calculations
		public Dictionary<string, double> Calculate(Dictionary<string, double> values, CalculationType calculationType)
		{
			var matrices = CalculateMatrices(values);

			switch (calculationType)
			{
				case CalculationType.AvarageOfMaximum:
					return AvarageOfMaximum(matrices);
				case CalculationType.Centroid:
					return Centroid(matrices);
				case CalculationType.MeanOfMaximum:
					return MeanOfMaximum(matrices);
			}
			throw new Exception("Invalid calculationType set in ruleset");
		}



		private Dictionary<string, double> MeanOfMaximum(Dictionary<string, AssociativeMatrix> matrices)
		{
			var result = new Dictionary<string, double>();
			foreach (var matrix in matrices)
			{
				var matrixValue = matrix.Value.MaxValues.Max(x => x.Value);
				result.Add(matrix.Key, matrixValue);
			}
			return result;
		}

		private Dictionary<string, double> Centroid(Dictionary<string, AssociativeMatrix> matrices)
		{
			var result = new Dictionary<string, double>();
			foreach (var matrix in matrices)
			{
				double top = 0;
				double bottom = 0;
				var graph = FuzzyMachine.graphs[matrix.Key];
				foreach (var section in graph.Sections)
				{
					var min = graph.MinValue;
					var max = graph.MaxValue;
					var sectionValue = matrix.Value.MaxValues[section.Name];
					var total = 0d;

					if (section.Type != GraphSection.GraphSectionType.RightShoulder)
					{
						max = section.MaxHigh;
						total += (section.MaxLow - section.MaxHigh) * (1 - sectionValue);
					}
					if (section.Type != GraphSection.GraphSectionType.LeftShoulder)
					{
						min = section.MinHigh;
					}
					sectionValue += matrix.Value.MaxValues[section.Name];

					top += ((min + max) / 2) * matrix.Value.MaxValues[section.Name];
				}
			}
			return result;
		}

		private Dictionary<string, double> AvarageOfMaximum(Dictionary<string, AssociativeMatrix> matrices)
		{
			var result = new Dictionary<string, double>();
			foreach (var matrix in matrices)
			{
				double top = 0;
				double bottom = 0;
				var graph = FuzzyMachine.graphs[matrix.Key];
				foreach (var section in graph.Sections)
				{
					var min = graph.MinValue;
					var max = graph.MaxValue;

					if (section.Type != GraphSection.GraphSectionType.RightShoulder)
					{
						max = section.MaxHigh;
					}
					if (section.Type != GraphSection.GraphSectionType.LeftShoulder)
					{
						min = section.MinHigh;
					}

					top += ((min + max) / 2) * matrix.Value.MaxValues[section.Name];
				}

				result.Add(matrix.Key, top / bottom);
			}
			return result;
		}

		private Dictionary<string, AssociativeMatrix> CalculateMatrices(Dictionary<string, double> values)
		{
			var matrices = new Dictionary<string, AssociativeMatrix>();

			// Create matrices.
			for (int i = 0; i < OutputGraphs.Count; i++)
			{
				var matrix = new AssociativeMatrix(OutputGraphs[i]);
				foreach (var section in FuzzyMachine.graphs[matrix.GraphName].Sections)
				{
					matrix.MaxValues.Add(section.Name, 0);
				}
			}

			// PopulateMatrices
			foreach (var rule in Rules)
			{
				// Magic is real here.
				double max = 0;
				foreach (var input in rule.Inputs)
				{
					var value = FuzzyMachine.graphs[input.Item1].Sections.First(x => x.Name == input.Item2).ValueForLocation(values[input.Item1]);
					if (value > max)
					{
						max = value;
					}
				}
				foreach (var output in rule.Output)
				{
					if (matrices[output.Item1].MaxValues[output.Item2] < max)
					{
						matrices[output.Item1].MaxValues[output.Item2] = max;
					}
				}
			}


			return matrices;
		}
		#endregion

		#region ExtraClasses
		public class Rule
		{
			public List<Tuple<string, string>> Inputs;
			public List<Tuple<string, string>> Output;
		}

		public enum CalculationType
		{
			MeanOfMaximum,
			Centroid,
			AvarageOfMaximum
		}
		#endregion
	}
}
