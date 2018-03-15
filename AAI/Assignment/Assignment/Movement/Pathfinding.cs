using System;
using System.Collections.Generic;
using Assignment.World;
using Assignment.Utilities;

namespace Assignment.Movement
{
    static class Pathfinding
    {
		public static bool PathSmoothing = true;

        public static List<Location> AStar(Graph.Vertex start, Graph.Vertex goal)
        {
            Graph navigationGraph = GameWorld.Instance.NavGraph;
            PriorityQueue<Graph.Vertex> priorityQueue = new PriorityQueue<Graph.Vertex>();

            // Initialize graph
            foreach(Graph.Vertex vertex in navigationGraph.vertices)
            {
                if (vertex != null)
                {
                    vertex.Distance = Double.MaxValue;
                    vertex.HeuristicDistance = Double.MaxValue;
                    vertex.Known = false;
                }
            }

            // Add start node to queue
            priorityQueue.Add(start);
            start.Distance = 0;
            start.HeuristicDistance = Heuristic(start, goal);
            start.Previous = null;

            while (!priorityQueue.IsEmpty)
            {   
                Graph.Vertex vertex = priorityQueue.Get();

				if (vertex.Known)
				{
					continue;
				}

				vertex.Known = true;

                // End-case : goal found, retrieve path to goal node.
                if (vertex == goal)
                {
                    return reconstructPath(goal);
                }

                foreach (Graph.Edge edge in vertex.Adjacent)
                {
                    Graph.Vertex w = edge.Dest;
                    if (w.Known)
                    {
                        continue; // Already evaluated
                    }
                    w.HeuristicDistance = vertex.Distance + edge.Cost + Heuristic(w, goal);
                    priorityQueue.Add(w); // Newly discovered node
                    
                    // Calculate distance from start till current vertex
                    double tentative_dist = vertex.Distance + edge.Cost;

                    if (w.Distance > tentative_dist)
                    {
						// Current best path
						w.Previous = vertex;
						w.Distance = tentative_dist;
					}
                }
            }
            return null; // goal not reached
        }

        // Heuristic for A* based on euclidian distance.
        private static double Heuristic(Graph.Vertex from, Graph.Vertex to)
        {
            // Determine which heuristic is to be used based on the fact that diagonal edges are used or not.
            if (GameWorld.Instance.NavGraph.DiagonalEdgesCost >= 0)
            {
                return Utility.Distance(from.Location, to.Location); // euclidian distance
            }
            else
			{
                return Math.Abs(from.Location.X - to.Location.X) + Math.Abs(from.Location.Y - to.Location.Y); // cardinal / manhattan distance
            }
        }

        // Reconstructs a path from graph explored by A*
        private static List<Location> reconstructPath(Graph.Vertex goal)
        {
            List<Location> path = new List<Location>();
            Graph nav = GameWorld.Instance.NavGraph;

            Graph.Vertex curr = goal;
            path.Insert(0, curr.Location);
            while ((curr = curr.Previous) != null)
            {
                path.Insert(0, curr.Location);
            }
            return path;
        }

        public static List<Location> RoughPathSmoothing(List<Location> path)
        {
            Console.WriteLine("RoughPathSmoothing not implemented -> calling FinePathSmoothing instead");
            return FinePathSmoothing(path);
        }

        public static List<Location> FinePathSmoothing(List<Location> path)
        {
			if (!PathSmoothing)
			{
				return path;
			}

			if (path == null)
            {
                return null;
            }

            // Using Erik's suggestion - Whilst smoothing always start checking from the goal node and move down towards the start node and return the first viable smoothing option.
            List<Location> smoothedPath = new List<Location>();
            int i = 0;
            Location start = path[i];
            Location eval; // Node currently being evaluated by the algorithm
            bool smoothed = false;
            
            while (true)
            {
                smoothed = false;
                for (int j = path.Count - 1; j > i + 1; j--)
                {
                    eval = path[j];
                    if (Walkable(start, eval))
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
            double AtoBDistance = Utility.Distance(a, b);
            double AtoBAngle = Utility.Direction(a, b);

            var boundBoxWidth = Settings.Instance.EntitySize; // Width / radius of the agent
            // ^-- might need to be passed to the pathfinder function or possible to be retreived by it
            if (a.X == 101 && a.Y == 51)
            {
                Math.Sin(0);
            }

            Location currEval;
            List<Obstacle.ObstacleCircle> obstacles;
            int step = 5; // distance between each sample point on the line from A to B. Lower distance means more precision.
            for (int d = 0; d <= AtoBDistance; d += step)
            {
                // Create the current point to be evaluated. It is 'd' distance away from A among the AtoB line.
                currEval = new Location(a.X + d * Math.Cos(AtoBAngle), a.Y + d * Math.Sin(AtoBAngle));
                obstacles = GameWorld.Instance.ObstaclesInArea(currEval, boundBoxWidth, true);
                
                if (obstacles.Count > 0)
                {
                    return false;
                }
            }
            // Check the very last point (b, or destination if you will) to ensure that there is enough space for the entity.
            //obstacles = GameWorld.Instance.ObstaclesInArea(b, boundBoxWidth);

            //if (obstacles.Count > 0)
            //{
            //    return false;
            //}
            return true;
        }

    }
}
