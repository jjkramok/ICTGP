using Assignment.Entity;
using Assignment.Movement;
using Assignment.Movement.Planning;
using Assignment.State;
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
		public MainForm()
		{
			InitializeComponent();

			// enable double buffering, for some reason this property is not public in panels
			typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
				   | BindingFlags.Instance | BindingFlags.NonPublic, null,
				   worldPanel, new object[] { true });

			GameWorld.Instance.Screen = this;
			SetCheckedCalculationType();
		}
	
		private void worldPanel_Paint(object sender, PaintEventArgs e)
		{
			Rendering.Render(e.Graphics, worldPanel);
		}

		public void Render()
		{
			worldPanel.Invalidate();
		}

		#region MenuClickHandlers
		private void restartToolStripMenuItem_Click(object sender, EventArgs e)
		{
			GameWorld.DeleteWorld();
			var world = GameWorld.Instance;
			world.Screen = this;
			SetCheckedCalculationType();
		}

		private void navigationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Rendering.RenderNavGraphOption = navigationToolStripMenuItem.Checked;
		}

		private void entityInfoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Rendering.RenderEntitiesInfoOption = entityInfoToolStripMenuItem.Checked;
		}

		private void entityForceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Rendering.RenderEntitiesForcesOption = entityForceToolStripMenuItem.Checked;
		}

		private void spatialPartitioningGridToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Rendering.RenderGridOption = spatialPartitioningGridToolStripMenuItem.Checked;
		}

		private void enableStatesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Enable or Disable all behaviour buttons.
			arriveToolStripMenuItem.Enabled = !enableStatesToolStripMenuItem.Checked;
			fleeToolStripMenuItem.Enabled = !enableStatesToolStripMenuItem.Checked;
			flockingToolStripMenuItem.Enabled = !enableStatesToolStripMenuItem.Checked;
			obstacleAvoidanceToolStripMenuItem.Enabled = !enableStatesToolStripMenuItem.Checked;
			pathFollowingToolStripMenuItem.Enabled = !enableStatesToolStripMenuItem.Checked;
			seekToolStripMenuItem.Enabled = !enableStatesToolStripMenuItem.Checked;
			wanderToolStripMenuItem.Enabled = !enableStatesToolStripMenuItem.Checked;

			// Uncheck all behaviour buttons.
			arriveToolStripMenuItem.Checked = false;
			fleeToolStripMenuItem.Checked = false;
			flockingToolStripMenuItem.Checked = false;
			obstacleAvoidanceToolStripMenuItem.Checked = false;
			pathFollowingToolStripMenuItem.Checked = false;
			seekToolStripMenuItem.Checked = false;
			wanderToolStripMenuItem.Checked = false;

			foreach (var entity in GameWorld.Instance.Entities)
			{
				if (enableStatesToolStripMenuItem.Checked)
				{
					entity.RemoveAllBehaviours();
					entity.State = entity.PreviousState;
					entity.PreviousState = "";
				}
				else
				{
					entity.RemoveAllBehaviours();
					entity.PreviousState = entity.State;
					entity.State = "DEBUGSTATE";
				}
			}
		}

		private void arriveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var world = GameWorld.Instance;
			foreach (var entity in world.Entities)
			{
				if (arriveToolStripMenuItem.Checked)
				{
					var behaviour = new Arrive();
					behaviour.ArriveLocation = new Location(world.Random.Next(5, (int) world.Width - 5),	world.Random.Next(5, (int) world.Height - 5));

					entity.AddBehaviour(behaviour);
				}
				else
				{
					entity.RemoveBehaviour(typeof(Arrive));
				}
			}
		}

		private void fleeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (var entity in GameWorld.Instance.Entities)
			{
				if (fleeToolStripMenuItem.Checked)
				{
					var behaviour = new Flee();
					if(entity.Type == EntityType.Omnivore)
						behaviour.FleeFrom = EntityType.Herbivore;
					else
						behaviour.FleeFrom = EntityType.Omnivore;

					behaviour.Radius = 150;

					entity.AddBehaviour(behaviour);
				}
				else
				{
					entity.RemoveBehaviour(typeof(Flee));
				}
			}
		}

		private void flockingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (var entity in GameWorld.Instance.Entities)
			{
				if (flockingToolStripMenuItem.Checked)
				{
					entity.AddBehaviour(new Flocking());
				}
				else
				{
					entity.RemoveBehaviour(typeof(Flocking));
				}
			}
		}

		private void obstacleAvoidanceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (var entity in GameWorld.Instance.Entities)
			{
				if (obstacleAvoidanceToolStripMenuItem.Checked)
				{
					entity.AddBehaviour(new ObstacleAvoidance());
				}
				else
				{
					entity.RemoveBehaviour(typeof(ObstacleAvoidance));
				}
			}
		}

		private void pathFollowingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var world = GameWorld.Instance;
			foreach (var entity in world.Entities)
			{
				if (pathFollowingToolStripMenuItem.Checked)
				{
					var behaviour = new PathFollowing();
					behaviour.Goal = new Location(world.Random.Next(5, (int) world.Width - 5), world.Random.Next(5, (int) world.Height - 5));

					entity.AddBehaviour(behaviour);
				}
				else
				{
					entity.RemoveBehaviour(typeof(PathFollowing));
				}
			}
		}

		private void seekToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var world = GameWorld.Instance;
			foreach (var entity in world.Entities)
			{
				if (seekToolStripMenuItem.Checked)
				{
					var behaviour = new Seek();
					behaviour.ChaseEntity = world.Entities[world.Random.Next(0, world.Entities.Count - 1)];
					behaviour.MaxDistance = 300;

					entity.AddBehaviour(behaviour);
				}
				else
				{
					entity.RemoveBehaviour(typeof(Seek));
				}
			}
		}

		private void wanderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (var entity in GameWorld.Instance.Entities)
			{
				if (wanderToolStripMenuItem.Checked)
				{
					entity.AddBehaviour(new Wander());
				}
				else
				{
					entity.RemoveBehaviour(typeof(Wander));
				}
			}
		}

		private void ditheringToolStripMenuItem_Click(object sender, EventArgs e)
		{
			GameWorld.Instance.SteeringForceCalculationType = SteeringForceCalculationType.Dithering;
			SetCheckedCalculationType();
		}

		private void truncatedSumToolStripMenuItem_Click(object sender, EventArgs e)
		{
			GameWorld.Instance.SteeringForceCalculationType = SteeringForceCalculationType.WeightedTruncatedSum;
			SetCheckedCalculationType();
		}

		private void prioToolStripMenuItem_Click(object sender, EventArgs e)
		{
			GameWorld.Instance.SteeringForceCalculationType = SteeringForceCalculationType.Priorization;
			SetCheckedCalculationType();
		}

		private void SetCheckedCalculationType()
		{
			truncatedSumToolStripMenuItem.Checked = false;
			prioToolStripMenuItem.Checked = false;
			ditheringToolStripMenuItem.Checked = false;

			switch (GameWorld.Instance.SteeringForceCalculationType)
			{
				case SteeringForceCalculationType.Dithering:
					ditheringToolStripMenuItem.Checked = true;
					break;
				case SteeringForceCalculationType.Priorization:
					prioToolStripMenuItem.Checked = true;
					break;
				case SteeringForceCalculationType.WeightedTruncatedSum:
					truncatedSumToolStripMenuItem.Checked = true;
					break;
			}
		}

		private void pathSmoothingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Pathfinding.PathSmoothing = pathSmoothingToolStripMenuItem.Checked;
		}

		#endregion

		private void worldPanel_Click(object sender, EventArgs e)
		{
			var clickEvent = (MouseEventArgs) e; 
			var world = GameWorld.Instance;
			var goal = new Location((double)clickEvent.X / worldPanel.Width * world.Width, (double)clickEvent.Y / worldPanel.Height * world.Width);

			foreach(var entity in world.Entities)
			{
				entity.RemoveBehaviour(typeof(PathFollowing));
				entity.AddBehaviour(new PathFollowing { Goal = goal });
			}
		}
	}
}
