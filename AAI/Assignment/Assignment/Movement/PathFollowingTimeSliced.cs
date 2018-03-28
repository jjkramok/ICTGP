using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Assignment.Entity;
using Assignment.World;
using Assignment.Movement.Planning;

namespace Assignment.Movement
{
    class PathFollowingTimeSliced : BaseSteering
    {
        private Arrive[] arrive = new Arrive[] { new Arrive { Force = 25 }, new Arrive { MaxSpeed = 50, DistanceDone = 15, StopDistance = 0 } };
        private PathPlanner planner;
        private List<Location> path = null;
        private bool RequestOpen;
        private Location _goal;
        public Location Goal
        {
            get { return _goal; }
            set { _goal = value; path = null; }
        }

        public void NotifyOfSearchStatus(SearchStatus status)
        {
            switch (status)
            {
                case SearchStatus.TARGET_FOUND:
                    Console.WriteLine("Retreiving path from PathPlanner.");
                    if (path == null)
                    {
                        path = planner.GetPath();
                    }
                    break;
                case SearchStatus.TARGET_NOT_FOUND:
                    break;
                case SearchStatus.SEARCH_INCOMPLETED:
                    Console.WriteLine("PathPlanner couldn't find path to goal.");
                    break;
            }
        }

        public PathFollowingTimeSliced(Location goal) : base() {
            Goal = goal;
            planner = new PathPlanner(this);
        }

        public override SteeringForce Calculate(BaseEntity entity)
        {
            // Check if the goal is immediatly reachable and for any open pathplanning requests.
            if (!RequestOpen && !Planning.Pathfinding.Walkable(entity.Location, Goal))
            {
                RequestOpen = planner.RequestPathToLocation(entity.Location, Goal);
            } else if (!RequestOpen && Planning.Pathfinding.Walkable(entity.Location, Goal)) {
                path = new List<Location> { Goal };
            }
            
            if (Goal == null)
            {
                BehaviorDone = true;
                return new SteeringForce();
            }

            if (path == null)
            {
                //arrive[0].BehaviorDone = false;
                //arrive[1].BehaviorDone = false;
                return new SteeringForce(); // TODO fix twiddling thumbs
            } else {
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
                arrive[checkBehaviourIndex].ArriveLocation = path.FirstOrDefault();
                arrive[checkBehaviourIndex].BehaviorDone = false;
                return arrive[checkBehaviourIndex].Calculate(entity);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            PathManager.Instance.RequestUnregister(planner);
        }

        public override void Render(Graphics g, BaseEntity entity)
        {
            if (path != null && path.Count > 0)
            {
                g.DrawLine(Pens.Purple, (int)entity.Location.X, (int)entity.Location.Y, (int)path[0].X, (int)path[0].Y);
                for (int i = 1; i < path.Count; i++)
                {
                    g.DrawLine(Pens.Purple, (int)path[i - 1].X, (int)path[i - 1].Y, (int)path[i].X, (int)path[i].Y);
                }
            }
        }
    }
}
