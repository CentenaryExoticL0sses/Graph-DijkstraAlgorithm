using System;
using System.Collections.Generic;
using GraphProject.Core.DataStructures;
using GraphProject.Graphs.Data;

namespace GraphProject.Graphs
{
    /// <summary>
    /// Модель данных графа. Хранит и управляет логической структурой графа.
    /// Не зависит от Unity и визуального представления.
    /// </summary>
    public class GraphModel
    {
        public event Action<VertexObjectData> OnVertexAdded;

        public event Action<EdgeObjectData, float> OnEdgeAdded;

        public event Action OnGraphCleared;

        public IReadOnlyDictionary<int, VertexObjectData> VertexData => _vertexData;
        public IReadOnlyList<EdgeObjectData> EdgeData => _edgeData;

        // Ссылка на сам логический граф.
        public Graph Graph { get; private set; }

        // Храним данные для визуализации вместе с логикой.
        private readonly Dictionary<int, VertexObjectData> _vertexData = new();
        private readonly List<EdgeObjectData> _edgeData = new();

        public GraphModel()
        {
            Graph = new Graph();
        }

        /// <summary>
        /// Добавляет новую вершину в граф.
        /// </summary>
        /// <param name="position">Позиция для новой вершины.</param>
        /// <returns>Данные о созданной вершине.</returns>
        public VertexObjectData AddVertex(UnityEngine.Vector2 position)
        {
            int id = Graph.Vertices.Count;
            Graph.AddVertex(id);

            var data = new VertexObjectData(id, position);
            _vertexData[id] = data;

            // Уведомляем подписчиков о добавлении новой вершины.
            OnVertexAdded?.Invoke(data);
            return data;
        }

        /// <summary>
        /// Добавляет ребро между двумя вершинами.
        /// </summary>
        /// <param name="firstID">ID первой вершины.</param>
        /// <param name="secondID">ID второй вершины.</param>
        /// <returns>Данные о созданном ребре или null, если ребро уже существует.</returns>
        public EdgeObjectData AddEdge(int firstID, int secondID)
        {
            if (Graph.FindVertex(firstID) == null || Graph.FindVertex(secondID) == null)
                return default;

            // Проверяем, существует ли уже такое ребро.
            foreach (var edge in _edgeData)
            {
                if (edge.FirstVertexID == firstID && edge.SecondVertexID == secondID ||
                    edge.FirstVertexID == secondID && edge.SecondVertexID == firstID)
                {
                    return default; // Ребро уже существует
                }
            }

            var firstVertexPos = _vertexData[firstID].Position;
            var secondVertexPos = _vertexData[secondID].Position;
            float weight = UnityEngine.Vector2.Distance(firstVertexPos, secondVertexPos);

            Graph.AddEdge(firstID, secondID, weight);

            var data = new EdgeObjectData(firstID, secondID);
            _edgeData.Add(data);

            // Уведомляем подписчиков о добавлении нового ребра.
            OnEdgeAdded?.Invoke(data, weight);
            return data;
        }

        /// <summary>
        /// Очищает все данные графа.
        /// </summary>
        public void Clear()
        {
            Graph = new Graph();
            _vertexData.Clear();
            _edgeData.Clear();

            OnGraphCleared?.Invoke();
        }
    }
}