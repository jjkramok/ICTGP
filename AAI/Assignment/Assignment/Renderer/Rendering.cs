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
		private Graphics graphics;
		private Panel panel;

		public Rendering(Graphics g, Panel p)
		{
			graphics = g;
			panel = p;
		}

		public void Render()
		{
			RenderBackground();
			RenderStatic();
			RenderEntities();
		}

		private void RenderBackground()
		{
			graphics.FillRectangle(Brushes.White, 0, 0, panel.Width, panel.Height);
		}

		private void RenderStatic()
		{

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
