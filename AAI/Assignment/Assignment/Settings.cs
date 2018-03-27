using Assignment.World;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
	public class Settings
	{
        public int Width = 500;
        public int Height = 500;
        public double NavigationCoarseness = 50;

		public int OmnivoreCount = 10;
		public int HerbivoreCount = 10;

        public double EntitySize = 5f;

        public string OmnivoreStartState = "DEBUGSTATE";
        public string HerbivoreStartState = "DEBUGSTATE";

        public int RockCount = 5;
		public int TreeCount = 1;

		public int SpatialPartitioningGridSize = 40;
        public bool UseTimeSlicedNavigation = false;
        public bool UseFinePathSmoothing = true;

        public int GameTickTime = 50;
        public int MaxPathfindingCyclesPerTick = 100;

        public int RandomSeed = 0;

		public string FuzzyCalculationType = "Centroid";


		private const string settingsFile = "settings.ini";

		// settings is singleton
		private static Settings _instance;
		private Settings() { }

		public static Settings Instance
		{
			get
			{
				if (_instance == null)
				{
					LoadSettings();
				}
				return _instance;
			}
		}

		public static Settings LoadSettings()
		{
			if (!File.Exists(settingsFile))
			{
				Console.WriteLine("Settings file not found");
				return new Settings();
			}
#if DEBUG
			MoveFile();
#endif
			_instance = new Settings();
			var settingsLines = File.ReadAllLines(settingsFile);

			for (var i = 0; i < settingsLines.Length; i++)
			{
				var line = settingsLines[i].Trim();
				if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
				{
					continue;
				}
				if (!line.Contains("="))
				{
					Console.WriteLine($"Invalid setting on line {i + 1} no '=' found");
				}

				try
				{
					_instance.SetSetting(line);
				}
				catch (Exception e)
				{
					Console.WriteLine($"Invalid setting on line {i + 1}, {e.Message}");
				}
			}
			return _instance;
		}

		private void SetSetting(string line)
		{
			var lineParts = line.Split('=');
			if (lineParts.Length != 2)
			{
				throw new Exception("line doesn't have one '=' character");
			}

			var field = GetType().GetFields().FirstOrDefault(x => x.Name == lineParts[0].Trim());

			if (field != null)
			{
				var value = Convert.ChangeType(lineParts[1].Trim(), field.FieldType);
				field.SetValue(this, value);
			}
			else
			{
				throw new Exception($"property {lineParts[0].Trim()} not found");
			}
		}

#if DEBUG
		private static void MoveFile()
		{
			try
			{
				File.Copy("./../../settings.ini", "./settings.ini", true);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
#endif
	}
}
