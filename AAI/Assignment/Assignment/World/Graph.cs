using System;
using System.Collections.Generic;
using Assignment.Movement.Planning;
using Assignment.Utilities;

namespace Assignment.World
{
	public class Graph
	{
		public Vertex[,] vertices;

		private readonly double NodeSpreadDistance;
		private readonly double AgentCollisionSpacing; // Used as a collision circle for all pathfinding agents
		public readonly int AmountOfNodesInRow;
		public readonly int AmountOfNodesInCol;
		private long nextVertexLabel = 0; // Used to generate vertex label.
		private const int XOffset = 1; // Should at least be one.
		private const int YOffset = 1; // Should at leats be one.
		private readonly double CardinalEdgesCost;
		public readonly double DiagonalEdgesCost; // Negative value disables diagonal edges.

		/// <summary>
		/// Initialize a navMap
		/// </summary>
		public Graph()
		{
            NodeSpreadDistance = Settings.Instance.NavigationCoarseness;
            AmountOfNodesInRow = (int) (GameWorld.Instance.Width / NodeSpreadDistance);
            AmountOfNodesInCol = (int) (GameWorld.Instance.Height / NodeSpreadDistance);
            AgentCollisionSpacing = 2 * Settings.Instance.EntitySize;
            CardinalEdgesCost = NodeSpreadDistance;
            DiagonalEdgesCost = Math.Sqrt(Math.Pow(CardinalEdgesCost, 2) * 2);

            BuildNavGraph();
		}

		private Vertex VertexAtLocation(double x, double y)
		{
			foreach (Vertex v in vertices)
			{
				double xDifference = x * 0.000001;
				double yDifference = y * 0.000001;
				if (Math.Abs(v.Location.X - x) <= xDifference && Math.Abs(v.Location.Y - y) <= yDifference)
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
			var topLeftCandidateX = (int) Math.Floor(loc.X / Settings.Instance.NavigationCoarseness);
			var topLeftCandidateY = (int) Math.Floor(loc.Y / Settings.Instance.NavigationCoarseness);

			Vertex nearestVertex = null;
			double nearestDistance = double.MaxValue;

			for (int layer = 1; layer < 1000; layer++)
			{
				for (int x = topLeftCandidateX; x < layer * 2 + topLeftCandidateX; x++)
				{
					var y1 = topLeftCandidateY; // top layer.
					var y2 = topLeftCandidateY + (layer * 2) - 1; //bottom layer.

					// Check for outside the navigation graph.
					if (x < 0 || x >= vertices.GetLength(0) || y1 < 0 || y2 >= vertices.GetLength(1))
					{
						continue;
					}
					// Check vertex is valid for top layer.
					UpdateNearestVertex(loc, ref nearestVertex, ref nearestDistance, x, y1);
					// Check vertex is valid for bottom layer.
					UpdateNearestVertex(loc, ref nearestVertex, ref nearestDistance, x, y2);
				}

				for (int y = topLeftCandidateY + 1; y < layer * 2 + topLeftCandidateY - 1; y++)
				{
					var x1 = topLeftCandidateX; // left side.
					var x2 = topLeftCandidateX + (layer * 2) - 1; //right side.

					// Check for outside the navigation graph.
					if (y < 0 || y >= vertices.GetLength(1) || x1 < 0 || x2 >= vertices.GetLength(0))
					{
						continue;
					}
					// Check vertex is valid for left side.
					UpdateNearestVertex(loc, ref nearestVertex, ref nearestDistance, x1, y);
					// Check vertex is valid for right side.
					UpdateNearestVertex(loc, ref nearestVertex, ref nearestDistance, x2, y);
				}

				// ending condition.
				if (nearestVertex != null)
				{
					return nearestVertex;
				}

				topLeftCandidateX--;
				topLeftCandidateY--;
			}


			return null;
		}

		private void UpdateNearestVertex(Location loc, ref Vertex nearestVertex, ref double nearestDistance, int x, int y)
		{
			if (vertices[x, y] != null)
			{
				var distance1 = Utility.Distance(loc, vertices[x, y].Location);
				if (vertices[x, y] != null && nearestDistance > distance1)
				{
					nearestDistance = distance1;
					nearestVertex = vertices[x, y];
				}
			}
		}



		/// <summary>
		/// (Re)Builds the Navigation Graph used in a GameWorld.
		/// Generates vertices in a grid like pattern based on constants above the class.
		/// </summary>
		private void BuildNavGraph()
		{

			// Declare help variables: Distance between vertices, less means more vertices in the graph.
			double step = Settings.Instance.NavigationCoarseness;

			// Clear vertices
			GameWorld gw = GameWorld.Instance;
			vertices = new Vertex[AmountOfNodesInRow, AmountOfNodesInCol];

			// Place vertices every step distance in the game world
			for (int x = 0; x < vertices.GetLength(0); x++)
			{
				for (int y = 0; y < vertices.GetLength(1); y++)
				{
					// Only place the vertex if there is enough space for the agent to fit in.
					Location newVertexLoc = new Location(XOffset + x * step, YOffset + y * step);

					if (gw.ObstaclesInArea(newVertexLoc, AgentCollisionSpacing, true).Count > 0)
					{
						continue;
						// To save computing power: store result of amoutOfCollisions seperatly or in the Vertex class
					}

					vertices[x, y] = new Vertex(newVertexLoc, nextVertexLabel.ToString());
					nextVertexLabel++;
				}
			}

			// Stitch edges to all adjacent vertices
			for (int x = 0; x < vertices.GetLength(0); x++)
			{
				for (int y = 0; y < vertices.GetLength(1); y++)
				{
					for (int xx = Math.Max(x - 1, 0); xx < Math.Min(x + 2, vertices.GetLength(0)); xx++)
					{
						for (int yy = Math.Max(y - 1, 0); yy < Math.Min(y + 2, vertices.GetLength(1)); yy++)
						{
							// Only connect edges while both vertices (src and dest) are instantiated
							if (vertices[x, y] != null && vertices[xx, yy] != null)
							{
								if (xx == x && yy == y)
								{
									// We wouldn't want to connect an edge to the src vertex itself now, would we?.
									continue;
								}

								Location a = new Location(vertices[x, y].Location.X, vertices[x, y].Location.Y);
								Location b = new Location(vertices[xx, yy].Location.X, vertices[xx, yy].Location.Y);
								// Add diagonal edges when enabled (non-neg. cost) and when not on the same cardinal axis as the src vertex.
								if (DiagonalEdgesCost >= 0 && (xx != x && yy != y) && Pathfinding.Walkable(a, b))
								{
									vertices[x, y].Adjacent.Add(new Edge(vertices[xx, yy], DiagonalEdgesCost));
								}

								// Add cardinal edges when dest is on the same cardinal axis as the src vertex.
								if ((xx == x || yy == y) && Pathfinding.Walkable(a, b))
								{
									vertices[x, y].Adjacent.Add(new Edge(vertices[xx, yy], CardinalEdgesCost));
								}
							}
						}
					}
				}
			}
		}

		public override string ToString()
		{
			string res = "";
			foreach (var entry in vertices)
				res += entry + "\n";
			return res;
		}

		public class Vertex : IComparable<Vertex>
		{
			public string Label;
			public List<Edge> Adjacent;
			public Vertex Previous;
			public double Distance;
			public double HeuristicDistance;
			public bool Known;
			public Location Location { get; set; }

			public Vertex(Location location, string label)
			{
				Label = label;
				Adjacent = new List<Edge>();
				Previous = null;
				Location = location;
				Distance = -1;
				Known = false;
			}

			public Vertex(double x, double y, string label)
			{
				Label = label;
				Adjacent = new List<Edge>();
				Previous = null;
				Location = new Location(x, y);
				Distance = -1;
				Known = false;
			}

			public bool Add(Edge edge)
			{
				foreach (Edge f in Adjacent)
				{
					if (f.Dest == edge.Dest)
						return false;
				}
				Adjacent.Add(edge);
				return true;
			}

			public int CompareTo(Vertex v)
			{
				return (int) (HeuristicDistance - v.HeuristicDistance);
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
				return String.Format("<{0},{1}>", Math.Round(Location.X / Settings.Instance.NavigationCoarseness), Math.Round(Location.Y / Settings.Instance.NavigationCoarseness));
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