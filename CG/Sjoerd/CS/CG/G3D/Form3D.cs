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
			var matrix = Matrix.ScalingMatrix(50f, 50f, 50f) * Matrix.TranslationMatrix3D(1.4f, 1.4f, 0f);
			//var matrix = Matrix.IdentityMatrix(4);
			for (int i = 0; i < cube.vertexbuffer.Count; i++)
			{
				cube.vertexbuffer[i] *= matrix;
			}

			//cube.Translate(new Vector(new float[] { 30, 30 }));
			InitializeComponent();
		}

		private void rotateButton_Click(object sender, EventArgs e)
		{
			// rotate
		}

		private void drawPanel_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			cube.Draw(g, cube.vertexbuffer);
		}
	}
}
