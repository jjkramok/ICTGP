using System;
using System.Collections.Generic;
using Assignment.Entity;

namespace Assignment.World
{
    public class Graph
    {
        //public HashSet<Vertex> vertices;
        public Dictionary<string, Vertex> old_vertices;
        public Vertex[,] vertices = new Vertex[(int) (GameWorld.Instance.Width / NodeSpreadFactor), (int) (GameWorld.Instance.Height / NodeSpreadFactor)];

        private const double NodeSpreadFactor = 25;
        private double AmountOfNodesInRow = GameWorld.Instance.Width / NodeSpreadFactor;
        private double AgentCollisionSpacing = 5f;
        private long nextVertexLabel = 0;
        private const int MAX_NAV_DEPTH = 5000;

        /// <summary>
        /// Initialize a navMap from point (0, 0)
        /// </summary>
        public Graph()
        {
            old_vertices = new Dictionary<string, Vertex>();
            //BuildNavGraphRec(GameWorld.Instance.Width / 2, GameWorld.Instance.Height / 2, null, 0);
            BuildNavGraph();
        }

        /// <summary>
        /// Initialize the NavGraph from point (x, y)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Graph(double x, double y)
        {
            old_vertices = new Dictionary<string, Vertex>();
            //BuildNavGraphRec(x, y, null, 0);
            BuildNavGraph();
        }

        private void BuildNavGraphRec(double x, double y, Vertex prev, int depth)
        {
            // Base case
            if (depth == MAX_NAV_DEPTH)
            {
                return;
            }
            // Base case: alread a vertex on this location, add an edge from the previous vertex to the colliding vertex.
            Vertex collisionVertex = VertexAtLocation(x, y);
            if (collisionVertex != null)
            {
                prev.Add(new Edge(collisionVertex));
                return;
            }

            // Base case: out of bounds
            if (x <= 0 || x >= GameWorld.Instance.Width || y <= 0 || y >= GameWorld.Instance.Height)
            {
                return;
            }

            // Create vertex on current location
            Vertex v = new Vertex(x, y, nextVertexLabel.ToString());
            old_vertices.Add(v.Label, v);
            if (prev != null)
                v.Add(new Edge(prev));
            nextVertexLabel++;

            // Check vicinity for obstructions
            List<BaseEntity> entitiesInProx = GameWorld.Instance.EntitiesInArea(new Location(x, y), Math.Max(AmountOfNodesInRow, AgentCollisionSpacing));

            // TODO check for illegal vertex locations based on entitiesInProx

            BuildNavGraphRec(x, y + AmountOfNodesInRow, v, depth + 1);
            BuildNavGraphRec(x + AmountOfNodesInRow, y, v, depth + 1);
            BuildNavGraphRec(x, y - AmountOfNodesInRow, v, depth + 1);
            BuildNavGraphRec(x - AmountOfNodesInRow, y, v, depth + 1);
        }

        private Vertex VertexAtLocation(double x, double y)
        {
            foreach (var entry in old_vertices)
            {
                Vertex v = entry.Value;
                double xDifference = x * 0.000001;
                double yDifference = y * 0.000001;
                if (Math.Abs(v.Loc.X - x) <= xDifference && Math.Abs(v.Loc.Y - y) <= yDifference)
                {
                    return v;
                }
            }
            return null;
        }
        
        private void BuildNavGraph()
        {
            // Clear vertices
            old_vertices = new Dictionary<string, Vertex>();
            GameWorld gw = GameWorld.Instance;

            // Declare help variables
            int xOffset = 10;
            int yOffset = 10;
            double step = NodeSpreadFactor;

            // Place vertices every step distance in the game world
            for (int x = 0; x < vertices.GetLength(0); x++)
            {
                for (int y = 0; y < vertices.GetLength(1); y++)
                {
                    Location loc = new Location(xOffset + x * step, yOffset + y * step);
                    vertices[x, y] = new Vertex(loc, nextVertexLabel.ToString());
                    nextVertexLabel++;
                }
            }
            
            // Place vertices every step distance in the game world
            for (int x = 0; x < vertices.GetLength(0); x++)
            {
                for (int y = 0; y < vertices.GetLength(1); y++)
                {
                    if (x + 1 < vertices.GetLength(0))
                    {
                        vertices[x, y].Adj.Add(new Edge(vertices[x + 1, y], 1));
                    }
                    if (x + 1 < vertices.GetLength(0) && y + 1 < vertices.GetLength(1))
                    {
                        vertices[x, y].Adj.Add(new Edge(vertices[x + 1, y + 1], 1.5));
                    }
                    if (y + 1 < vertices.GetLength(1))
                    {
                        vertices[x, y].Adj.Add(new Edge(vertices[x, y + 1], 1));
                    }
                    if (x - 1 > -1 && y + 1 < vertices.GetLength(1))
                    {
                        vertices[x, y].Adj.Add(new Edge(vertices[x - 1, y + 1], 1.5));
                    }
                    if (x - 1 > -1)
                    {
                        vertices[x, y].Adj.Add(new Edge(vertices[x - 1, y], 1));
                    }
                    if (x - 1 > -1 && y - 1 > -1)
                    {
                        vertices[x, y].Adj.Add(new Edge(vertices[x - 1, y - 1], 1.5));
                    }
                    if (y - 1 > -1)
                    {
                        vertices[x, y].Adj.Add(new Edge(vertices[x, y - 1], 1));
                    }
                    if (x + 1 < vertices.GetLength(0) && y - 1 > -1)
                    {
                        vertices[x, y].Adj.Add(new Edge(vertices[x + 1, y - 1], 1.5));
                    }
                }
            }

            
            // TODO Correctly stitch corner vertices


        }

        public void AddVertex(Vertex v) {
            old_vertices.Add(v.Label, v);
        }
    
        private void AddEdge(String src, String dest, double cost) {
            Vertex s = GetVertex(src);
            Vertex d = GetVertex(dest);
            if (s == null) {
                Console.WriteLine("src vertex does not exist");
                return;
            } else if (d == null) {
                Console.WriteLine("dest vertex does not exist");
                return;
            }
            s.Adj.Add(new Edge(d, cost));
        }
    
        private Vertex GetVertex(String label) {
            foreach (var entry in old_vertices) {
                Vertex v = entry.Value;
                if (v.Label.Equals(label))
                    return v;
            }
            return null;
        }
    
        public override String ToString() {
            String res = "";
            foreach (var entry in old_vertices)
                res += entry.Value + "\n";
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
            }
            
            public Vertex(double x, double y, string label) {
                Label = label;
                Adj = new HashSet<Edge>();
                Prev = null;
                Loc = new Location(x, y);
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
                return (int)(HDist - v.HDist);
            }

            public override string ToString() {
                string prevLabel = (Prev == null) ? "none" : Prev.Label;
                string result = "Vertex: " + Label + " prev: " + prevLabel + " dist: " +
                                Dist + " known: " + Known + " {";
                foreach (Edge e in Adj)
                    result += e;
                return result + "}";
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