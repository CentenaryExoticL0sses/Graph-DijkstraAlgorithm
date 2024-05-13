using System.Collections.Generic;

namespace Graphs
{
    public class Graph
    {
        public List<Vertex> Vertices { get; }

        public Graph()
        {
            Vertices = new List<Vertex>();
        }

        public Vertex AddVertex(int id)
        {
            Vertex newVertex = new Vertex(id);
            Vertices.Add(newVertex);
            return newVertex;
        }

        public Vertex FindVertex(int id)
        {
            foreach (Vertex vertex in Vertices)
            {
                if (vertex.ID == id)
                {
                    return vertex;
                }
            }

            return null;
        }

        public void AddEdge(int firstID, int secondID, float weight)
        {
            Vertex firstVertex = FindVertex(firstID);
            Vertex secondVertex = FindVertex(secondID);
            if (firstVertex != null && secondVertex != null)
            {
                firstVertex.AddEdge(secondVertex, weight);
                secondVertex.AddEdge(firstVertex, weight);
            }
        }
    }
}
