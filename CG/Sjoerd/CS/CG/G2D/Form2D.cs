using GC.G2D;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG.G2D
{
	public partial class Form2D : Form
	{
		private Square square;

		public Form2D()
		{
			square = new Square(Color.Green, 50, 3);
			square.Translate(new Vector(new float[] { 30, 30 }));
			InitializeComponent();
		}

		private void rotateButton_Click(object sender, EventArgs e)
		{
			Matrix rotateMatrix = Matrix.RotationMatrix2D(0.1f * (float) Math.PI);
			// todo apply matrix to square;
		}

		private void drawPanel_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			square.Draw(g);
		}
	}
}
