
namespace GraphProject.Core.DataStructures
{
    public class VertexData
    {
        public Vertex Vertex { get; set; }

        public bool IsUnvisited { get; set; }

        public float EdgesWeightSum { get; set; }

        public Vertex PreviousVertex { get; set; }

        public VertexData(Vertex vertex)
        {
            Vertex = vertex;
            IsUnvisited = true;
            EdgesWeightSum = float.MaxValue;
            PreviousVertex = null;
        }
    }
}

