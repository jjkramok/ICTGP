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
        public static List<Graph.Vertex> AStar(Graph.Vertex start, Graph.Vertex goal)
        {
            Graph nav = GameWorld.Instance.NavGraph;
            Utilities.PriorityQueue<Graph.Vertex> pq = new Utilities.PriorityQueue<Graph.Vertex>();

            // Initialize graph
            foreach(Graph.Vertex v in nav.vertices)
            {
                v.Dist = Double.MaxValue;
                v.HDist = Double.MaxValue;
                v.Known = false;
            }

            // Add start node to queue
            pq.Add(start);
            start.Dist = 0;
            start.HDist = h(start, goal);
            start.Prev = null;

            while (!pq.IsEmpty)
            {
                Graph.Vertex v = pq.Get();
                v.Known = true;

                // End-case : goal found, retrieve path to goal node.
                if (v == goal)
                {
                    return reconstructPath(goal);
                }

                foreach (Graph.Edge e in v.Adj)
                {
                    Graph.Vertex w = e.Dest;
                    if (w.Known)
                    {
                        continue; // Already evaluated
                    }
                    if (!w.Known)
                    {
                        pq.Add(w); // Newly discovered node
                    }

                    // Calculate distance from start till current vertex
                    double tentative_dist = v.Dist + e.Cost;

                    if (w.Dist < tentative_dist)
                    {
                        continue; // Previous path was better
                    }

                    // Current best path
                    w.Prev = v;
                    w.Dist = tentative_dist;
                    w.HDist = w.Dist + h(w, v);
                }
            }
            return null; // goal not reached
        }

        // Heuristic for A* based on euclidian distance.
        private static double h(Graph.Vertex v, Graph.Vertex w)
        {
            return Utilities.Utilities.Distance(v.Loc, w.Loc);
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
            Console.WriteLine("Hallo ");
            return path;
        }

    }
}
