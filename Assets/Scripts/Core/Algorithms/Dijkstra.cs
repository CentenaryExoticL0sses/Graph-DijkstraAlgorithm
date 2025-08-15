using System.Collections.Generic;
using GraphProject.Core.DataStructures;
using GraphProject.Utilities;

namespace GraphProject.Core.Algorithms
{
    public class Dijkstra
    {
        private readonly Graph _graph;
        private Dictionary<Vertex, VertexData> _vertexData;

        public Dijkstra(Graph graph)
        {
            _graph = graph;
        }

        public List<int> FindShortestPath(int startID, int finishID)
        {
            var startVertex = _graph.FindVertex(startID);
            var finishVertex = _graph.FindVertex(finishID);

            if (startVertex == null || finishVertex == null)
                return new List<int>(); // Возвращаем пустой путь, если вершин нет

            return FindShortestPath(startVertex, finishVertex);
        }

        public List<int> FindShortestPath(Vertex startVertex, Vertex finishVertex)
        {
            InitData();

            var startData = _vertexData[startVertex];
            startData.EdgesWeightSum = 0;

            var priorityQueue = new PriorityQueue<VertexData, float>();
            priorityQueue.Enqueue(startData, 0);

            while (priorityQueue.TryDequeue(out var currentData, out _))
            {
                // Если мы уже обработали эту вершину (нашли к ней кратчайший путь), пропускаем
                if (!currentData.IsUnvisited)
                {
                    continue;
                }

                // Помечаем вершину как посещенную (её кратчайший путь найден)
                currentData.IsUnvisited = false;

                // Если достигли конечной вершины, можно завершить алгоритм
                if (currentData.Vertex == finishVertex)
                {
                    break;
                }

                // Обновляем расстояния для всех соседей
                foreach (var edge in currentData.Vertex.Edges)
                {
                    var neighborData = _vertexData[edge.ConnectedVertex];
                    if (neighborData.IsUnvisited)
                    {
                        float newWeight = currentData.EdgesWeightSum + edge.Weight;
                        if (newWeight < neighborData.EdgesWeightSum)
                        {
                            neighborData.EdgesWeightSum = newWeight;
                            neighborData.PreviousVertex = currentData.Vertex;
                            priorityQueue.Enqueue(neighborData, newWeight);
                        }
                    }
                }
            }

            return GetPath(startVertex, finishVertex);
        }

        private void InitData()
        {
            _vertexData = new Dictionary<Vertex, VertexData>();
            foreach (Vertex vertex in _graph.Vertices)
            {
                _vertexData.Add(vertex, new VertexData(vertex));
            }
        }

        private List<int> GetPath(Vertex startVertex, Vertex endVertex)
        {
            var path = new List<int>();

            // Проверяем, был ли найден путь
            if (_vertexData[endVertex].PreviousVertex == null && endVertex != startVertex)
            {
                return path; // Путь не найден, возвращаем пустой список
            }

            var current = endVertex;
            while (current != null)
            {
                path.Add(current.ID);
                current = _vertexData[current].PreviousVertex;
            }
            path.Reverse();

            // Убедимся, что путь начинается с начальной вершины
            if (path.Count > 0 && path[0] == startVertex.ID)
            {
                return path;
            }

            return new List<int>(); // Если что-то пошло не так, возвращаем пустой путь
        }
    }
}