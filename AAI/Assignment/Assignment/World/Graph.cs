using System;
using System.Collections.Generic;
using Assignment.Entity;

namespace Assignment.World
{
    public class Graph
    {
        private HashSet<Vertex> vertices;

        private float NodeSpreadDistance = 1;
        private float AgentCollisionSpacing = .5f;
        private long nextVertexLabel = 0;

        /// <summary>
        /// Initialize a navMap from point (0, 0)
        /// </summary>
        public Graph()
        {
            vertices = new HashSet<Vertex>();
            BuildNavGraph(GameWorld.Instance.Width / 2, GameWorld.Instance.Height / 2, null);
        }

        /// <summary>
        /// Initialize the NavGraph from point (x, y)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Graph(double x, double y)
        {
            vertices = new HashSet<Vertex>();
            BuildNavGraph(x, y, null);
        }

        private void BuildNavGraph(double x, double y, Vertex prev)
        {
            // Base case: alread a vertex on this location, add an edge from the previous vertex to the colliding vertex.
            Vertex collisionVertex = VertexAtLocation(x, y);
            if (collisionVertex != null)
            {
                prev.Adj.Add(new Edge(collisionVertex));
                return;
            }

            // Base case: out of bounds
            if (x < 0 || x > GameWorld.Instance.Width || y < 0 || y > GameWorld.Instance.Height)
            {
                Console.WriteLine("NavGraph out-of-bounds");
                return;
            }
                
            // Create vertex on current location
            Vertex v = new Vertex(x, y, nextVertexLabel.ToString());
            nextVertexLabel++;
            
            // Check vicinity for obstructions
            List<BaseEntity> entitiesInProx = GameWorld.Instance.EntitiesInArea(new Location(x, y),  Math.Max(NodeSpreadDistance, AgentCollisionSpacing));

            // TODO check for illegal vertex locations based on entitiesInProx
            
            BuildNavGraph(x, y + NodeSpreadDistance, v);
            BuildNavGraph(x + NodeSpreadDistance, y, v);
            BuildNavGraph(x, y - NodeSpreadDistance, v);
            BuildNavGraph(x - NodeSpreadDistance, y, v);
        }

        private Vertex VertexAtLocation(double x, double y)
        {
            foreach (Vertex v in vertices)
            {
                double xDifference = x * 0.0001;
                double yDifference = y * 0.0001;
                if (v.Loc.X - x <= xDifference && v.Loc.Y - y <= yDifference)
                {
                    return v;
                }
            }
            return null;
        }
        
        public void AddVertex(Vertex v) {
            vertices.Add(v);
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
            foreach (Vertex v in vertices) {
                if (v.Label.Equals(label))
                    return v;
            }
            return null;
        }
    
        public override String ToString() {
            String res = "";
            foreach (Vertex v in vertices)
                res += v + "\n";
            return res;
        }

        public class Vertex
        {      
            public string Label;
            public HashSet<Edge> Adj;
            public Vertex Prev;
            public double Dist;
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