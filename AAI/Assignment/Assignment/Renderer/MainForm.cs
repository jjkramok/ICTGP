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
		public MainForm()
		{
			InitializeComponent();
			GameWorld.Instance.Screen = this;
		}

		private void startButton_Click(object sender, EventArgs e)
		{
			GameWorld.DeleteWorld();
			var world = GameWorld.Instance;
			world.Screen = this;

			Timer timer = new Timer();
			timer.Interval = (GameWorld.TickDelay);
			timer.Tick += new EventHandler(world.GameTick);
			timer.Start();

		}

		private void worldPanel_Paint(object sender, PaintEventArgs e)
		{
			var renderer = new Rendering(e.Graphics, worldPanel);
			renderer.Render();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			//GameWorld.Instance.Entities[0].Direction = GameWorld.Instance.Random.NextDouble() * Math.PI * 2;
			//infoLabel.Text = $"direction= {GameWorld.Instance.Entities[0].Direction / Math.PI * 180}";
			GameWorld.Instance.UpdateEntites();

			worldPanel.Refresh();
		}
	}
}
