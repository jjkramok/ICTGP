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
		private Arrive arrive = new Arrive();

		private List<Graph.Vertex> path = null;
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
				return new SteeringForce();

			if (path == null)
			{
				path = Pathfinding.AStar(GameWorld.Instance.NavGraph.NearestVertexFromLocation(entity.Location), GameWorld.Instance.NavGraph.NearestVertexFromLocation(Goal));
                path = Pathfinding.FinePathSmoothing(path);

                if (path == null || path.Count == 0)
                {
                    BehaviorDone = true;
                    return new SteeringForce();
                }
				arrive.BehaviorDone = true;
			}

			if (arrive.BehaviorDone)
			{
                if (path.Count < 1) {}
				path.RemoveAt(0);
				if (path.Count == 0)
				{
					path = null;
					BehaviorDone = true;
					return new SteeringForce();
				}
				arrive.ArriveLocation = path.First().Loc;
				arrive.BehaviorDone = false;
			}
			return arrive.Calculate(entity);
		}

		public override void Render(Graphics g, BaseEntity entity)
		{
			if (path != null && path.Count > 0)
			{
                g.DrawLine(Pens.Purple, (int)entity.Location.X, (int)entity.Location.Y, (int)path[0].Loc.X, (int)path[0].Loc.Y);
                for (int i = 1; i < path.Count; i++)
				{
					g.DrawLine(Pens.Purple, (int) path[i - 1].Loc.X, (int) path[i - 1].Loc.Y, (int) path[i].Loc.X, (int) path[i].Loc.Y);
				}
			}
		}
	}
}
