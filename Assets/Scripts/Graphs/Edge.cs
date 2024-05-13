namespace Graphs
{
    public class Edge
    {
        public Vertex ConnectedVertex { get; }
        public float Weight { get; }

        public Edge(Vertex connectedVertex, float weight)
        {
            ConnectedVertex = connectedVertex;
            Weight = weight;
        }
    }
}
