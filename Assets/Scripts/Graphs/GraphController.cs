using System.Collections.Generic;
using UnityEngine;
using GraphProject.Graphs.Data;
using GraphProject.Graphs.View;
using GraphProject.Services;

namespace GraphProject.Graphs
{
    /// <summary>
    /// Контроллер графа. Является точкой входа для всех операций с графом.
    /// Связывает модель (GraphModel) и представление (GraphView).
    /// </summary>
    public class GraphController : MonoBehaviour
    {
        [Header("Компоненты")]
        [SerializeField] private GraphView _view; // Ссылка на представление графа

        public IReadOnlyDictionary<int, VertexDisplayObject> VertexObjects => _view.VertexObjects;
        public IReadOnlyList<EdgeDisplayObject> EdgeObjects => _view.EdgeObjects;

        public GraphModel Model { get; private set; }

        private PathfindingService _pathfindingService;

        /// <summary>
        /// Инициализация контроллера. Вызывается из AppManager.
        /// </summary>
        public void Initialize()
        {
            Model = new GraphModel();
            _pathfindingService = new PathfindingService();

            // Передаем модель в представление, чтобы оно могло подписаться на события.
            _view.Initialize(Model);
        }

        /// <summary>
        /// Создание новой вершины.
        /// </summary>
        /// <param name="position">Позиция для новой вершины.</param>
        /// <returns>Визуальный объект созданной вершины.</returns>
        public VertexDisplayObject CreateVertex(Vector2 position)
        {
            var vertexData = Model.AddVertex(position);
            return _view.GetVertexObject(vertexData.ID);
        }

        /// <summary>
        /// Создание нового ребра.
        /// </summary>
        /// <param name="firstID">ID первой вершины.</param>
        /// <param name="secondID">ID второй вершины.</param>
        /// <returns>Визуальный объект созданного ребра.</returns>
        public EdgeDisplayObject CreateEdge(int firstID, int secondID)
        {
            var edgeData = Model.AddEdge(firstID, secondID);
            if (edgeData.Equals(default(EdgeObjectData)))
            {
                // Если ребро не было создано (уже существует), возвращаем существующий объект.
                return GetEdgeObject(firstID, secondID);
            }
            return _view.GetEdgeObject(edgeData.FirstVertexID, edgeData.SecondVertexID);
        }

        /// <summary>
        /// Поиск кратчайшего пути.
        /// </summary>
        public List<int> FindShortestPath(int firstID, int secondID)
        {
            return _pathfindingService.FindShortestPath(Model.Graph, firstID, secondID);
        }

        /// <summary>
        /// Очистка графа.
        /// </summary>
        public void ClearGraph()
        {
            Model.Clear();
        }

        /// <summary>
        /// Загрузка графа из данных.
        /// </summary>
        public void LoadGraph(List<VertexObjectData> vertices, List<EdgeObjectData> edges)
        {
            ClearGraph();

            foreach (var vertexData in vertices)
            {
                // Напрямую добавляем вершину в модель. Представление обновится автоматически по событию.
                Model.AddVertex(vertexData.Position);
            }

            foreach (var edgeData in edges)
            {
                // Напрямую добавляем ребро в модель.
                Model.AddEdge(edgeData.FirstVertexID, edgeData.SecondVertexID);
            }
        }

        // Предоставляем доступ к методам View для внешних систем (например, GraphPartSelector)
        public VertexDisplayObject GetVertexObject(int id) => _view.GetVertexObject(id);

        public EdgeDisplayObject GetEdgeObject(int firstID, int secondID) => _view.GetEdgeObject(firstID, secondID);

    }
}