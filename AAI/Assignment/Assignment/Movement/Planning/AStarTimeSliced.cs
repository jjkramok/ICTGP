using System;
using System.Collections.Generic;
using System.Drawing;
using Assignment.Utilities;
using Assignment.World;

namespace Assignment.Movement.Planning
{
    class AStarTimeSliced : ISearchTimeSliced
    {
        private Graph.Vertex Start, Goal;
        private Graph NavigationGraph;
        private bool GoalAlreadyReached = false;
        private PriorityQueue<PriorityVertex> OpenSet; // Set of all discovered nodes.
        private HashSet<Graph.Vertex> ClosedSet; // Set of all known nodes.
        private Dictionary<Graph.Vertex, Graph.Vertex> CameFrom; // Previous vertex of key node.
        private Dictionary<Graph.Vertex, double> Costs; // Cost to visit key node.
        private Dictionary<Graph.Vertex, double> HCosts; // Heuristic cost to visit key node.

        // Class used for the OpenSet
        private class PriorityVertex : IComparable<PriorityVertex>
        {
            internal Graph.Vertex Vertex;
            internal double Priority;

            internal PriorityVertex(Graph.Vertex v, double prio)
            {
                Vertex = v;
                Priority = prio;
            }

            public int CompareTo(PriorityVertex other)
            {
                return (int)(this.Priority - other.Priority);
            }
        }

        public AStarTimeSliced(Graph.Vertex start, Graph.Vertex goal)
        {
            Start = start;
            Goal = goal;
            NavigationGraph = GameWorld.Instance.NavGraph;
            OpenSet = new PriorityQueue<PriorityVertex>();
            ClosedSet = new HashSet<Graph.Vertex>();
            CameFrom = new Dictionary<Graph.Vertex, Graph.Vertex>();
            Costs = new Dictionary<Graph.Vertex, double>();
            HCosts = new Dictionary<Graph.Vertex, double>();

            // Initialize graph
            //foreach (Graph.Vertex vertex in NavigationGraph.vertices)
            //{
            //    if (vertex != null) {
            //        Costs.Add(vertex, Double.MaxValue);
            //        HCosts.Add(vertex, Double.MaxValue);
            //    }
            //}

            // Add start node to queue
            Costs[Start] = 0;
            HCosts[Start] = Heuristic(Start, Goal);
            OpenSet.Add(new PriorityVertex(Start, Costs[Start]));
        }

        public SearchStatus CycleOnce()
        {
            if (!OpenSet.IsEmpty)
            {
                Graph.Vertex vertex = OpenSet.Get().Vertex;

                // End-case : goal found, tell our handler that the goal has been reached.
                if (vertex == Goal)
                {
                    GoalAlreadyReached = true;
                    return SearchStatus.TARGET_FOUND;
                }

                ClosedSet.Add(vertex);

                foreach (Graph.Edge edge in vertex.Adjacent)
                {
                    Graph.Vertex neighbour = edge.Dest;
                    if (ClosedSet.Contains(neighbour))
                    {
                        continue; // Already evaluated
                    }

                    HCosts[neighbour] = CostToReach(vertex) + edge.Cost + Heuristic(neighbour, Goal);
                    OpenSet.Add(new PriorityVertex(neighbour, HCosts[neighbour])); // Newly discovered node

                    // Calculate distance from start till neighbour vertex
                    double tentative_dist = CostToReach(vertex) + edge.Cost;

                    // First time for each vertex this should be a check against inifinity, but we already set the score above.
                    // Cost to reach never set, assumed infinite
                    if (tentative_dist >= CostToReach(neighbour))
                    {
                        continue;
                    }
                    // Current best path
                    CameFrom[neighbour] = vertex;
                    Costs[neighbour] = tentative_dist;
                    HCosts[neighbour] = CostToReach(neighbour) + Heuristic(neighbour, Goal);

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

        public List<Location> GetPath(bool UseFineSmoothing, bool UseRoughSmoothing = false)
        {
            if (UseFineSmoothing)
                return Pathfinding.FinePathSmoothing(reconstructPath(Goal));
            else if (UseRoughSmoothing)
                return Pathfinding.RoughPathSmoothing(reconstructPath(Goal));
            else
                return reconstructPath(Goal);
        }

        // Reconstructs a path from graph explored by Time-Sliced A*
        private List<Location> reconstructPath(Graph.Vertex goal)
        {
            List<Location> path = new List<Location>();
            Graph nav = GameWorld.Instance.NavGraph;

            Graph.Vertex curr = goal;
            while (curr != null)
            {
                path.Insert(0, curr.Location);
                if (curr == Start)
                {
                    break;
                }
                curr = CameFrom[curr];
            }
            return path;
        }

        public ICollection<Graph.Vertex> GetSPT()
        {
            return ClosedSet;
        }

        /// <summary>
        /// Efficiency Warpper for the Dictionary access operator.
        /// This method assumes that invalid keys weren't initialized and should have a cost of Infinity.
        /// This makes it so that during initialization the algorithm doesn't need to initialize the costs of all vertices.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private double CostToReach(Graph.Vertex v)
        {
            if (Costs.ContainsKey(v))
            {
                return Costs[v];
            }
            else
            {
                return Double.MaxValue;
            }
        }

        /// <summary>
        /// Efficiency Warpper for the Dictionary access operator.
        /// This method assumes that invalid keys weren't initialized and should have a cost of Infinity.
        /// This makes it so that during initialization the algorithm doesn't need to initialize the costs of all vertices.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private double HCostToReach(Graph.Vertex v)
        {
            if (HCosts.ContainsKey(v))
            {
                return HCosts[v];
            }
            else
            {
                return Double.MaxValue;
            }
        }

        public void Render(Graphics g)
        {
            foreach (var inPQ in OpenSet.queue)
            {
                if (inPQ != null)
                {
                    Pen p = new Pen(Color.Orange);
                    g.DrawEllipse(p, (float)inPQ.Vertex.Location.X - 4, (float)inPQ.Vertex.Location.Y - 4,
                        8, 8);
                }
            }
            foreach (var evaluatedVertex in ClosedSet)
            {
                Pen p = new Pen(Color.Red);
                //g.DrawLine(p, (float) evaluatedVertex.Location.X, (float)evaluatedVertex.Location.Y,
                //    (float)CameFrom[evaluatedVertex].Location.X, (float)CameFrom[evaluatedVertex].Location.Y);
                g.DrawEllipse(p, (float) evaluatedVertex.Location.X - 3, (float)evaluatedVertex.Location.Y - 3, 
                    6, 6);
            }
        }
    }
}
