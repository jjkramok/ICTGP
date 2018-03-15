using Assignment.World;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Assignment.Renderer
{
	static class Rendering
	{
		private static Graphics graphicsPanel;
		private static Panel panel;

		private static Bitmap screen;
		private static Graphics graphics;

		public static bool RenderNavGraphOption = false;
		public static bool RenderAStarPathOption = false;
		public static bool RenderGridOption = false;
		public static bool RenderEntitiesInfoOption = false;
		public static bool RenderEntitiesForcesOption = false;

		public static void Render(Graphics g, Panel p)
		{
			graphicsPanel = g;
			panel = p;
			screen = new Bitmap((int) GameWorld.Instance.Width, (int) GameWorld.Instance.Height);

			using (graphics = Graphics.FromImage(screen))
			{
				RenderBackground();
				RenderObstacles();

				if (RenderGridOption)
					RenderGrid();

				if (RenderNavGraphOption)
					RenderNavGraph();

				RenderEntities();

				if (RenderEntitiesInfoOption)
					RenderEntitiesInfo();

				if (RenderEntitiesForcesOption)
					RenderEntitiesForces();

				if (RenderAStarPathOption)
					RenderShortestPath();

				RenderFPS();
			}

			graphicsPanel.DrawImage(screen, 0, 0, panel.Width, panel.Height);
		}

		private static void RenderBackground()
		{
			Image texture = ImageManager.Instance.GetImage("grass-background", 0);
			if (texture != null)
			{
				TextureBrush brush = new TextureBrush(texture, WrapMode.Tile);
				graphics.FillRectangle(brush, 0, 0, (float) GameWorld.Instance.Width, (float) GameWorld.Instance.Height);
			}
			else
			{
				graphics.FillRectangle(Brushes.White, 0, 0, (int) GameWorld.Instance.Width, (int) GameWorld.Instance.Height);
			}
		}

		private static void RenderObstacles()
		{
			const bool SHOW_OBSTACLE_LOCATION = false;

			foreach (var obstacle in GameWorld.Instance.Obstacles)
			{
				obstacle.Render(graphics);
				if (SHOW_OBSTACLE_LOCATION)
				{
					Brush b = new SolidBrush(Color.DarkGoldenrod);
					Font f = new Font(SystemFonts.DefaultFont.Name, 6);
					graphics.DrawString(obstacle.Location.X + ", " + obstacle.Location.Y, f, b, (float) obstacle.Location.X, (float) obstacle.Location.Y);
				}
			}
		}

		private static void RenderGrid()
		{
			var pen = new Pen(Color.Red);
			var font = new Font(FontFamily.GenericSansSerif, 7);
			var grid = GameWorld.Instance.GridEntity;

			for (int x = 0; x < grid.GridWidth; x++)
			{
				for (int y = 0; y < grid.GridHeight; y++)
				{
					graphics.DrawRectangle(pen, x * grid.CellSize, y * grid.CellSize, grid.CellSize, grid.CellSize);

					graphics.DrawString(grid.GridCells[x, y].Objects.Count.ToString(), font, Brushes.Blue, x * grid.CellSize, y * grid.CellSize);
				}
			}
		}

		private static void RenderEntities()
		{
			foreach (var entity in GameWorld.Instance.Entities)
			{
				var oldLocation = new Location(entity.Location.X, entity.Location.Y);
				entity.Render(graphics);
				GameWorld.Instance.GridEntity.UpdateObject(entity, oldLocation);
			}
		}

		private static void RenderEntitiesInfo()
		{
			foreach (var entity in GameWorld.Instance.Entities)
			{
				entity.RenderDebug(graphics);
			}
		}

		private static void RenderEntitiesForces()
		{
			foreach (var entity in GameWorld.Instance.Entities)
			{
				entity.RenderSteering(graphics);
			}
		}

		private static void RenderNavGraph()
		{
			const bool SHOW_VERTEX_LABEL = false;
			const bool SHOW_EDGES = true;
			const bool SHOW_VERTICES = true;

			var vs = GameWorld.Instance.NavGraph.vertices;
			for (int x = 0; x < vs.GetLength(0); x++)
			{
				for (int y = 0; y < vs.GetLength(1); y++)
				{
					Graph.Vertex v = vs[x, y];
					if (v != null)
					{
						if (SHOW_EDGES)
						{
							foreach (Graph.Edge e in v.Adjacent)
							{
								Graph.Vertex w = e.Dest;
								graphics.DrawLine(new Pen(Color.Black, 1), (float) v.Location.X, (float) v.Location.Y, (float) w.Location.X, (float) w.Location.Y);
								//if (v.Loc.X > w.Loc.X)
								//{
								//    graphics.DrawLine(new Pen(Color.Black, 1), (float)w.Loc.X, (float)w.Loc.Y, (float)w.Loc.X + 5, (float)w.Loc.Y + 5);
								//    graphics.DrawLine(new Pen(Color.Black, 1), (float)w.Loc.X, (float)w.Loc.Y, (float)w.Loc.X + 5, (float)w.Loc.Y - 5);
								//}
							}
						}
						if (SHOW_VERTICES)
						{
							float renderedVertexRadius = 1.5f;
							graphics.DrawEllipse(new Pen(Color.LawnGreen), (float) v.Location.X - renderedVertexRadius / 2,
								(float) v.Location.Y - renderedVertexRadius / 2, renderedVertexRadius, renderedVertexRadius);
						}
						if (SHOW_VERTEX_LABEL)
						{
							Brush b = new SolidBrush(Color.DarkBlue);
							Font f = new Font(SystemFonts.DefaultFont.Name, 6);
							graphics.DrawString(v.Label, f, b, (float) v.Location.X, (float) v.Location.Y);
						}
					}
				}
			}
		}

		private static bool RenderShortestPath()
		{
			const bool SHOW_SMOOTHED_PATH = true;

			Graph.Vertex[,] vertices = GameWorld.Instance.NavGraph.vertices;
			try
			{
				if (GameWorld.Instance.PathAlreadyCalculated == null)
				{
					Random rand = new Random();
					Graph.Vertex start = null, goal = null;
					while (goal == null || start == null)
					{
						// Generate random start and goal for pathfinder example
						start = vertices[rand.Next(vertices.GetLength(0)), rand.Next(vertices.GetLength(1))];//start = vertices[2, 1];
						goal = vertices[rand.Next(vertices.GetLength(0)), rand.Next(vertices.GetLength(1))];//goal = vertices[1, 15];
					}

					GameWorld.Instance.PathAlreadyCalculated = Movement.Pathfinding.AStar(start, goal);
				}

				List<Location> path = GameWorld.Instance.PathAlreadyCalculated;
				Pen p = new Pen(Color.MediumPurple, Math.Max(1, (float) GameWorld.Instance.Width / 250));
				for (int i = 1; i < path.Count; i++)
				{
					Location v = path[i - 1];
					Location w = path[i];
					graphics.DrawLine(p, (float) v.X, (float) v.Y, (float) w.X, (float) w.Y);
				}
				if (SHOW_SMOOTHED_PATH)
				{
					List<Location> smoothedPath = Movement.Pathfinding.FinePathSmoothing(path);
					p = new Pen(Color.DeepPink, Math.Max(1, (float) GameWorld.Instance.Width / 250));
					for (int i = 1; i < smoothedPath.Count; i++)
					{
						Location v = smoothedPath[i - 1];
						Location w = smoothedPath[i];
						graphics.DrawLine(p, (float) v.X, (float) v.Y, (float) w.X, (float) w.Y);
					}
				}
				return true;
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine("Could not render Shortest Path");
				return false;
			}
		}

		private static void RenderFPS()
		{
			if (GameWorld.Instance.TickTime == 0)
				return;

			var lastFPS = 1000 / GameWorld.Instance.TickTime;
			graphics.DrawString($"{lastFPS} fps", new Font(FontFamily.GenericSansSerif, 20), Brushes.Black, 20, 20);
		}
	}
}
