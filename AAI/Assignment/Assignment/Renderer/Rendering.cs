using Assignment.World;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment.Renderer
{
	class Rendering
	{
		private Graphics graphicsPanel;
		private Panel panel;

		private Bitmap screen;
		private Graphics graphics;

		public Rendering(Graphics g, Panel p)
		{
			graphicsPanel = g;
			panel = p;
		}

		public void Render()
		{
			screen = new Bitmap((int) GameWorld.Instance.Width, (int) GameWorld.Instance.Height);

			using (graphics = Graphics.FromImage(screen))
			{
				RenderBackground();
				RenderStatic();
				//RenderGrid();
				RenderEntities();
				//RenderNavGraph();
                //RenderShortestPath();
			}

			graphicsPanel.DrawImage(screen, 0, 0);
		}

		private void RenderBackground()
		{
			graphics.FillRectangle(Brushes.White, 0, 0, panel.Width, panel.Height);
		}

		private void RenderStatic()
		{

		}

		private void RenderGrid()
		{
			var pen = new Pen(Color.Red);
			var font = new Font(FontFamily.GenericSansSerif, 7);
			var grid = GameWorld.Instance.Grid;

			for (int x = 0; x < grid.GridWidth; x++)
			{
				for (int y = 0; y < grid.GridHeight; y++)
				{
					graphics.DrawRectangle(pen, x * Grid.CellWidth, y * Grid.CellHeight, Grid.CellWidth, Grid.CellHeight);

					graphics.DrawString(grid.GridCells[x, y].Entities.Count.ToString(), font, Brushes.Blue, x * Grid.CellWidth, y * Grid.CellHeight);
				}
			}
		}

		private void RenderEntities()
		{
			int size = 20;
			foreach (var entity in GameWorld.Instance.Entities)
			{
				graphics.FillEllipse(Brushes.Blue, (int) entity.Location.X - (size / 2), (int) entity.Location.Y - (size / 2), size, size);

				int x = (int) (entity.Location.X + (Math.Cos(entity.Direction) * size));
				int y = (int) (entity.Location.Y + (Math.Sin(entity.Direction) * size));

				graphics.DrawLine(new Pen(Color.Green), (int) entity.Location.X, (int) entity.Location.Y, x, y);
			}
		}
        
        private void RenderNavGraph()
        {
            const bool SHOW_VERTEX_LABEL = false;
            const bool SHOW_EDGES = false;
            /**
            foreach (var entry in GameWorld.Instance.NavGraph.old_vertices)
            {
                Graph.Vertex v = entry.Value;
                float pointRadius = 1;
                graphics.DrawEllipse(new Pen(Color.LawnGreen), (float) v.Loc.X - pointRadius / 2, (float) v.Loc.Y - pointRadius / 2, pointRadius, pointRadius);
                if (SHOW_EDGES)
                {
                    foreach (Graph.Edge e in v.Adj)
                    {
                        Graph.Vertex w = e.Dest;
                        graphics.DrawLine(new Pen(Color.Black, 1), (float)v.Loc.X, (float)v.Loc.Y, (float)w.Loc.X, (float)w.Loc.Y);
                    }
                }
                if (SHOW_VERTEX_LABEL)
                {
                    Brush b = new SolidBrush(Color.DarkBlue);
                    Font f = new Font(SystemFonts.DefaultFont.Name, 6);
                    graphics.DrawString(v.Label, f, b, (float) v.Loc.X, (float) v.Loc.Y);
                }
            }**/
            var vs = GameWorld.Instance.NavGraph.vertices;
            for (int x = 0; x < vs.GetLength(0); x++)
            {
                for (int y = 0; y < vs.GetLength(1); y++)
                {
                    Graph.Vertex v = vs[x, y];

                    float pointRadius = 1;
                    graphics.DrawEllipse(new Pen(Color.LawnGreen), (float)v.Loc.X - pointRadius / 2, (float)v.Loc.Y - pointRadius / 2, pointRadius, pointRadius);

                    if (SHOW_VERTEX_LABEL)
                    {
                        Brush b = new SolidBrush(Color.DarkBlue);
                        Font f = new Font(SystemFonts.DefaultFont.Name, 6);
                        graphics.DrawString(v.Label, f, b, (float)v.Loc.X, (float)v.Loc.Y);
                    }
                }
            }
        }

        private void RenderShortestPath()
        {
            var vertices = GameWorld.Instance.NavGraph.old_vertices;
            try
            {
                Graph.Vertex start, goal;
                vertices.TryGetValue("0", out start);
                vertices.TryGetValue("100", out goal);
                List<Graph.Vertex> path = Movement.Pathfinding.AStar(start, goal);

                Pen p = new Pen(Color.DeepPink);
                for (int i = 1; i < path.Count; i++)
                {
                    Graph.Vertex v = path[i - 1];
                    Graph.Vertex w = path[i];
                    graphics.DrawLine(p, (float) v.Loc.X, (float) v.Loc.Y, (float)w.Loc.X, (float) w.Loc.Y);
                }
            } catch (NullReferenceException e)
            {
                Console.WriteLine("Could not render Shortest Path");
                return;
            }        
        }
	}
}
