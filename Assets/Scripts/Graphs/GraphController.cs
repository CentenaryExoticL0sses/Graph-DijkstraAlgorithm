using System.Collections.Generic;
using UnityEngine;
using GraphProject.Graphs.Data;
using GraphProject.Graphs.View;
using GraphProject.Services;

namespace GraphProject.Graphs
{
    /// <summary>
    /// ���������� �����. �������� ������ ����� ��� ���� �������� � ������.
    /// ��������� ������ (GraphModel) � ������������� (GraphView).
    /// </summary>
    public class GraphController : MonoBehaviour
    {
        [SerializeField] private GraphView _view; // ������ �� ������������� �����

        public GraphModel Model { get; private set; }
        private PathfindingService _pathfindingService;

        /// <summary>
        /// ������������� �����������. ���������� �� AppManager.
        /// </summary>
        public void Initialize()
        {
            Model = new GraphModel();
            _pathfindingService = new PathfindingService();

            // �������� ������ � �������������, ����� ��� ����� ����������� �� �������.
            _view.Initialize(Model);
        }

        /// <summary>
        /// �������� ����� �������.
        /// </summary>
        /// <param name="position">������� ��� ����� �������.</param>
        /// <returns>���������� ������ ��������� �������.</returns>
        public VertexDisplayObject CreateVertex(Vector2 position)
        {
            var vertexData = Model.AddVertex(position);
            return _view.GetVertexObject(vertexData.ID);
        }

        /// <summary>
        /// �������� ������ �����.
        /// </summary>
        /// <param name="firstID">ID ������ �������.</param>
        /// <param name="secondID">ID ������ �������.</param>
        /// <returns>���������� ������ ���������� �����.</returns>
        public EdgeDisplayObject CreateEdge(int firstID, int secondID)
        {
            var edgeData = Model.AddEdge(firstID, secondID);
            if (edgeData.Equals(default(EdgeObjectData)))
            {
                // ���� ����� �� ���� ������� (��� ����������), ���������� ������������ ������.
                return GetEdgeObject(firstID, secondID);
            }
            return _view.GetEdgeObject(edgeData.FirstVertexID, edgeData.SecondVertexID);
        }

        /// <summary>
        /// ����� ����������� ����.
        /// </summary>
        public List<int> FindShortestPath(int firstID, int secondID)
        {
            return _pathfindingService.FindShortestPath(Model.Graph, firstID, secondID);
        }

        /// <summary>
        /// ������� �����.
        /// </summary>
        public void ClearGraph()
        {
            Model.Clear();
        }

        /// <summary>
        /// �������� ����� �� ������.
        /// </summary>
        public void LoadGraph(List<VertexObjectData> vertices, List<EdgeObjectData> edges)
        {
            ClearGraph();

            foreach (var vertexData in vertices)
            {
                // �������� ��������� ������� � ������. ������������� ��������� ������������� �� �������.
                Model.AddVertex(vertexData.Position);
            }

            foreach (var edgeData in edges)
            {
                // �������� ��������� ����� � ������.
                Model.AddEdge(edgeData.FirstVertexID, edgeData.SecondVertexID);
            }
        }

        // ������������� ������ � ������� View ��� ������� ������ (��������, GraphPartSelector)
        public VertexDisplayObject GetVertexObject(int id) => _view.GetVertexObject(id);

        public EdgeDisplayObject GetEdgeObject(int firstID, int secondID) => _view.GetEdgeObject(firstID, secondID);

        public IReadOnlyCollection<VertexDisplayObject> GetAllVertexObjects() => _view.GetAllVertexObjects();

        public IReadOnlyList<EdgeDisplayObject> GetAllEdgeObjects() => _view.GetAllEdgeObjects();

    }
}