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
        private Square scaledSquare;
        private Square rotatedSquare;
        private AxisX axisX;
        private AxisY axisY;
        private float sf = 0.1f; // scaling factor
        private float rf = 1f; // rotation factor


        public Form2D()
		{
            // Axes
            axisX = new AxisX(200);
            axisY = new AxisY(200);
            int baseSize = 50;

            // First Square
			square = new Square(Color.Green, baseSize, 3);
            square.Translate(new Vector(new float[] { -baseSize / 2, -baseSize / 2, 1 }));

            // Second Square
            scaledSquare = new Square(Color.Gray, baseSize, 3);
            List<Vector> scaledVectors = new List<Vector>();
            Matrix scale = Matrix.ScalingMatrix2D(1.5f, 1.5f);
            scale = Matrix.TranslationMatrix2D(-baseSize * 3 / 4, -baseSize * 3 / 4) * scale;
            foreach (Vector v in scaledSquare.vb)
            {
                scaledVectors.Add(v * scale);
            }
            scaledSquare.vb = scaledVectors;

            // Third Square
            rotatedSquare = new Square(Color.Orange, baseSize, 3);
            List<Vector> rotatedVectors = new List<Vector>();
            Matrix rotation = Matrix.RotationMatrix2D((float) Math.PI / 9);
            rotation = rotation * Matrix.TranslationMatrix2D(-baseSize / 2, -baseSize / 2);
            foreach (Vector v in rotatedSquare.vb)
            {
                rotatedVectors.Add(v * rotation);
            }
            rotatedSquare.vb = rotatedVectors;

            InitializeComponent();
		}

		private void drawPanel_Paint(object sender, PaintEventArgs e)
		{
            Graphics g = e.Graphics;
            g.TranslateTransform(drawPanel.Width / 2, drawPanel.Height / 2);

            axisX.Draw(g, axisX.vb);
            axisY.Draw(g, axisY.vb);
            square.Draw(g);
            scaledSquare.Draw(g);
            rotatedSquare.Draw(g);
        }

        private void Form2D_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.PageUp:
                    square.Transform(Matrix.RotationMatrix2D((float) (rf * (Math.PI / 180))));
                    break;
                case Keys.PageDown:
                    square.Transform(Matrix.RotationMatrix2D((float)(-rf * (Math.PI / 180))));
                    break;
                case Keys.Oemplus:
                case Keys.Add:
                    square.Transform(Matrix.ScalingMatrix2D(1.0f + 0.1f * sf, 1.0f + 0.1f * sf));
                    break;
                case Keys.OemMinus:
                case Keys.Subtract:
                    square.Transform(Matrix.ScalingMatrix2D(1.0f - 0.1f * sf, 1.0f - 0.1f * sf));
                    break;
                default:
                    break;
            }
            this.Refresh();
        }
    }
}
