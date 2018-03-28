using Assignment.Entity;
using Assignment.Obstacle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.World
{
	public class Grid<T> where T : BaseObject
	{
		public readonly int CellSize;

		public readonly int GridWidth;
		public readonly int GridHeight;

		public readonly GridCell<T>[,] GridCells;

		public Grid(int cellSize = 20)
		{
			CellSize = cellSize;

			GridWidth = (int) Math.Ceiling((double) GameWorld.Instance.Width / CellSize);
			GridHeight = (int) Math.Ceiling((double) GameWorld.Instance.Height / CellSize);

			GridCells = new GridCell<T>[GridWidth, GridHeight];
			for (var x = 0; x < GridWidth; x++)
			{
				for (var y = 0; y < GridHeight; y++)
				{
					GridCells[x, y] = new GridCell<T>();
				}
			}

		}

		public void MapObjects(List<T> baseobjects)
		{
			foreach (var baseobject in baseobjects)
			{
				var cellLocation = GetGridCellForLocation(baseobject.Location);
				GridCells[cellLocation.Item1, cellLocation.Item2].Objects.Add(baseobject);
			}
		}

		public List<T> ObjectsNearLocation(Location location, double distance)
		{
			var centerCell = GetGridCellForLocation(location);
			var cellDistanceX = (int) Math.Floor(distance / CellSize) + 1;
			var cellDistanceY = (int) Math.Floor(distance / CellSize) + 1;

			List<T> objects = new List<T>();

			for (int x = Math.Max(centerCell.Item1 - cellDistanceX, 0); x < Math.Min(centerCell.Item1 + cellDistanceX + 1, GridWidth); x++)
			{
				for (int y = Math.Max(centerCell.Item2 - cellDistanceY, 0); y < Math.Min(centerCell.Item2 + cellDistanceY + 1, GridHeight); y++)
				{
					objects.AddRange(GridCells[x, y].Objects);
				}
			}

			return objects;
		}

		public void UpdateObject(T baseobject, Location oldLocation)
		{
			var newCell = GetGridCellForLocation(baseobject.Location);
			var oldCell = GetGridCellForLocation(oldLocation);

			if (newCell.Equals(oldCell))
				return;

			GridCells[oldCell.Item1, oldCell.Item2].Objects.Remove(baseobject);
			GridCells[newCell.Item1, newCell.Item2].Objects.Add(baseobject);
		}

		private Tuple<int, int> GetGridCellForLocation(Location location)
		{
			int cellX = (int) (location.X / CellSize);
			int cellY = (int) (location.Y / CellSize);

			return new Tuple<int, int>(cellX, cellY);
		}
	}
}
