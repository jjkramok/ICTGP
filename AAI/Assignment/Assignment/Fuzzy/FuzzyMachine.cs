﻿using Assignment.Entity;
using Assignment.Obstacle;
using Assignment.Utilities;
using Assignment.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Fuzzy
{
	public static class FuzzyMachine
	{
		public static Dictionary<string, Graph> graphs;
		public static List<RuleSet> ruleSets;

		public static void Initialize()
		{
			graphs = FileManager.ReadGraphs();
			ruleSets = FileManager.ReadRules(graphs);
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
				var value = rules.Calculate(values, RuleSet.CalculationType.MeanOfMaximum);

				if(value > bestValue)
				{
					bestValue = value;
					bestIndex = i;
				}
			}

			return trees[bestIndex];
		}

	}
}
