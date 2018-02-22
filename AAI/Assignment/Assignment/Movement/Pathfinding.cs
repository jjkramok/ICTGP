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

    }
}
