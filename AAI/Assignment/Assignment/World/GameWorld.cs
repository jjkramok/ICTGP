using Assignment.Entity;
using Assignment.Movement;
using Assignment.Renderer;
using System;
using System.Collections.Generic;

namespace Assignment.World
{
	class GameWorld
	{
		private static GameWorld _instance = null;

		public SteeringForceCalculationType SteeringForceCalculationType;
		public double Width { get; private set; }
		public double Height { get; private set; }
		public Grid Grid { get; private set; }
		public Graph NavGraph { get; private set; }
		public List<BaseEntity> Entities { get; private set; }

		public const int TickDelay = 10;
		public MainForm Screen;
		public Random Random;

		public static GameWorld Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new GameWorld();
					_instance.PostInitialize();
				}
				return _instance;
			}
		}

		public void GameTick(object sender, EventArgs e)
		{
			UpdateEntites();
			Screen.Refresh();
		}

		private GameWorld()
		{
			Random = new Random();
			Entities = new List<BaseEntity>
			{
				new Herbivore{ Direction = Math.PI * 2 * Random.NextDouble(), Location = new Location(100, 100)},
			};

            Width = 600;
			Height = 600;
			SteeringForceCalculationType = SteeringForceCalculationType.WeightedTruncatedSum;
		}

		private void PostInitialize()
		{
			Grid = new Grid();
            NavGraph = new Graph();
        }

		public void UpdateEntites()
		{
			foreach(var entity in Entities)
			{
				Location oldLocation = new Location(entity.Location.X, entity.Location.Y);
				entity.Update(1);
				Grid.UpdateEntity(entity, oldLocation);
			}
		}

		public static void DeleteWorld()
		{
			_instance = null;
		}

		public List<BaseEntity> EntitiesInArea(Location location, double radius)
		{
			var searchableEntities = Grid.EntitiesNearLocation(location, radius);

			var closeEntities = new List<BaseEntity>();
			foreach(var entity in searchableEntities)
			{
				if(Utilities.Utilities.Distance(entity.Location, location) < radius)
				{
					closeEntities.Add(entity);
				}
			}

			return closeEntities;
		}
	}
}
