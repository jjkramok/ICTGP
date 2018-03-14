using Assignment.Entity;
using Assignment.Movement;
using Assignment.Obstacle;
using Assignment.Renderer;
using Assignment.State;
using Assignment.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Assignment.World
{
	class GameWorld
	{
		private static GameWorld _instance = null;

		public SteeringForceCalculationType SteeringForceCalculationType;
		public double Width { get; private set; }
		public double Height { get; private set; }
		public Graph NavGraph { get; private set; }
		public List<BaseEntity> Entities { get; private set; }
		public List<BaseObstacle> Obstacles { get; private set; }
		internal List<Graph.Vertex> PathAlreadyCalculated = null; // Used to test pathfinding

		public const int TickDelay = 50;
		public MainForm Screen;
		public Random Random;

		private Stopwatch watch;
		private Timer timer;
		public long TickCounter;

		public int TickTime;

		// grids
		public const int GRIDENTITY = 0;
		public const int GRIDOBSTACLE = 1;
		public const int GRIDFOOD = 2;
		public Grid<BaseEntity> GridEntity;
		public Grid<ObstacleCircle> GridObstacle;
		public Grid<Tree> GridFood;

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

			watch.Stop();
			TickTime = (int) watch.ElapsedMilliseconds;
			watch.Restart();
			TickCounter++;
		}

		#region initialize
		private GameWorld()
		{
			StateMachine.Initialize();

			Width = 550;
			Height = 550;

			Random = new Random();

			Obstacles = new List<BaseObstacle>();
			for (int i = 0; i < Width / 20; i++)
			{
				Obstacles.Add(new Rock(new Location(Random.Next((int) Width), Random.Next((int) Height)), 30));
			}

			SteeringForceCalculationType = SteeringForceCalculationType.WeightedTruncatedSum;
		}

		private void StoneEdge()
		{
			if (Obstacles == null)
				Obstacles = new List<BaseObstacle>();

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
				new Omnivore{ Direction = Math.PI * 0.4, Location = new Location(530, 320.01)},
            };
			for (int i = 0; i < 100; i++)
			{
				Entities.Add(new Herbivore { Direction = Math.PI * 2 * Random.NextDouble(), Location = new Location(40 + Random.Next(0, 500), 40 + Random.Next(0, 500)) });
			}

			InitGrids();
			NavGraph = new Graph();

			watch = new Stopwatch();
			watch.Start();

			timer = new Timer();
			timer.Interval = TickDelay;
			timer.Tick += new EventHandler(GameTick);
			timer.Start();
		}

		private void InitGrids()
		{
			GridFood = new Grid<Tree>();
			GridEntity = new Grid<BaseEntity>();
			GridObstacle = new Grid<ObstacleCircle>();

			GridFood.MapObjects(Obstacles.Where(x => x.GetType().Name == "Tree").Select(x => (Tree) x).ToList());
			GridEntity.MapObjects(Entities);
			foreach (var obstacle in Obstacles)
			{
				GridObstacle.MapObjects(obstacle.CollisionCircles);
			}
		}

		#endregion
		public void UpdateEntites()
		{
			foreach (var entity in Entities)
			{
				Location oldLocation = new Location(entity.Location.X, entity.Location.Y);
				entity.Update(1);
				GridEntity.UpdateObject(entity, oldLocation);
			}
		}

		public static void DeleteWorld()
		{
			Instance.timer.Stop();
			_instance = null;
		}

		public List<BaseEntity> EntitiesInArea(Location location, double radius)
		{
			return ObjectFinder(GridEntity, location, radius);
		}

		public List<ObstacleCircle> ObstaclesInArea(Location location, double radius)
		{
			return ObjectFinder(GridObstacle, location, radius);
		}

		public List<Tree> FoodInArea(Location location, double radius)
		{
			return ObjectFinder(GridFood, location, radius);
		}

		private List<T> ObjectFinder<T>(Grid<T> grid, Location location, double radius) where T : BaseObject
		{
			var searchableObjects = grid.ObjectsNearLocation(location, radius);

			var closeObjects = new List<T>();
			foreach (var baseobject in searchableObjects)
			{
				if (Utility.Distance(baseobject.Location, location) < radius)
				{
					closeObjects.Add(baseobject);
				}
			}

			return closeObjects;
		}

		
	}
}
