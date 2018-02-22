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
				RenderGrid();
				RenderEntities();
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
	}
}
