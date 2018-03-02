using Assignment.Entity;
using Assignment.Obstacle;
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

		public readonly GridCell[,] GridCells;

		public Grid()
		{
			GridWidth = (int) Math.Ceiling((double) GameWorld.Instance.Width / CellWidth);
			GridHeight = (int) Math.Ceiling((double) GameWorld.Instance.Height / CellHeight);

			GridCells = new GridCell[GridWidth, GridHeight];
			for (var x = 0; x < GridWidth; x++)
			{
				for (var y = 0; y < GridHeight; y++)
				{
					GridCells[x, y] = new GridCell();
				}
			}

			MapEntities();
			MapObstacles();
		}

		private void MapEntities()
		{
			foreach (var entity in GameWorld.Instance.Entities)
			{
				var cellLocation = GetGridCellForLocation(entity.Location);
				GridCells[cellLocation.Item1, cellLocation.Item2].Entities.Add(entity);
			}
		}

		private void MapObstacles()
		{
			foreach (var obstacle in GameWorld.Instance.Obstacles)
			{
				foreach (var circle in obstacle.CollisionCircles)
				{
					var cellLocation = GetGridCellForLocation(circle.Location);
					GridCells[cellLocation.Item1, cellLocation.Item2].Obstacles.Add(circle);
				}
			}
		}

		public List<BaseEntity> EntitiesNearLocation(Location location, double distance)
		{
			var centerCell = GetGridCellForLocation(location);
			var cellDistanceX = (int) Math.Floor(distance / CellWidth) + 1;
			var cellDistanceY = (int) Math.Floor(distance / CellHeight) + 1;

			List<BaseEntity> entities = new List<BaseEntity>();

			for (int x = Math.Max(centerCell.Item1 - cellDistanceX, 0); x < Math.Min(centerCell.Item1 + cellDistanceX + 1, GridWidth); x++)
			{
				for (int y = Math.Max(centerCell.Item2 - cellDistanceY, 0); y < Math.Min(centerCell.Item2 + cellDistanceY + 1, GridHeight); y++)
				{
					entities.AddRange(GridCells[x, y].Entities);
				}
			}

			return entities;
		}

		public void UpdateEntity(BaseEntity entity, Location oldLocation)
		{
			var newCell = GetGridCellForLocation(entity.Location);
			var oldCell = GetGridCellForLocation(oldLocation);

			if (newCell.Equals(oldCell))
				return;

			GridCells[oldCell.Item1, oldCell.Item2].Entities.Remove(entity);
			GridCells[newCell.Item1, newCell.Item2].Entities.Add(entity);
		}

		private Tuple<int, int> GetGridCellForLocation(Location location)
		{
			int cellX = (int) (location.X / CellWidth);
			int cellY = (int) (location.Y / CellHeight);

			return new Tuple<int, int>(cellX, cellY);
		}

		public List<ObstacleCircle> ObstacleCirclesNearLocation(Location location, double distance)
		{
            var centerCell = GetGridCellForLocation(location);
			var cellDistanceX = (int) Math.Floor(distance / CellWidth) + 1;
			var cellDistanceY = (int) Math.Floor(distance / CellHeight) + 1;

			List<ObstacleCircle> circles = new List<ObstacleCircle>();

			for (int x = Math.Max(centerCell.Item1 - cellDistanceX, 0); x < Math.Min(centerCell.Item1 + cellDistanceX + 1, GridWidth); x++)
			{
				for (int y = Math.Max(centerCell.Item2 - cellDistanceY, 0); y < Math.Min(centerCell.Item2 + cellDistanceY + 1, GridHeight); y++)
				{
                    circles.AddRange(GridCells[x, y].Obstacles);
				}
			}

			return circles;
		}
	}
}
