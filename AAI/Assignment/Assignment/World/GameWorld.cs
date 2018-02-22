using Assignment.Entity;
using Assignment.Movement;
using Assignment.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public const int TickDelay = 50;
		public MainForm Screen;
		public Random Random;

		public static GameWorld Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new GameWorld();
					_instance.PostInitialize()
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
				new Herbivore{ Direction = Math.PI * 0.1, Location = new Location(50, 50)},
			};
			Width = 200;
			Height = 200;
			SteeringForceCalculationType = SteeringForceCalculationType.WeightedTruncatedSum;
		}

		private void PostInitialize()
		{
			Grid = new Grid();
		}

		public void UpdateEntites()
		{
			foreach(var entity in Entities)
			{
				entity.Update(1);
			}
		}

		public static void DeleteWorld()
		{
			_instance = null;
		}

		public List<BaseEntity> EntitiesInArea(Location location, double radius)
		{
			return new List<BaseEntity>();
		}
	}
}
