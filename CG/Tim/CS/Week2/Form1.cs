using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatrixTransformations
{
    public partial class Form1 : Form
    {
        AxisX x_axis;
        AxisY y_axis;
        Square square;

        public Form1()
        {
            InitializeComponent();
            this.Width = 800;
            this.Height = 600;

            x_axis = new AxisX(200);
            y_axis = new AxisY(200);
            square = new Square(Color.Purple,100);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            x_axis.Draw(e.Graphics, x_axis.vb);
            y_axis.Draw(e.Graphics, y_axis.vb);

            square.Draw(e.Graphics, square.vb);
        }


    }
}
