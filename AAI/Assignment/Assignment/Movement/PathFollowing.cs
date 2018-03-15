using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Entity;
using Assignment.World;

namespace Assignment.Movement
{
	class PathFollowing : BaseSteering
	{
		private Arrive arrive = new Arrive { Force = 50 };

		private List<Location> path = null;
		private Location _goal;
		public Location Goal
		{
			get { return _goal; }
			set { _goal = value; path = null; }
		}


		public PathFollowing() : base()
		{
			Priority = 0.8;
		}

		public override SteeringForce Calculate(BaseEntity entity)
		{
			if (Goal == null)
			{
				BehaviorDone = true;
				return new SteeringForce();
			}

			if (path == null)
			{
				path = Pathfinding.AStar(GameWorld.Instance.NavGraph.NearestVertexFromLocation(entity.Location), GameWorld.Instance.NavGraph.NearestVertexFromLocation(Goal));
				if (path == null)
				{
					path = new List<Location> { Goal };
				}
				else
				{
					path.Add(Goal);
					path = Pathfinding.FinePathSmoothing(path);
				}
				arrive.BehaviorDone = true;
			}

			if (arrive.BehaviorDone)
			{
				if (path.Count > 0)
				{
					path.RemoveAt(0);
				}
				if (path.Count == 0)
				{
					path = null;
					BehaviorDone = true;
					return new SteeringForce();
				}
				arrive.ArriveLocation = path.First();
				arrive.BehaviorDone = false;
			}
			if(path.Count > 1)
			{
				arrive.StopDistance = 0;
				arrive.DistanceDone = 20;
				arrive.MaxSpeed = 50;
			}
			else
			{
				arrive.StopDistance = 3;
				arrive.DistanceDone = 5;
				arrive.MaxSpeed = 0.5;
			}

			return arrive.Calculate(entity);
		}

		public override void Render(Graphics g, BaseEntity entity)
		{
			if (path != null && path.Count > 0)
			{
				g.DrawLine(Pens.Purple, (int) entity.Location.X, (int) entity.Location.Y, (int) path[0].X, (int) path[0].Y);
				for (int i = 1; i < path.Count; i++)
				{
					g.DrawLine(Pens.Purple, (int) path[i - 1].X, (int) path[i - 1].Y, (int) path[i].X, (int) path[i].Y);
				}
			}
		}
	}
}
