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
            //RenderNavGraph();
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

				int x = (int) (entity.Location.X + Math.Cosh(entity.Direction) * size);
				int y = (int) (entity.Location.Y + Math.Sinh(entity.Direction) * size);
					
				graphics.DrawLine(new Pen(Color.Green), (int) entity.Location.X, (int) entity.Location.Y, x, y);
			}
		}
        
        private void RenderNavGraph()
        {
            foreach (var entry in GameWorld.Instance.NavGraph.vertices)
            {
                Graph.Vertex v = entry.Value;
                float pointRadius = 1;
                graphics.DrawEllipse(new Pen(Color.LawnGreen), (float) v.Loc.X - pointRadius / 2, (float) v.Loc.Y - pointRadius / 2, pointRadius, pointRadius);
            }
        }
	}
}
