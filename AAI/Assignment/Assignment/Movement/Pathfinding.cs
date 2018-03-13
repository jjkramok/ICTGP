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
					Console.WriteLine($"fail: {v}");
					flops = 0;
                    return reconstructPath(v);
                }

				if (v.Known)
				{
					continue;
				}

				v.Known = true;

                // End-case : goal found, retrieve path to goal node.
                if (v == goal)
                {
					Console.WriteLine($"x{v.Loc.X}, y{v.Loc.Y}");
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

                    if (w.Dist > tentative_dist)
                    {
						// Current best path
						w.Prev = v;
						w.Dist = tentative_dist;
					}
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
            int i = 0;
            Graph.Vertex start = path[i];
            Graph.Vertex eval; // Node currently being evaluated by the algorithm
            bool smoothed = false;
            
            while (true)
            {
                smoothed = false;
                for (int j = path.Count - 1; j > i + 1; j--)
                {
                    eval = path[j];
                    if (Walkable(start.Loc, eval.Loc))
                    {
                        // We found the best smoothing for our two nodes, save it.
                        smoothedPath.Add(start);
                        smoothedPath.Add(eval);
                        start = eval;
                        i = j;
                        smoothed = true;
                        break;
                    }
                }

                // Move to next node to avoid evaluating the same start node
                if (!smoothed)
                {
                    i++;
                    // end-case
                    if (i >= path.Count - 1)
                    {
                        break;
                    }
                    start = path[i];
                }
            }

            return smoothedPath;
        }

        /// <summary>
        /// Helper function of pathsmoothing.
        /// Determines if an entity (see: boundBoxWidth) can travel across a line from A to B without colliding with obstacles.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Walkable(Location a, Location b)
        {
            double AtoBDistance = Utilities.Utilities.Distance(a, b);
            double AtoBAngle = Utilities.Utilities.Direction(a, b);

            var boundBoxWidth = 10; // Width / radius of the agent
            // ^-- might need to be passed to the pathfinder function or possible to be retreived by it

            int step = 5; // distance between each sample point on the line from A to B. Lower distance means more precision.
            for (int d = 0; d < AtoBDistance; d += step)
            {
                // Create the current point to be evaluated. It is 'd' distance away from A among the AtoB line.
                Location currEval = new Location(a.X + d * Math.Cos(AtoBAngle), a.Y + d * Math.Sin(AtoBAngle));
                var obstacles = GameWorld.Instance.ObstaclesInArea(currEval, boundBoxWidth);
                
                if (obstacles.Count > 0)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
