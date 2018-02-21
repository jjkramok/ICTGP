using Assignment.Entity;
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

		public double Width { get; private set; }
		public double Height { get; private set; }
		public double GridSize { get; private set; }
		public List<BaseEntity> Entities { get; private set; }

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
			Entities = new List<BaseEntity>();
			Width = 200;
			Height = 200;
			GridSize = 10;
		}

		public static void DeleteWorld()
		{
			_instance = null;
		}

		public List<BaseEntity> EntitiesInArea(double x, double y, double radius)
		{
			return new List<BaseEntity>();
		}
	
	}
}
