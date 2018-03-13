using System;
using System.Collections.Generic;
using Assignment.Entity;
using Assignment.Movement;
using Assignment.Utilities;

namespace Assignment.World
{
	public class Graph
	{
		public Vertex[,] vertices;

        private const double NodeSpreadFactor = 50; // Distance between vertices, less means more vertices in the graph.
        private double AmountOfNodesInRow = GameWorld.Instance.Width / NodeSpreadFactor;
        private double AmountOfNodesInCol = GameWorld.Instance.Height / NodeSpreadFactor;
        private const double AgentCollisionSpacing = 5f; // Used as a collision circle for all pathfinding agents
        private long nextVertexLabel = 0; // Used to generate vertex label.
        private const int XOffset = 1; // Should at least be one.
        private const int YOffset = 1; // Should at leats be one.
        private const double CardinalEdgesCost = NodeSpreadFactor;
        public double DiagonalEdgesCost = Math.Sqrt(Math.Pow(CardinalEdgesCost, 2) * 2); // Negative value disables diagonal edges.

		/// <summary>
		/// Initialize a navMap from point (0, 0)
		/// </summary>
		public Graph()
		{
			BuildNavGraph();
		}

		private Vertex VertexAtLocation(double x, double y)
		{
			foreach (Vertex v in vertices)
			{
				double xDifference = x * 0.000001;
				double yDifference = y * 0.000001;
				if (Math.Abs(v.Loc.X - x) <= xDifference && Math.Abs(v.Loc.Y - y) <= yDifference)
				{
					return v;
				}
			}
			return null;
		}

		/// <summary>
		/// Get the closesed vertex to the given location.
		/// </summary>
		/// <param name="loc">The location to start searching from.</param>
		/// <returns>The nearest vertex.</returns>
		public Vertex NearestVertexFromLocation(Location loc)
		{
			// todo optimize so start searching from location

			Vertex nearestVertex = null;
			double nearestDistance = double.MaxValue;

			for (int x = 0; x < vertices.GetLength(0); x++)
			{
				for (int y = 0; y < vertices.GetLength(1); y++)
				{
					if (vertices[x, y] == null)
						continue;

					var distance = Utility.Distance(loc, vertices[x, y].Loc);
					if (distance < nearestDistance)
					{
						nearestDistance = distance;
						nearestVertex = vertices[x, y];
					}
				}
			}

			return nearestVertex;
		}

		/// <summary>
		/// (Re)Builds the Navigation Graph used in a GameWorld.
		/// Generates vertices in a grid like pattern based on constants above the class.
		/// </summary>
		private void BuildNavGraph()
		{
			// Clear vertices
			vertices = new Vertex[(int) AmountOfNodesInRow, (int) AmountOfNodesInCol];
			GameWorld gw = GameWorld.Instance;

			// Declare help variables
			double step = NodeSpreadFactor;

            // Place vertices every step distance in the game world
            for (int x = 0; x < vertices.GetLength(0); x++)
            {
                for (int y = 0; y < vertices.GetLength(1); y++)
                {
                    int amountOfCollisions = gw.ObstaclesInArea(new Location(XOffset + x * step, YOffset + y * step), AgentCollisionSpacing).Count;
                    if (amountOfCollisions > 0)
                    {
                        continue;
                        // To save computing power: store result of amoutOfCollisions seperatly or in the Vertex class
                    }
                    
                    Location loc = new Location(XOffset + x * step, YOffset + y * step);
                    vertices[x, y] = new Vertex(loc, nextVertexLabel.ToString());
                    nextVertexLabel++;
                }
            }
            
            // Stitch edges to all adjacent vertices
            for (int x = 0; x < vertices.GetLength(0); x++)
            {
                for (int y = 0; y < vertices.GetLength(1); y++)
                {
                    int amountOfCollisions = gw.ObstaclesInArea(new Location(XOffset + x * step, YOffset + y * step), AgentCollisionSpacing).Count;
                    if (amountOfCollisions > 0)
                    {
                        continue; //TODO code for edge stitching assumes all slots in vertices[,] are not null, breaking here breaks stuff
                        // To save computing power: store result of amoutOfCollisions seperatly or in the Vertex class
                    }

                    for (int xx = Math.Max(x - 1, 0); xx < Math.Min(x + 2, vertices.GetLength(0)); xx++)
                    {
                        for (int yy = Math.Max(y - 1, 0); yy < Math.Min(y + 2, vertices.GetLength(1)); yy++)
                        {
                            // Only connect edges while both vertices (src and dest) are instantiated
                            if (vertices[x, y] != null && vertices[xx, yy] != null) {
                                if (xx == x && yy == y)
                                {
                                    // We wouldn't want to connect an edge to the src vertex itself now, would we?.
                                    continue;
                                }

                                Location a = new Location(vertices[x, y].Loc.X, vertices[x, y].Loc.Y);
                                Location b = new Location(vertices[xx, yy].Loc.X, vertices[xx, yy].Loc.Y);
                                // Add diagonal edges when enabled (non-neg. cost) and when not on the same cardinal axis as the src vertex.
                                if (DiagonalEdgesCost >= 0 && (xx != x && yy != y) && Pathfinding.Walkable(a, b))
                                {
                                    vertices[x, y].Adj.Add(new Edge(vertices[xx, yy], DiagonalEdgesCost));
                                }

                                // Add cardinal edges when dest is on the same cardinal axis as the src vertex.
                                if ((xx == x || yy == y) && Pathfinding.Walkable(a, b)) {
                                    vertices[x, y].Adj.Add(new Edge(vertices[xx, yy], CardinalEdgesCost));
                                }
                            }
                        }
                    }
                }
            }
        }
    
        public override string ToString() {
            string res = "";
            foreach (var entry in vertices)
                res += entry + "\n";
            return res;
        }

		public class Vertex : IComparable<Vertex>
		{
			public string Label;
			public List<Edge> Adj;
			public Vertex Prev;
			public double Dist;
			public double HDist;
			public bool Known;
			public Location Loc { get; set; }
			public string ExtraInfo { get; set; } //TODO node should be able to contain items or other objects (change type later)

			public Vertex(Location loc, string label)
			{
				Label = label;
				Adj = new List<Edge>();
				Prev = null;
				Loc = loc;
				Dist = -1;
				Known = false;
			}

			public Vertex(double x, double y, string label)
			{
				Label = label;
				Adj = new List<Edge>();
				Prev = null;
				Loc = new Location(x, y);
				Dist = -1;
				Known = false;
			}

			public bool Add(Edge e)
			{
				foreach (Edge f in Adj)
				{
					if (f.Dest == e.Dest)
						return false;
				}
				Adj.Add(e);
				return true;
			}

			public int CompareTo(Vertex v)
			{
				return (int) (HDist - v.HDist);
			}

			public override string ToString()
			{
				/*
                string prevLabel = (Prev == null) ? "none" : Prev.Label;
                string result = "Vertex: " + Label + " prev: " + prevLabel + " dist: " +
                                Dist + " known: " + Known + " {";
                foreach (Edge e in Adj)
                    result += e;
                return result + "}";
                */
				return String.Format("<{0},{1}>", Math.Round(Loc.X / NodeSpreadFactor), Math.Round(Loc.Y / NodeSpreadFactor));
			}
		}

		public class Edge
		{
			public Vertex Dest;
			public double Cost;

			public Edge(Vertex dest, double cost)
			{
				Dest = dest;
				Cost = cost;
			}

			public Edge(Vertex dest)
			{
				Dest = dest;
				Cost = 1;
			}

			public override string ToString()
			{
				return "{c = " + Cost + ", d = " + Dest.Label + "} ";
			}
		}
	}
}