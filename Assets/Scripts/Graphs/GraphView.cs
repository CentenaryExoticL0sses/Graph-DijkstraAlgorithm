using System.Collections.Generic;
using UnityEngine;
using GraphProject.Graphs.Data;
using GraphProject.Graphs.View;

namespace GraphProject.Graphs
{
    /// <summary>
    /// Представление графа. Отвечает за создание и управление визуальными
    /// объектами вершин и рёбер на сцене.
    /// </summary>
    public class GraphView : MonoBehaviour
    {
        [Header("Шаблоны объектов")]
        [SerializeField] private VertexDisplayObject _vertexPrefab;
        [SerializeField] private EdgeDisplayObject _edgePrefab;

        // Методы для получения всех визуальных объектов (может понадобиться для сохранения).
        public IReadOnlyDictionary<int, VertexDisplayObject> VertexObjects => _vertexObjects;
        public IReadOnlyList<EdgeDisplayObject> EdgeObjects => _edgeObjects;

        // Словари для быстрого доступа к визуальным объектам по их ID.
        private readonly Dictionary<int, VertexDisplayObject> _vertexObjects = new();
        private readonly List<EdgeDisplayObject> _edgeObjects = new();

        private GraphModel _model;

        /// <summary>
        /// Инициализация представления.
        /// </summary>
        /// <param name="model">Модель данных, на события которой нужно подписаться.</param>
        public void Initialize(GraphModel model)
        {
            _model = model;

            // Подписываемся на события модели, чтобы реагировать на изменения данных.
            _model.OnVertexAdded += HandleVertexAdded;
            _model.OnEdgeAdded += HandleEdgeAdded;
            _model.OnGraphCleared += HandleGraphCleared;
        }

        // Отписываемся от событий при уничтожении объекта, чтобы избежать утечек памяти.
        private void OnDestroy()
        {
            if (_model != null)
            {
                _model.OnVertexAdded -= HandleVertexAdded;
                _model.OnEdgeAdded -= HandleEdgeAdded;
                _model.OnGraphCleared -= HandleGraphCleared;
            }
        }

        // Обработчик события добавления вершины.
        private void HandleVertexAdded(VertexObjectData data)
        {
            var newVertex = Instantiate(_vertexPrefab, data.Position, Quaternion.identity, transform);
            newVertex.Initialize(data);
            _vertexObjects[data.ID] = newVertex;
        }

        // Обработчик события добавления ребра.
        private void HandleEdgeAdded(EdgeObjectData data, float weight)
        {
            var firstVertex = GetVertexObject(data.FirstVertexID);
            var secondVertex = GetVertexObject(data.SecondVertexID);

            if (firstVertex != null && secondVertex != null)
            {
                var newEdge = Instantiate(_edgePrefab, transform);
                newEdge.Initialize(data, weight, firstVertex.Data.Position, secondVertex.Data.Position);
                _edgeObjects.Add(newEdge);
            }
        }

        // Обработчик события очистки графа.
        private void HandleGraphCleared()
        {
            foreach (var edge in _edgeObjects)
            {
                Destroy(edge.gameObject);
            }
            foreach (var vertex in _vertexObjects.Values)
            {
                Destroy(vertex.gameObject);
            }
            _vertexObjects.Clear();
            _edgeObjects.Clear();
        }

        // Публичные методы для получения визуальных объектов.
        public VertexDisplayObject GetVertexObject(int id)
        {
            _vertexObjects.TryGetValue(id, out var vertex);
            return vertex;
        }

        public EdgeDisplayObject GetEdgeObject(int firstID, int secondID)
        {
            return _edgeObjects.Find(edge =>
                edge.Data.FirstVertexID == firstID && edge.Data.SecondVertexID == secondID ||
                edge.Data.FirstVertexID == secondID && edge.Data.SecondVertexID == firstID);
        }
    }
}