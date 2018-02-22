using Assignment.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.World
{
	public class Grid
	{
		public const int CellHeight = 20;
		public const int CellWidth = 20;

		public readonly int GridWidth;
		public readonly int GridHeight;

		private GridCell[,] gridcells;

		public Grid()
		{
			GridWidth = (int) Math.Ceiling((double) GameWorld.Instance.Width / CellWidth);
			GridHeight = (int) Math.Ceiling((double) GameWorld.Instance.Height / CellHeight);

			gridcells = new GridCell[GridWidth, GridHeight];
			for (var x = 0; x < GridWidth; x++)
			{
				for (var y = 0; y < GridHeight; y++)
				{
					gridcells[x, y] = new GridCell();
				}
			}

			MapEntities();
		}

		private void MapEntities()
		{
			foreach (var entity in GameWorld.Instance.Entities)
			{
				var cellLocation = GetGridCellForLocation(entity.Location);
				gridcells[cellLocation.Item1, cellLocation.Item2].Entities.Add(entity);
			}
		}

		public List<BaseEntity> EntitiesNearLocation(Location location, double distance)
		{
			var centerCell = GetGridCellForLocation(location);
			var cellDistanceX = (int) Math.Ceiling(distance / CellWidth);
			var cellDistanceY = (int) Math.Ceiling(distance / CellHeight);

			List<BaseEntity> entities = new List<BaseEntity>();

			for(int x = Math.Max(centerCell.Item1 - cellDistanceX, 0); x < Math.Min(centerCell.Item1 + cellDistanceX, GridWidth); x++)
			{
				for (int y = Math.Max(centerCell.Item2 - cellDistanceY, 0); y < Math.Min(centerCell.Item2 + cellDistanceY, GridHeight); y++)
				{
					entities.AddRange(gridcells[x, y].Entities);
				}
			}

			return entities;
		}

		private Tuple<int, int> GetGridCellForLocation(Location location)
		{
			int cellX = (int) (location.X / CellWidth);
			int cellY = (int) (location.Y / CellHeight);

			return new Tuple<int, int>(cellX, cellY);
		}
	}
}
