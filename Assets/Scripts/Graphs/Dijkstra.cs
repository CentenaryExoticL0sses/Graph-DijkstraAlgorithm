using System;
using System.Collections.Generic;

namespace Graphs
{
    public class Dijkstra
    {
        private Graph _graph;
        private List<VertexData> _vertexData;

        public Dijkstra(Graph graph)
        {
            _graph = graph;
        }

        private void InitData()
        {
            _vertexData = new List<VertexData>();
            foreach (Vertex vertex in _graph.Vertices)
            {
                _vertexData.Add(new VertexData(vertex));
            }
        }

        private VertexData GetVertexData(Vertex vertex)
        {
            foreach (VertexData data in _vertexData)
            {
                if (data.Vertex.Equals(vertex))
                {
                    return data;
                }
            }
            return null;
        }

        public VertexData FindUnvisitedVertexWithMinSum()
        {
            float minValue = float.MaxValue;
            VertexData minVertexInfo = null;
            foreach (VertexData data in _vertexData)
            {
                if (data.IsUnvisited && data.EdgesWeightSum < minValue)
                {
                    minVertexInfo = data;
                    minValue = data.EdgesWeightSum;
                }
            }
            return minVertexInfo;
        }

        public List<int> FindShortestPath(int startID, int finishID)
        {
            return FindShortestPath(_graph.FindVertex(startID), _graph.FindVertex(finishID));
        }

        public List<int> FindShortestPath(Vertex startVertex, Vertex finishVertex)
        {
            InitData();
            VertexData first = GetVertexData(startVertex);
            first.EdgesWeightSum = 0;
            while (true)
            {
                VertexData current = FindUnvisitedVertexWithMinSum();
                if (current == null)
                {
                    break;
                }
                SetSumToNextVertex(current);
            }
            return GetPath(startVertex, finishVertex);
        }

        private void SetSumToNextVertex(VertexData data)
        {
            data.IsUnvisited = false;
            foreach (Edge edge in data.Vertex.Edges)
            {
                var nextData = GetVertexData(edge.ConnectedVertex);
                var sum = data.EdgesWeightSum + edge.Weight;
                if (sum < nextData.EdgesWeightSum)
                {
                    nextData.EdgesWeightSum = sum;
                    nextData.PreviousVertex = data.Vertex;
                }
            }
        }

        private List<int> GetPath(Vertex startVertex, Vertex endVertex)
        {
            List<int> path = new List<int>{ endVertex.ID };

            while (startVertex != endVertex)
            {
                endVertex = GetVertexData(endVertex).PreviousVertex;
                if(endVertex == null)
                    break;

                path.Add(endVertex.ID);
            }
            return path;
        }
    }
}

