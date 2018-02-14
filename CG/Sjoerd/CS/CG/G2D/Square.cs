using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using CG;

namespace GC.G2D
{
	// Added in Lecture 3.1
	public class Square
	{
		Color color;
		private int size;
		private float weight;

		public List<Vector> vb;

		public Square(Color color, int size = 100, float weight = 3)
		{
			this.color = color;
			this.size = size;
			this.weight = weight;

			vb = new List<Vector>();
			vb.Add(new Vector(new float[] { 0, 0 }));
			vb.Add(new Vector(new float[] { size, 0 }));
			vb.Add(new Vector(new float[] { size, size }));
			vb.Add(new Vector(new float[] { 0, size }));
		}

		public void Draw(Graphics g)
		{
			Pen pen = new Pen(color, weight);
			g.DrawLine(pen, vb[0].Values[0], vb[0].Values[1], vb[1].Values[0], vb[1].Values[1]);
			g.DrawLine(pen, vb[1].Values[0], vb[1].Values[1], vb[2].Values[0], vb[2].Values[1]);
			g.DrawLine(pen, vb[2].Values[0], vb[2].Values[1], vb[3].Values[0], vb[3].Values[1]);
			g.DrawLine(pen, vb[3].Values[0], vb[3].Values[1], vb[0].Values[0], vb[0].Values[1]);
		}

		public void Translate(Vector Change)
		{
			for (int i = 0; i < vb.Count; i++)
			{
				vb[i] = vb[i] + Change;
			}
		}
	}
}
