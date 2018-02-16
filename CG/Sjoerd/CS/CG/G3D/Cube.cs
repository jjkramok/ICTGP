using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CG;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CG.G3D
{
	public class Cube
	{
		//          3----------2
		//         /|         /|
		//        / |        / |                z
		//       /  7-------/--6                |
		//      4--/-------1  /                 ----x
		//      | /        | /                 /
		//      |/         |/                  y
		//      8----------5

		private const int size = 1;
		public List<Vector> vertexbuffer = new List<Vector>
		{
			new Vector(new float[]{ 1.0f,  1.0f, 1.0f, 1.0f }),     //1
            new Vector(new float[]{ 1.0f, -1.0f, 1.0f, 1.0f }),     //2
            new Vector(new float[]{-1.0f, -1.0f, 1.0f, 1.0f }),     //3
            new Vector(new float[]{-1.0f,  1.0f, 1.0f, 1.0f }),     //4

            new Vector(new float[]{ 1.0f,  1.0f, -1.0f, 1.0f}),    //5
            new Vector(new float[]{ 1.0f, -1.0f, -1.0f, 1.0f}),    //6
            new Vector(new float[]{-1.0f, -1.0f, -1.0f, 1.0f}),    //7
            new Vector(new float[]{-1.0f,  1.0f, -1.0f, 1.0f}),    //8

            new Vector(new float[]{ 1.2f,  1.2f, 1.2f, 1.0f }),     //1
            new Vector(new float[]{ 1.2f, -1.2f, 1.2f, 1.0f }),     //2
            new Vector(new float[]{-1.2f, -1.2f, 1.2f, 1.0f }),     //3
            new Vector(new float[]{-1.2f,  1.2f, 1.2f, 1.0f }),     //4

            new Vector(new float[]{ 1.2f,  1.2f, -1.2f, 1.0f}),    //5
            new Vector(new float[]{ 1.2f, -1.2f, -1.2f, 1.0f}),    //6
            new Vector(new float[]{-1.2f, -1.2f, -1.2f, 1.0f}),    //7
            new Vector(new float[]{-1.2f,  1.2f, -1.2f, 1.0f})     //8
        };

		Color col;

		public Cube(Color c)
		{
			col = c;
		}

		public void Draw(Graphics g, List<Vector> vb)
		{
			Pen pen = new Pen(col, 3f);
			g.DrawLine(pen, vb[0].Values[0], vb[0].Values[1], vb[1].Values[0], vb[1].Values[1]);    //1 -> 2
			g.DrawLine(pen, vb[1].Values[0], vb[1].Values[1], vb[2].Values[0], vb[2].Values[1]);    //2 -> 3
			g.DrawLine(pen, vb[2].Values[0], vb[2].Values[1], vb[3].Values[0], vb[3].Values[1]);    //3 -> 4
			g.DrawLine(pen, vb[3].Values[0], vb[3].Values[1], vb[0].Values[0], vb[0].Values[1]);    //4 -> 1

			g.DrawLine(pen, vb[4].Values[0], vb[4].Values[1], vb[5].Values[0], vb[5].Values[1]);    //5 -> 6
			g.DrawLine(pen, vb[5].Values[0], vb[5].Values[1], vb[6].Values[0], vb[6].Values[1]);    //6 -> 7
			g.DrawLine(pen, vb[6].Values[0], vb[6].Values[1], vb[7].Values[0], vb[7].Values[1]);    //7 -> 8
			g.DrawLine(pen, vb[7].Values[0], vb[7].Values[1], vb[4].Values[0], vb[4].Values[1]);    //8 -> 5

			pen.DashStyle = DashStyle.DashDot;
			g.DrawLine(pen, vb[0].Values[0], vb[0].Values[1], vb[4].Values[0], vb[4].Values[1]);    //1 -> 5
			g.DrawLine(pen, vb[1].Values[0], vb[1].Values[1], vb[5].Values[0], vb[5].Values[1]);    //2 -> 6
			g.DrawLine(pen, vb[2].Values[0], vb[2].Values[1], vb[6].Values[0], vb[6].Values[1]);    //3 -> 7
			g.DrawLine(pen, vb[3].Values[0], vb[3].Values[1], vb[7].Values[0], vb[7].Values[1]);    //4 -> 8

			Font font = new Font("Arial", 12, FontStyle.Bold);
			for (int i = 0; i < 8; i++)
			{
				PointF p = new PointF(vb[i + 8].Values[0], vb[i + 8].Values[1]);
				g.DrawString(i.ToString(), font, Brushes.Gray, p);
			}
		}

	}
}
