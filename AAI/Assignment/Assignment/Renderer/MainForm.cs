using Assignment.World;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment.Renderer
{
	public partial class MainForm : Form
	{
		private bool panel1Active = true;

		public MainForm()
		{
			InitializeComponent();
			GameWorld.Instance.Screen = this;
			worldPanel2.Visible = false;
		}

		private void startButton_Click(object sender, EventArgs e)
		{
			GameWorld.DeleteWorld();
			var world = GameWorld.Instance;
			world.Screen = this;
		}

		private void worldPanel1_Paint(object sender, PaintEventArgs e)
		{

			var renderer = new Rendering(e.Graphics, worldPanel1);
			renderer.Render();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			GameWorld.Instance.UpdateEntites();

			worldPanel1.Refresh();
		}

		private void worldPanel2_Paint(object sender, PaintEventArgs e)
		{
			var renderer = new Rendering(e.Graphics, worldPanel2);
			renderer.Render();
		}


		public void Render()
		{
			worldPanel1.Invalidate();
			return;

			if (panel1Active)
			{
				worldPanel2.SendToBack();

				worldPanel2.Invalidate();
			}
			else
			{
				worldPanel1.SendToBack();

				worldPanel1.Invalidate();
			}

			panel1Active = !panel1Active;
		}
	}
}
