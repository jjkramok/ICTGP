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

            

            List<Graph.Vertex> path = new List<Graph.Vertex>();

            return path;
        }

       

        // Heuristic based on the euclidean distance between current vertex and goal vertex
        private static double heuristic(Location loc1, Location loc2)
        {
            double a = loc1.X - loc2.X;
            double b = loc1.Y - loc2.Y;
            return Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
        }
    }
}
