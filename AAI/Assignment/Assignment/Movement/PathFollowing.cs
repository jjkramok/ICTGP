using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Assignment.Entity;
using Assignment.World;

namespace Assignment.Movement
{
	class PathFollowing : BaseSteering
	{
		private Arrive[] arrive = new Arrive[] { new Arrive { Force = 25 }, new Arrive { MaxSpeed = 50, DistanceDone = 15, StopDistance = 0 } };

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
                if (Pathfinding.Walkable(entity.Location, Goal))
                {
                    path = new List<Location> { Goal };
                } else
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
                }
				arrive[0].BehaviorDone = false;
				arrive[1].BehaviorDone = false;
			}

			int checkBehaviourIndex = path.Count >= 2 ? 1 : 0;

			if (arrive[0].BehaviorDone || arrive[1].BehaviorDone)
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
			}
			arrive[checkBehaviourIndex].ArriveLocation = path.First();
			arrive[checkBehaviourIndex].BehaviorDone = false;
			return arrive[checkBehaviourIndex].Calculate(entity);
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
