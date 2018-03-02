using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.World;

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

            //Console.WriteLine(String.Join(" ->\n ", path));
            return path;
        }

    }
}
