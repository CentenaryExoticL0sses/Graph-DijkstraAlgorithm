using System.Collections.Generic;
using System.Linq;

namespace GraphProject.Core.DataStructures
{
    public class Graph
    {
        public List<Vertex> Vertices => _vertices.Values.ToList();

        private readonly Dictionary<int, Vertex> _vertices;

        public Graph()
        {
            _vertices = new Dictionary<int, Vertex>();
        }

        public Vertex AddVertex(int id)
        {
            if (_vertices.ContainsKey(id))
            {
                return _vertices[id];
            }
            Vertex newVertex = new Vertex(id);
            _vertices.Add(id, newVertex);
            return newVertex;
        }

        public Vertex FindVertex(int id)
        {
            _vertices.TryGetValue(id, out var vertex);
            return vertex;
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