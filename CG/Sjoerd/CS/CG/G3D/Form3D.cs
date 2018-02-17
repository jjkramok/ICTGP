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
			}
			cube.Draw(g, drawableCube);
		}


		// todo implement
		private void Form3D_KeyDown(object sender, KeyEventArgs e)
		{
			bool inverse = e.Shift;
			switch (e.KeyCode)
			{
				case Keys.Up: // change x/z
					break;
				case Keys.Down: // change x/z
					break;
				case Keys.Left: // change x/z
					break;
				case Keys.Right: // change x/z
					break;
				case Keys.X: // rotate
					break;
				case Keys.Y: // rotate
					break;
				case Keys.Z: // rotate
					break;
				case Keys.PageUp: // change y
					break;
				case Keys.PageDown: // change y
					break;
				case Keys.S: // scale
					break;
				case Keys.A: // animate
					break;
				case Keys.C: // reset
					break;
			}
			drawPanel.Refresh();
		}
		
	}
}