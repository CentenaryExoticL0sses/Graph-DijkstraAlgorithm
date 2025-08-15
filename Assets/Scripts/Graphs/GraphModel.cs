using System;
using System.Collections.Generic;
using GraphProject.Core.DataStructures;
using GraphProject.Graphs.Data;

namespace GraphProject.Graphs
{
    /// <summary>
    /// ������ ������ �����. ������ � ��������� ���������� ���������� �����.
    /// �� ������� �� Unity � ����������� �������������.
    /// </summary>
    public class GraphModel
    {
        // ������� ��� ����������� �� ���������� � �����.
        // �� ��� ���������� GraphView, ����� �������� ������������.
        public event Action<VertexObjectData> OnVertexAdded;
        public event Action<EdgeObjectData, float> OnEdgeAdded;
        public event Action OnGraphCleared;

        // ������ �� ��� ���������� ����.
        public Graph Graph { get; private set; }

        // ������ ������ ��� ������������ ������ � �������.
        private readonly Dictionary<int, VertexObjectData> _vertexData = new Dictionary<int, VertexObjectData>();
        private readonly List<EdgeObjectData> _edgeData = new List<EdgeObjectData>();

        public GraphModel()
        {
            Graph = new Graph();
        }

        /// <summary>
        /// ��������� ����� ������� � ����.
        /// </summary>
        /// <param name="position">������� ��� ����� �������.</param>
        /// <returns>������ � ��������� �������.</returns>
        public VertexObjectData AddVertex(UnityEngine.Vector2 position)
        {
            int id = Graph.Vertices.Count;
            Graph.AddVertex(id);

            var data = new VertexObjectData(id, position);
            _vertexData[id] = data;

            // ���������� ����������� � ���������� ����� �������.
            OnVertexAdded?.Invoke(data);
            return data;
        }

        /// <summary>
        /// ��������� ����� ����� ����� ���������.
        /// </summary>
        /// <param name="firstID">ID ������ �������.</param>
        /// <param name="secondID">ID ������ �������.</param>
        /// <returns>������ � ��������� ����� ��� null, ���� ����� ��� ����������.</returns>
        public EdgeObjectData AddEdge(int firstID, int secondID)
        {
            if (Graph.FindVertex(firstID) == null || Graph.FindVertex(secondID) == null)
                return default;

            // ���������, ���������� �� ��� ����� �����.
            foreach (var edge in _edgeData)
            {
                if (edge.FirstVertexID == firstID && edge.SecondVertexID == secondID ||
                    edge.FirstVertexID == secondID && edge.SecondVertexID == firstID)
                {
                    return default; // ����� ��� ����������
                }
            }

            var firstVertexPos = _vertexData[firstID].Position;
            var secondVertexPos = _vertexData[secondID].Position;
            float weight = UnityEngine.Vector2.Distance(firstVertexPos, secondVertexPos);

            Graph.AddEdge(firstID, secondID, weight);

            var data = new EdgeObjectData(firstID, secondID);
            _edgeData.Add(data);

            // ���������� ����������� � ���������� ������ �����.
            OnEdgeAdded?.Invoke(data, weight);
            return data;
        }

        /// <summary>
        /// ������� ��� ������ �����.
        /// </summary>
        public void Clear()
        {
            Graph = new Graph();
            _vertexData.Clear();
            _edgeData.Clear();

            // ���������� ����������� � ������ ������� �����.
            OnGraphCleared?.Invoke();
        }

        public IReadOnlyDictionary<int, VertexObjectData> GetAllVertexData() => _vertexData;
        public IReadOnlyList<EdgeObjectData> GetAllEdgeData() => _edgeData;
    }
}