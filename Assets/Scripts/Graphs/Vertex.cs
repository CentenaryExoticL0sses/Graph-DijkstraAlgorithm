using System.Collections.Generic;

namespace Graphs
{
    public class Vertex
    {
        public int ID { get; }
        public List<Edge> Edges { get; }

        public Vertex(int id)
        {
            ID = id;
            Edges = new List<Edge>();
        }

        public void AddEdge(Edge newEdge)
        {
            Edges.Add(newEdge);
        }

        public void AddEdge(Vertex vertex, float weight)
        {
            AddEdge(new Edge(vertex, weight));
        }
    }
}
