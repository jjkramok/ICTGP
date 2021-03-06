﻿using Assignment.Entity;
using Assignment.Fuzzy;
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
	public class GameWorld
	{
		private static GameWorld _instance = null;

		public SteeringForceCalculationType SteeringForceCalculationType;
		public double Width { get; private set; }
		public double Height { get; private set; }
		public Graph NavGraph { get; private set; }
		public List<BaseEntity> Entities { get; private set; }
		public List<BaseObstacle> Obstacles { get; private set; }
		internal List<Location> PathAlreadyCalculated = null; // Used to test pathfinding

		public int TickDelay;
		public MainForm Screen;
		public Random Random;

		private Stopwatch watch;
		private Timer timer;
		public long TickCounter;

		public int TickTime;

		// grids
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
            PathManager.Instance.UpdateSearches();
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

		}

		private void PostInitialize()
		{
			Settings.LoadSettings();
			ImageManager.Delete();
            PathManager.Delete();

			TickDelay = Settings.Instance.GameTickTime;

			Width = Settings.Instance.Width;
			Height = Settings.Instance.Height;

			FuzzyMachine.Initialize();
			StateMachine.Initialize();

			if(Settings.Instance.RandomSeed != 0)
				Random = new Random(Settings.Instance.RandomSeed);
			else
				Random = new Random();

			Obstacles = new List<BaseObstacle>();
			for (int i = 0; i < Settings.Instance.RockCount; i++)
			{
				Obstacles.Add(new Rock(new Location(Random.Next((int) Width), Random.Next((int) Height)), 30));
			}
			for (int i = 0; i < Settings.Instance.TreeCount; i++)
			{
				Obstacles.Add(new Tree(new Location(Random.Next((int) Width), Random.Next((int) Height)), 30));
			}

			SteeringForceCalculationType = SteeringForceCalculationType.WeightedTruncatedSum;

			StoneEdge();

			Entities = new List<BaseEntity>();
			for (int i = 0; i < Settings.Instance.HerbivoreCount; i++)
			{
				Entities.Add(new Herbivore { State = Settings.Instance.HerbivoreStartState,  Direction = Math.PI * 2 * Random.NextDouble(), Location = new Location(Random.Next(40, (int) Width - 40), Random.Next(40, (int) Height - 40)) });
			}

			for (int i = 0; i < Settings.Instance.OmnivoreCount; i++)
			{
				Entities.Add(new Omnivore { State = Settings.Instance.OmnivoreStartState, Direction = Math.PI * 2 * Random.NextDouble(), Location = new Location(Random.Next(40, (int) Width - 40), Random.Next(40, (int) Height - 40)) });
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

		private void InitGrids()
		{
			GridFood = new Grid<Tree>(Settings.Instance.SpatialPartitioningGridSize);
			GridEntity = new Grid<BaseEntity>(Settings.Instance.SpatialPartitioningGridSize);
			GridObstacle = new Grid<ObstacleCircle>(Settings.Instance.SpatialPartitioningGridSize);

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

		public List<BaseEntity> EntitiesInArea(Location location, double radius, EntityType type)
		{
			return ObjectFinder(GridEntity, location, radius).Where(x => x.Type == type).ToList();
		}

		public List<ObstacleCircle> ObstaclesInArea(Location location, double radius, bool includeObstacleRadius = false)
		{
			return ObjectFinder(GridObstacle, location, radius, includeObstacleRadius);
		}

		public List<Tree> FoodInArea(Location location, double radius)
		{
			return ObjectFinder(GridFood, location, radius);
		}

        private List<T> ObjectFinder<T>(Grid<T> grid, Location location, double radius, bool includeObstacleRadius = false) where T : BaseObject
		{
			var searchableObjects = grid.ObjectsNearLocation(location, radius);

			var closeObjects = new List<T>();
			foreach (var baseobject in searchableObjects)
			{
				if (!includeObstacleRadius && Utility.Distance(baseobject.Location, location) < radius)
				{
					closeObjects.Add(baseobject);
				} 
                if (includeObstacleRadius && (baseobject.GetType() == typeof(ObstacleCircle)))
                {
                    ObstacleCircle objectAsObstacle = (ObstacleCircle)Convert.ChangeType(baseobject, typeof(ObstacleCircle));
                    if (Utility.Distance(baseobject.Location, location) < radius + objectAsObstacle.Radius)
                    {
                        closeObjects.Add(baseobject);
                    }
                }
			}

			return closeObjects;
		}


	}
}
