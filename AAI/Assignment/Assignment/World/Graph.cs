using System;
using System.Collections.Generic;
using Assignment.Entity;

namespace Assignment.World
{
    public class Graph
    {
        public Vertex[,] vertices;

        private const double NodeSpreadFactor = 10; // Distance between vertices, less means more vertices in the graph.
        private double AmountOfNodesInRow = GameWorld.Instance.Width / NodeSpreadFactor;
        private double AmountOfNodesInCol = GameWorld.Instance.Height / NodeSpreadFactor;
        private const double AgentCollisionSpacing = 5f; // Used as a collision circle for all pathfinding agents
        private long nextVertexLabel = 0; // Used to generate vertex label.
        private const int XOffset = 1; // Should at least be one.
        private const int YOffset = 1; // Should at leats be one.
        private const double CardinalEdgesCost = NodeSpreadFactor;
        public double DiagonalEdgesCost = -1;// Math.Sqrt(Math.Pow(CardinalEdgesCost, 2) * 2); // Negative value disables diagonal edges.

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

            // TODO hold into account obstacles, bounding circles and entity collision box

            // Place vertices every step distance in the game world
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
                    

                    Location loc = new Location(XOffset + x * step, YOffset + y * step);
                    vertices[x, y] = new Vertex(loc, nextVertexLabel.ToString());
                    nextVertexLabel++;
                }
            }
            
            // Place vertices every step distance in the game world
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
                    if (x + 1 < vertices.GetLength(0))
                    {
                        vertices[x, y].Adj.Add(new Edge(vertices[x + 1, y], CardinalEdgesCost));
                    }
                    if (x + 1 < vertices.GetLength(0) && y + 1 < vertices.GetLength(1) && DiagonalEdgesCost > -1)
                    {
                        vertices[x, y].Adj.Add(new Edge(vertices[x + 1, y + 1], DiagonalEdgesCost));
                    }
                    if (y + 1 < vertices.GetLength(1))
                    {
                        vertices[x, y].Adj.Add(new Edge(vertices[x, y + 1], CardinalEdgesCost));
                    }
                    if (x - 1 > -1 && y + 1 < vertices.GetLength(1) && DiagonalEdgesCost > -1)
                    {
                        vertices[x, y].Adj.Add(new Edge(vertices[x - 1, y + 1], DiagonalEdgesCost));
                    }
                    if (x - 1 > -1)
                    {
                        vertices[x, y].Adj.Add(new Edge(vertices[x - 1, y], CardinalEdgesCost));
                    }
                    if (x - 1 > -1 && y - 1 > -1 && DiagonalEdgesCost > -1)
                    {
                        vertices[x, y].Adj.Add(new Edge(vertices[x - 1, y - 1], DiagonalEdgesCost));
                    }
                    if (y - 1 > -1)
                    {
                        vertices[x, y].Adj.Add(new Edge(vertices[x, y - 1], CardinalEdgesCost));
                    }
                    if (x + 1 < vertices.GetLength(0) && y - 1 > -1 && DiagonalEdgesCost > -1)
                    {
                        vertices[x, y].Adj.Add(new Edge(vertices[x + 1, y - 1], DiagonalEdgesCost));
                    }
                }
            }
        }

        public void AddVertex(Vertex v) {
            if (v.Loc != null)
            {
                Location loc = v.Loc;
                vertices[(int) loc.X, (int) loc.Y] = v;
            }
        }
    
        private Vertex GetVertex(String label) {
            for (int x = 0; x < vertices.GetLength(0); x++)
            {
                for (int y = 0; y < vertices.GetLength(1); y++)
                {
                    if (vertices[x,y].Label.Equals(label))
                        return vertices[x,y];
                }
            }
            return null;
        }
    
        public override String ToString() {
            String res = "";
            foreach (var entry in vertices)
                res += entry + "\n";
            return res;
        }

        public class Vertex : IComparable<Vertex>
        {      
            public string Label;
            public HashSet<Edge> Adj;
            public Vertex Prev;
            public double Dist;
            public double HDist;
            public bool Known;
            public Location Loc { get; set; }
            public string ExtraInfo { get; set; } //TODO node should be able to contain items or other objects (change type later)
            
            public Vertex(Location loc, string label) {
                Label = label;
                Adj = new HashSet<Edge>();
                Prev = null;
                Loc = loc;
                Dist = -1;
                Known = false;
            }
            
            public Vertex(double x, double y, string label) {
                Label = label;
                Adj = new HashSet<Edge>();
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
                return (int)(HDist - v.HDist); // TODO dist compare (to HDist?)
            }

            public override string ToString() {
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

            public Edge(Vertex dest, double cost) {
                Dest = dest;
                Cost = cost;
            }

            public Edge(Vertex dest)
            {
                Dest = dest;
                Cost = 1;
            }

            public override string ToString() {
                return "{c = " + Cost + ", d = " + Dest.Label + "} ";
            }
        }
    }
}