using Assignment.Entity;
using Assignment.Obstacle;
using Assignment.Utilities;
using Assignment.World;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment.Fuzzy
{
	public static class FuzzyMachine
	{
		public static Dictionary<string, Graph> graphs;
		public static List<RuleSet> ruleSets;

		private static RuleSet.CalculationType calculationType;

		public static void Initialize()
		{
			graphs = FileManager.ReadGraphs();
			ruleSets = FileManager.ReadRules(graphs);

			switch (Settings.Instance.FuzzyCalculationType)
			{
				case "AvarageOfMaximum":
					calculationType = RuleSet.CalculationType.AvarageOfMaximum;
					break;
				case "Centroid":
					calculationType = RuleSet.CalculationType.Centroid;
					break;
				case "MeanOfMaximum":
					calculationType = RuleSet.CalculationType.MeanOfMaximum;
					break;
				default:
					calculationType = RuleSet.CalculationType.AvarageOfMaximum;
					break;
			}
		}

		public static Tree BestTree(BaseEntity entity)
		{
			var rules = ruleSets.First(x => x.OutputGraphs.Contains("Tree"));
			var trees = GameWorld.Instance.FoodInArea(entity.Location, graphs["Distance"].MaxValue);
			int bestIndex = 0;
			double bestValue = 0;
			for (int i = 0; i < trees.Count; i++)
			{
				var tree = trees[i];
				var values = new Dictionary<string, double>();
				values.Add("Distance", Utility.Distance(entity.Location, tree.Location));
				values.Add("FoodEntity", entity.Food);
				values.Add("EntitiesNearTree", GameWorld.Instance.EntitiesInArea(tree.Location, 50).Count);
				var value = rules.Calculate(values, calculationType);

				if (value["Tree"] > bestValue)
				{
					bestValue = value["Tree"];
					bestIndex = i;
				}
			}
			return trees[bestIndex];
		}
#if DEBUG
		public static void TestFuzzy()
		{
			var rules = ruleSets.First(x => x.OutputGraphs.Contains("Weapon"));

			var values = new Dictionary<string, double>();
			values.Add("Ammo", 8);
			values.Add("DTT", 200);
			Console.WriteLine("Fuzzy(MOM, 83)			= " + rules.Calculate(values, RuleSet.CalculationType.MeanOfMaximum)["Weapon"]);

			Console.WriteLine("Fuzzy(Centroid, 62)		= " + rules.Calculate(values, RuleSet.CalculationType.Centroid)["Weapon"]);

			Console.WriteLine("Fuzzy(MaxAv, 60.416667)		= " + rules.Calculate(values, RuleSet.CalculationType.AvarageOfMaximum)["Weapon"]);
		}
#endif
	}
}
