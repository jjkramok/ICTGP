﻿using System;
using System.Collections.Generic;
using Assignment.Utilities;
using Assignment.World;

namespace Assignment.Movement.Planning
{
    class AStarTimeSliced : ISearchTimeSliced
    {
        private Graph.Vertex Start, Goal;
        private Graph NavigationGraph;
        private PriorityQueue<Graph.Vertex> PriorityQueue;
        private bool GoalAlreadyReached = false;
        private AStarTSNode[,] NodesInfo;

        public AStarTimeSliced(Graph.Vertex start, Graph.Vertex goal)
        {
            NavigationGraph = GameWorld.Instance.NavGraph; //new Graph();
            // TODO rework to use the prime NavGraph (remove NearestVertex calls etc.)
            Start = NavigationGraph.NearestVertexFromLocation(start.Location);
            Goal = NavigationGraph.NearestVertexFromLocation(goal.Location);
            PriorityQueue = new PriorityQueue<World.Graph.Vertex>();
            int nodesInARow = GameWorld.Instance.NavGraph.AmountOfNodesInRow;
            int nodesInACol = GameWorld.Instance.NavGraph.AmountOfNodesInCol;

            // Initialize graph
            // TODO use Sjoerds approach (See keep)
            for (int x = 0; x < nodesInARow; x++)
            {
                for (int y = 0; y < nodesInACol; y++)
                {
                    AStarTSNode nodeInfo = new AStarTSNode();
                    nodeInfo.IsNullInNavGraph = NavigationGraph.vertices[x, y] == null;
                    NodesInfo[x, y] = nodeInfo;
                }
            }

            // Add start node to queue
            Start.Distance = 0;
            Start.HeuristicDistance = Heuristic(Start, Goal);
            Start.Previous = null;
            PriorityQueue.Add(Start);
        }

        public SearchStatus CycleOnce()
        {
            if (!PriorityQueue.IsEmpty)
            {
                Graph.Vertex vertex = PriorityQueue.Get();

                if (vertex.Known)
                {
                    return SearchStatus.TARGET_NOT_FOUND; // Node already evaluated. TODO Maybe call function again since we didn't really do anything this cycle?
                }

                if (vertex.Distance == -1 || vertex.HeuristicDistance == -1)
                {
                    Console.WriteLine("Vertex {0} has negative distance!", vertex);
                }

                vertex.Known = true;

                // End-case : goal found, tell our handler that the goal has been reached.
                if (vertex == Goal)
                {
                    GoalAlreadyReached = true;
                    return SearchStatus.TARGET_FOUND;
                }

                foreach (Graph.Edge edge in vertex.Adjacent)
                {
                    Graph.Vertex w = edge.Dest;
                    if (w.Known)
                    {
                        continue; // Already evaluated
                    }
                    w.HeuristicDistance = vertex.Distance + edge.Cost + Heuristic(w, Goal);
                    PriorityQueue.Add(w); // Newly discovered node

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
            return Goal.Distance;
        }

        public double HeuristicCostToTarget()
        {
            return Goal.HeuristicDistance;
        }

        public List<Location> GetPath()
        {
            return Pathfinding.reconstructPath(Goal);
        }

        public List<Graph.Edge> GetSPT()
        {
            throw new NotImplementedException();
        }

        class AStarTSNode
        {
            internal bool Known;
            internal double Distance;
            internal double HDistance;
            internal bool IsNullInNavGraph;
            internal AStarTSNode Prev;

            public AStarTSNode()
            {
                Known = false;
                Distance = Double.MaxValue;
                HDistance = Double.MaxValue;
                Prev = null;
            }
        }
    }
}
