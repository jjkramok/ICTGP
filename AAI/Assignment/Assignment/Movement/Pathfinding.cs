using System;
using System.Collections.Generic;
using Assignment.World;
using System.Windows.Media;
namespace Assignment.Movement
{
    static class Pathfinding
    {
        private static long flops = 0;

        public static List<Graph.Vertex> AStar(Graph.Vertex start, Graph.Vertex goal)
        {
            Graph nav = GameWorld.Instance.NavGraph;
            Utilities.PriorityQueue<Graph.Vertex> pq = new Utilities.PriorityQueue<Graph.Vertex>();

            // Initialize graph
            foreach(Graph.Vertex v in nav.vertices)
            {
                if (v != null)
                {
                    v.Dist = Double.MaxValue;
                    v.HDist = Double.MaxValue;
                    v.Known = false;
                }
            }

            // Add start node to queue
            pq.Add(start);
            start.Dist = 0;
            start.HDist = h(start, goal);
            start.Prev = null;

            while (!pq.IsEmpty)
            {
                flops++;
                
                Graph.Vertex v = pq.Get();
                if (flops > 10000)
                {
                    flops = 0;
                    return reconstructPath(v);
                }
                v.Known = true;

                // End-case : goal found, retrieve path to goal node.
                if (v == goal)
                {
                    flops = 0;
                    return reconstructPath(goal);
                }

                foreach (Graph.Edge e in v.Adj)
                {
                    Graph.Vertex w = e.Dest;
                    if (w.Known)
                    {
                        continue; // Already evaluated
                    }
                    w.HDist = v.Dist + e.Cost + h(w, goal);
                    pq.Add(w); // Newly discovered node
                    
                    // Calculate distance from start till current vertex
                    double tentative_dist = v.Dist + e.Cost;

                    if (w.Dist < tentative_dist)
                    {
                        continue; // Previous path was better
                    }

                    // Current best path
                    w.Prev = v;
                    w.Dist = tentative_dist;
                }
            }
            return null; // goal not reached
        }

        // Heuristic for A* based on euclidian distance.
        private static double h(Graph.Vertex v, Graph.Vertex w)
        {
            // Determine which heuristic is to be used based on the fact that diagonal edges are used or not.
            if (GameWorld.Instance.NavGraph.DiagonalEdgesCost >= 0)
            {
                return Utilities.Utilities.Distance(v.Loc, w.Loc); // euclidian distance
            }
            else {
                return Math.Abs(v.Loc.X - w.Loc.X) + Math.Abs(v.Loc.Y - w.Loc.Y); // cardinal / manhattan distance
            }
        }

        // Reconstructs a path from graph explored by A*
        private static List<Graph.Vertex> reconstructPath(Graph.Vertex goal)
        {
            List<Graph.Vertex> path = new List<Graph.Vertex>();
            Graph nav = GameWorld.Instance.NavGraph;

            Graph.Vertex curr = goal;
            path.Insert(0, curr);
            while ((curr = curr.Prev) != null)
            {
                path.Insert(0, curr);
            }

            //Console.WriteLine(String.Join(" -> ", path));
            return path;
        }

        public static List<Graph.Vertex> RoughPathSmoothing(List<Graph.Vertex> path)
        {
            Console.WriteLine("RoughPathSmoothing not implemented -> calling FinePathSmoothing instead");
            return FinePathSmoothing(path);
        }

        public static List<Graph.Vertex> FinePathSmoothing(List<Graph.Vertex> path)
        {
            // Using Erik's suggestion - Whilst smoothing always start checking from the goal node and move down towards the start node and return the first viable smoothing option.
            List<Graph.Vertex> smoothedPath = new List<Graph.Vertex>();
            Graph.Vertex start = path[0];
            Graph.Vertex eval; // Node currently being evaluated by the algorithm

            smoothedPath.Add(start); // Add start node since it cannot be smoothed anyway.
            for (int i = path.Count - 1; i > 1; i--)
            {
                // Call walkable and return first solution found for each step
            }

            return smoothedPath;
        }

        /// <summary>
        /// Helper function of pathsmoothing.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static bool Walkable(Location a, Location b)
        {
            // TODO call obstacles in area
            // Check for all obstacles if they intersect with the rectangle formed by the four 'sidepoints'
            double AtoBDistance = Utilities.Utilities.Distance(a, b);
            double AtoBAngle = Utilities.Utilities.Direction(a, b);

            //var offset = Math.Sin(AtoBAngle) * AtoBDistance; // y distance from a to b (not used in my case)

            var boundBoxWidth = 1; // Space / distance between the two sidepoints making up the rectangle bounding box.
            var sidepointAngle = (Math.PI / 2) - AtoBAngle; // Angle between origin and sidepoint, used to calculate location of the sidepoint below.
            Location aSidePoint1 = new Location(a.X + boundBoxWidth * Math.Cos(sidepointAngle), a.Y + boundBoxWidth * Math.Sin(sidepointAngle));
            Location aSidePoint2 = new Location(a.X + boundBoxWidth * Math.Cos(sidepointAngle + Math.PI / 2), a.Y + boundBoxWidth * Math.Sin(sidepointAngle + Math.PI / 2));

            Location bSidePoint1 = new Location(b.X + boundBoxWidth * Math.Cos(sidepointAngle), b.Y + boundBoxWidth * Math.Sin(sidepointAngle));
            Location bSidePoint2 = new Location(b.X + boundBoxWidth * Math.Cos(sidepointAngle + Math.PI / 2), b.Y + boundBoxWidth * Math.Sin(sidepointAngle + Math.PI / 2));

            
            /*
            while (angleDiff > Math.PI)
                angleDiff -= Math.PI * 2;

            while (angleDiff < -Math.PI)
                angleDiff += Math.PI * 2;

            // ignore obstacles behind the entity
            if (Math.Abs(angleDiff) < Math.PI / 2)
            {
                if (Math.Abs(offset) < offsetMargin + obstacle.Radius)
                {
                    var amountToSteer = obstacle.Radius - Math.Abs(offset) + offsetMargin;
                    var steeringNeed = AtoBDistance;

                    if (offset < 0)
                    {
                        force += new SteeringForce(entity.Direction + Math.PI / 2, avoidanceFactor / steeringNeed * amountToSteer);
                        forceCounterL++;
                    }
                    else
                    {
                        force += new SteeringForce(entity.Direction - Math.PI / 2, avoidanceFactor / steeringNeed * amountToSteer);
                        forceCounterR++;
                    }
                    totalForce += force.Amount;
                }
            }


            var angle = Utilities.Utilities.Direction(a, b);
            Geometry boundBox = new RectangleGeometry(new System.Windows.Rect(50, 50, 50, 50));
            CombinedGeometry collision = new CombinedGeometry();
            */
            return false;
        }

    }
}
