using Assignment.Entity;
using Assignment.Movement;
using Assignment.Obstacle;
using Assignment.Renderer;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
		public List<BaseObstacle> Obstacles { get; private set; }
        internal List<Graph.Vertex> PathAlreadyCalculated = null; // Used to test pathfinding

        public const int TickDelay = 50;
		public MainForm Screen;
		public Random Random;

		private Timer timer;

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

		public void GameTick(object sender = null, EventArgs e = null)
		{
			UpdateEntites();
			Screen.Render();
		}

		private GameWorld()
		{
			Random = new Random();
			Obstacles = new List<BaseObstacle>
			{
				new Rock(new Location(40,10), 20),
				new Rock(new Location(110, 150), 20),
				new Rock(new Location(40,90), 20),
				new Rock(new Location(90,90), 20),
				new Rock(new Location(80,40), 20),
				new Rock(new Location(80,10), 20),
			};

			Width = 1000;
			Height = 1000;
			SteeringForceCalculationType = SteeringForceCalculationType.WeightedTruncatedSum;
		}

		private void StoneEdge()
		{
			for (int i = 5; i < Width; i += 10)
			{
				Obstacles.Add(new Rock(new Location(i, 5), 1));
				Obstacles.Add(new Rock(new Location(i, Height - 5), 1));
			}

			for (int i = 5; i < Height; i += 10)
			{
				Obstacles.Add(new Rock(new Location(5, i), 1));
				Obstacles.Add(new Rock(new Location(Width - 5, i), 1));
			}
		}

		private void PostInitialize()
		{
			StoneEdge();

			Entities = new List<BaseEntity>
			{
				new Herbivore{ Direction = Math.PI * 2, Location = new Location(70, 90.01)},
				//new Omnivore{ Direction = Math.PI * 1.2, Location = new Location(130, 120.01)},
				new Omnivore{ Direction = Math.PI * 0.4, Location = new Location(530, 320.01)},
			};
			for (int i = 0; i < 100; i++)
			{
				Entities.Add(new Herbivore { Direction = Math.PI * 2 * Random.NextDouble(), Location = new Location(40 + Random.Next(0, 500), 40 + Random.Next(0, 500)) });
			}

			Grid = new Grid();
			NavGraph = new Graph();

			timer = new Timer();
			timer.Interval = TickDelay;
			timer.Tick += new EventHandler(GameTick);
			timer.Start();
		}

		public void UpdateEntites()
		{
			foreach (var entity in Entities)
			{
				Location oldLocation = new Location(entity.Location.X, entity.Location.Y);
				entity.Update(1);
				Grid.UpdateEntity(entity, oldLocation);
			}
		}

		public static void DeleteWorld()
		{
			Instance.timer.Stop();
			_instance = null;
		}

		public List<BaseEntity> EntitiesInArea(Location location, double radius)
		{
			var searchableEntities = Grid.EntitiesNearLocation(location, radius);

			var closeEntities = new List<BaseEntity>();
			foreach (var entity in searchableEntities)
			{
				if (Utilities.Utilities.Distance(entity.Location, location) < radius)
				{
					closeEntities.Add(entity);
				}
			}

			return closeEntities;
		}

		public List<ObstacleCircle> ObstaclesInArea(Location location, double radius)
		{
			var searchableObstacleCircles = Grid.ObstacleCirclesNearLocation(location, radius);

			var closeCircles = new List<ObstacleCircle>();
			foreach (var circle in searchableObstacleCircles)
			{
				if (Utilities.Utilities.Distance(circle.Location, location) < radius + circle.Radius)
                {
					closeCircles.Add(circle);
				}
			}

			return closeCircles;
		}
	}
}
