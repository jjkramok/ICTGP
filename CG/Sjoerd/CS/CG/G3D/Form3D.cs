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
			for(int i=0;i<cube.vertexbuffer.Count;i++)
			{
				cube.vertexbuffer[i] *= 100f;
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
