using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG.G3D
{
	public partial class Form3D : Form
	{
		private Cube cube;

		public Form3D()
		{
			cube = new Cube(Color.Green);
			var matrix = Matrix.ScalingMatrix3D(0.1f, 0.1f, 0.1f);
			//var matrix = Matrix.IdentityMatrix(4);
			for (int i = 0; i < cube.vertexbuffer.Count; i++)
			{
				cube.vertexbuffer[i] *= matrix;
			}

			InitializeComponent();
		}

		private void rotateButton_Click(object sender, EventArgs e)
		{
			// rotate
		}

		private void drawPanel_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			Draw3D(g);
			//cube.Draw(g, cube.vertexbuffer);
		}

		private void Draw3D(Graphics g)
		{
			var d = 800f;
			var r = 10f;
			var theta = -90f;
			var phi = -90f;
			// draw cube
			var viewMatrix = Matrix.ViewMatrix3D(theta, phi, r);
			var drawableCube = new List<Vector>();
			foreach (var original in cube.vertexbuffer)
			{
				var changed = original * viewMatrix;
				var translateMatrices = Matrix.ProjectionMatrix(d, changed.Values[3]);
				drawableCube.Add((changed * translateMatrices) + new Vector(new float[] { 150f, 150f, 0, 0 }));

				//Pen pen = new Pen(Color.Green, 3f);
				//g.DrawLine(pen, drawer.Values[0], drawer.Values[1], drawer.Values[0], drawer.Values[1]);
				Console.WriteLine(drawableCube.Last());
			}
			cube.Draw(g, drawableCube);
		}
	}
}