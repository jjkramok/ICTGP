using Assignment.Entity;
using Assignment.Movement;
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
		public double GridSize { get; private set; }
		public List<BaseEntity> Entities { get; private set; }

		public Random Random;

		public static GameWorld Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new GameWorld();
				}
				return _instance;
			}
		}
		private GameWorld()
		{
			Random = new Random();
			Entities = new List<BaseEntity>
			{
				new BaseEntity{ Direction = Math.PI * 0.1, Location = new Location(50, 50)},
			};
			Width = 200;
			Height = 200;
			GridSize = 10;
			SteeringForceCalculationType = SteeringForceCalculationType.Dithering;

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
