using Assignment.World;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
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

			typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
				   | BindingFlags.Instance | BindingFlags.NonPublic, null,
				   worldPanel1, new object[] { true });

			GameWorld.Instance.Screen = this;

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
			GameWorld.Instance.GameTick();
		}

		public void Render()
		{
			worldPanel1.Invalidate();
		}
	}
}
