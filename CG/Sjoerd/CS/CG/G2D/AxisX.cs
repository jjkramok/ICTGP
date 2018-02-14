using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace CG.G2D
{
	public class AxisX
	{
		private int size;

		public List<Vector> vb;

		public AxisX(int size = 100)
		{
			this.size = size;

			vb = new List<Vector>();
			vb.Add(new Vector(new float[] { 0, 0 }));
			vb.Add(new Vector(new float[] { size, 0 }));
		}

		public void Draw(Graphics g, List<Vector> vb)
		{
			Pen pen = new Pen(Color.Red, 2f);
			g.DrawLine(pen, vb[0].Values[0], vb[0].Values[1], vb[1].Values[0], vb[1].Values[1]);
			Font font = new Font("Arial", 10);
			PointF p = new PointF(vb[1].Values[0], vb[1].Values[1]);
			g.DrawString("x", font, Brushes.Red, p);
		}
	}
}
