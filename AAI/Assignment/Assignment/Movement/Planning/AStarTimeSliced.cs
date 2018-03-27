using System;
using System.Collections.Generic;
using Assignment.Utilities;
using Assignment.World;

namespace Assignment.Movement.Planning
{
    class AStarTimeSliced : ISearchTimeSliced
    {
        private Graph.Vertex Start, Goal;
        private Graph NavigationGraph;
        private bool GoalAlreadyReached = false;
        private PriorityQueue<Graph.Vertex> OpenSet; // Set of all discovered nodes.
        private HashSet<Graph.Vertex> ClosedSet; // Set of all known nodes.
        private Dictionary<Graph.Vertex, Graph.Vertex> CameFrom; // Previous vertex of key node.
        private Dictionary<Graph.Vertex, double> Costs; // Cost to visit key node.
        private Dictionary<Graph.Vertex, double> HCosts; // Heuristic cost to visit key node.

        public AStarTimeSliced(Graph.Vertex start, Graph.Vertex goal)
        {
            NavigationGraph = GameWorld.Instance.NavGraph;
            OpenSet = new PriorityQueue<World.Graph.Vertex>();
            OpenSet.Add(Start);
            ClosedSet = new HashSet<Graph.Vertex>();
            CameFrom = new Dictionary<Graph.Vertex, Graph.Vertex>();
            Costs = new Dictionary<Graph.Vertex, double>();
            HCosts = new Dictionary<Graph.Vertex, double>();

            // Initialize graph
            foreach (Graph.Vertex vertex in NavigationGraph.vertices)
            {
                Costs.Add(vertex, Double.MaxValue);
                HCosts.Add(vertex, Double.MaxValue);
            }

            // Add start node to queue
            Costs[Start] = 0;
            HCosts[Start] = Heuristic(Start, Goal);
        }

        public SearchStatus CycleOnce()
        {
            if (!OpenSet.IsEmpty)
            {
                Graph.Vertex vertex = OpenSet.Get();

                if (ClosedSet.Contains(vertex))
                {
                    return SearchStatus.TARGET_NOT_FOUND; // Node already evaluated. TODO Maybe call function again since we didn't really do anything this cycle?
                }

                if (Costs[vertex] == -1 || HCosts[vertex] == -1)
                {
                    // TODO remove this if clause
                    Console.WriteLine("Vertex {0} has negative distance!", vertex);
                }

                ClosedSet.Add(vertex);

                // End-case : goal found, tell our handler that the goal has been reached.
                if (vertex == Goal)
                {
                    GoalAlreadyReached = true;
                    return SearchStatus.TARGET_FOUND;
                }

                foreach (Graph.Edge edge in vertex.Adjacent)
                {
                    Graph.Vertex w = edge.Dest;
                    if (ClosedSet.Contains(w))
                    {
                        continue; // Already evaluated
                    }
                    HCosts[w] = Costs[vertex] + edge.Cost + Heuristic(w, Goal);
                    OpenSet.Add(w); // Newly discovered node

                    // Calculate distance from start till current vertex
                    double tentative_dist = Costs[vertex] + edge.Cost;

                    if (Costs[w] > tentative_dist)
                    {
                        // Current best path
                        CameFrom[w] = vertex;
                        Costs[w] = tentative_dist;
                    }
                }
            }
            if (GoalAlreadyReached) {
                return SearchStatus.TARGET_FOUND;
            } else {
                return SearchStatus.TARGET_NOT_FOUND;
            }
        }

        // Heuristic for A* based on euclidian distance.
        private static double Heuristic(Graph.Vertex from, Graph.Vertex to)
        {
            // Determine which heuristic is to be used based on the fact that diagonal edges are used or not.
            if (GameWorld.Instance.NavGraph.DiagonalEdgesCost >= 0)
            {
                return Utility.Distance(from.Location, to.Location); // Pythagoras / euclidian distance.
            }
            else
            {
                return Math.Abs(from.Location.X - to.Location.X) + Math.Abs(from.Location.Y - to.Location.Y); // Cardinal / manhattan distance.
            }
        }

        public double CostToTarget()
        {
            return Costs[Goal];
        }

        public double HeuristicCostToTarget()
        {
            return HCosts[Goal];
        }

        public List<Location> GetPath()
        {
            return reconstructPath(Goal);
        }

        // Reconstructs a path from graph explored by Time-Sliced A*
        private List<Location> reconstructPath(Graph.Vertex goal)
        {
            List<Location> path = new List<Location>();
            Graph nav = GameWorld.Instance.NavGraph;

            Graph.Vertex curr = goal;
            path.Insert(0, curr.Location);
            while ((curr = CameFrom[curr]) != null)
            {
                path.Insert(0, curr.Location);
            }
            return path;
        }

        public List<Graph.Edge> GetSPT()
        {
            throw new NotImplementedException();
        }
    }
}
