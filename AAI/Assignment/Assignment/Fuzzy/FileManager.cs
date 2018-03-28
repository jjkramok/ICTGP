using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Fuzzy
{
	public static class FileManager
	{
		public const string graphsFile = "./fuzzyrules/graphs.txt";
		public const string rulesFile = "./fuzzyrules/rules.txt";

		#region graphReading
		public static Dictionary<string, Graph> ReadGraphs()
		{
			var graphs = new Dictionary<string, Graph>();
			string currentGraph = "";
			int currentGraphReadCount = 0;

			var readingState = GraphReadingState.ReadyForNew;
			var lines = File.ReadAllLines(graphsFile);
			for (int i = 0; i < lines.Length; i++)
			{
				var line = lines[i].Trim();
				if (string.IsNullOrEmpty(line) || line.StartsWith("#")) continue;

				switch (readingState)
				{
					case GraphReadingState.ReadyForNew:
						graphs.Add(line, new Graph(line));
						currentGraph = line;
						currentGraphReadCount = 0;
						readingState = GraphReadingState.ReadingGraph;
						break;
					case GraphReadingState.ReadingGraph:
						SetGraphtItem(graphs[currentGraph], line, i + 1);
						if (currentGraphReadCount == 3)
						{
							readingState = GraphReadingState.ReadyForNew;
						}
						currentGraphReadCount++;
						break;
				}
			}

			return graphs;
		}

		private static void SetGraphtItem(Graph graph, string line, int lineNumber)
		{
			if (!line.Contains('='))
			{
				throw new Exception($"Expected an = sign on line {lineNumber}");
			}

			var parts = line.Split('=');
			switch (parts[0].Trim())
			{
				case "MIN":
					var minValue = GetDoubleValue(parts[1].Trim(), lineNumber);
					graph.MinValue = minValue;
					break;
				case "MAX":
					var maxValue = GetDoubleValue(parts[1].Trim(), lineNumber);
					graph.MaxValue = maxValue;
					break;
				case "SECTIONS":
					SetGraphSections(graph, parts[1].Trim(), lineNumber);
					break;
				case "SPACING":
					SetGraphSpacing(graph, parts[1].Trim(), lineNumber);
					break;
				default:
					throw new Exception($"Unexpected '{parts[0].Trim()}' on line {lineNumber}");
			}
		}

		private static void SetGraphSections(Graph graph, string value, int lineNumber)
		{
			graph.Sections = new List<GraphSection>();
			var sections = value.Split(',');
			if (sections.Length < 2)
				throw new Exception($"Graph should contain at least 2 sections. On line {lineNumber}.");

			for (int i = 0; i < sections.Length; i++)
			{
				var section = sections[i].Trim();
				if (graph.Sections.Any(x => x.Name == section))
					throw new Exception($"Cannot add 2 graph sections with the same name: {section}. On line {lineNumber}.");

				GraphSection.GraphSectionType type = GraphSection.GraphSectionType.Center;
				if (i == 0)
					type = GraphSection.GraphSectionType.LeftShoulder;
				else if (i == sections.Length - 1)
					type = GraphSection.GraphSectionType.RightShoulder;

				graph.Sections.Add(new GraphSection(section, type));
			}
		}

		private static void SetGraphSpacing(Graph graph, string value, int lineNumber)
		{
			var spacing = value.Split(',');
			if (graph.Sections.Count == 0)
				throw new Exception($"Sections have to be set before spacing. On line {lineNumber}.");
			if (spacing.Length != graph.Sections.Count * 2 - 2)
				throw new Exception($"Spacing item count does not match section length, there should be {graph.Sections.Count * 2 - 2} spacing item, there are {spacing.Length}. On line {lineNumber}.");

			int sectionIndex = 0;
			bool isMax = true;
			for (int i = 0; i < spacing.Length; i++)
			{
				var spacerValue = GetDoubleValue(spacing[i].Trim(), lineNumber);
				if (isMax)
				{
					graph.Sections[sectionIndex].MaxHigh = spacerValue;
					graph.Sections[sectionIndex + 1].MinLow = spacerValue;
				}
				else
				{
					graph.Sections[sectionIndex].MaxLow = spacerValue;
					graph.Sections[sectionIndex + 1].MinHigh = spacerValue;
					sectionIndex++;
				}
				isMax = !isMax;
			}
		}

		private static double GetDoubleValue(string value, int lineNumber)
		{
			if (!double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
			{
				throw new Exception($"Unexpected '{value}', should be a valid double on line {lineNumber}");
			}
			return result;
		}

		private enum GraphReadingState
		{
			ReadyForNew,
			ReadingGraph
		}
		#endregion

		#region rulesReading
		public static List<RuleSet> ReadRules(Dictionary<string, Graph> graphs)
		{
			List<RuleSet> rulesets = new List<RuleSet>();

			int rulesetIndex = -1;
			var lines = File.ReadAllLines(rulesFile);
			for (int i = 0; i < lines.Length; i++)
			{
				var line = lines[i].Trim();
				if (string.IsNullOrEmpty(line) || line.StartsWith("#")) continue;

				switch (line.Split(' ')[0])
				{
					case "INPUTS":
						rulesets.Add(new RuleSet());
						rulesetIndex++;
						SetRuleSet(graphs, line.Split('=')[1], i + 1, rulesets[rulesetIndex].InputGraphs);
						break;
					case "OUTPUT":
						SetRuleSet(graphs, line.Split('=')[1], i + 1, rulesets[rulesetIndex].OutputGraphs);
						break;
					case "IF":
						SetRuleSetRule(rulesets[rulesetIndex], graphs, line.Substring(2).Trim(), i + 1);
						break;

				}
			}
			return rulesets;
		}

		private static void SetRuleSetRule(RuleSet ruleSet, Dictionary<string, Graph> graphs, string line, int lineNumber)
		{
			var rule = new RuleSet.Rule();
			if (!line.Contains("THEN"))
				throw new Exception($"No THEN in IF statement on line {lineNumber}");

			rule.Inputs = RuleSetRulePart(ruleSet.InputGraphs, graphs, line.Split(new string[] { "THEN" }, StringSplitOptions.None)[0], lineNumber);
			rule.Output = RuleSetRulePart(ruleSet.OutputGraphs, graphs, line.Split(new string[] { "THEN" }, StringSplitOptions.None)[1], lineNumber);

			ruleSet.Rules.Add(rule);
		}

		private static List<Tuple<string, string>> RuleSetRulePart(List<string> validGraphs, Dictionary<string, Graph> graphs, string value, int lineNumber)
		{
			var result = new List<Tuple<string, string>>();
			var parts = value.Split(new string[] { "AND" }, StringSplitOptions.None);
			for (int i = 0; i < parts.Length; i++)
			{
				var part = parts[i].Trim();
				var subParts = part.Split('.');
				// check valid values.
				if (!validGraphs.Contains(subParts[0]))
					throw new Exception($"Graph \"{subParts[0]}\"is not found in input graphs. on line {lineNumber}");
				if (!graphs[subParts[0]].Sections.Any(y => y.Name == subParts[1]))
					throw new Exception($"Graph \"{subParts[0]}\" does not contain value \"{subParts[1]}\". on line {lineNumber}");

				result.Add(new Tuple<string, string>(subParts[0], subParts[1]));
			}
			return result;
		}

		private static void SetRuleSet(Dictionary<string, Graph> graphs, string value, int lineNumber, List<string> graphSet)
		{
			var parts = value.Split(',');
			for (int i = 0; i < parts.Length; i++)
			{
				var part = parts[i].Trim();
				if (!graphs.ContainsKey(part))
					throw new Exception($"Graph with name: \"{part}\" not found. On line {lineNumber}");

				graphSet.Add(part);
			}
		}

		#endregion

	}
}
