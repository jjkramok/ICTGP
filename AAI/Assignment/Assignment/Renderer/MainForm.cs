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
		}

		private void startButton_Click(object sender, EventArgs e)
		{
			GameWorld.DeleteWorld();
			var world = GameWorld.Instance;
		}

		private void worldPanel_Paint(object sender, PaintEventArgs e)
		{
			var renderer = new Rendering(e.Graphics, worldPanel);
			renderer.Render();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			GameWorld.Instance.Entities[0].Direction = GameWorld.Instance.Random.NextDouble() * Math.PI * 2;
			worldPanel.Refresh();
		}
	}
}
