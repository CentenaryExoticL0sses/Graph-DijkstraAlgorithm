using System.Collections.Generic;
using UnityEngine;
using GraphProject.Graphs;
using GraphProject.Graphs.View;

namespace GraphProject.Tools
{
    /// <summary>
    /// Класс, отвечающий за выделение части графа
    /// </summary>
    public class GraphPartSelector
    {
        public IReadOnlyList<int> SelectedVertices => _selectedVertices;

        public IReadOnlyList<(int, int)> SelectedEdges => _selectedEdges;

        private readonly GraphController _graphController;

        private readonly List<int> _selectedVertices;
        private readonly List<(int, int)> _selectedEdges;

        public GraphPartSelector(GraphController controller)
        {
            _graphController = controller;
            _selectedVertices = new List<int>();
            _selectedEdges = new List<(int FirstId, int SecondId)>();
        }

        //Получение вершины по нажатию на неё
        private VertexDisplayObject GetVertexAtPosition(Vector2 position)
        {
            var hit = Physics2D.Raycast(position, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out VertexDisplayObject vertex))
                {
                    return vertex;
                }
            }
            return null;
        }

        ///Выделение вершины
        public void SelectVertex(Vector2 position)
        {
            var vertex = GetVertexAtPosition(position);
            SelectVertex(vertex);
        }

        public void SelectVertex(VertexDisplayObject vertex)
        {
            if (vertex)
            {
                vertex.Select();
                _selectedVertices.Add(vertex.Data.ID);
            }
        }

        //Выделение ребра
        public void SelectEdge(EdgeDisplayObject edge)
        {
            if (edge)
            {
                edge.Select();
                _selectedEdges.Add((edge.Data.FirstVertexID, edge.Data.SecondVertexID));
            }
        }

        //Сброс всех выделенных вершин
        public void DeselectVertices()
        {
            foreach (var vertexId in _selectedVertices)
            {
                VertexDisplayObject vertex = _graphController.GetVertexObject(vertexId);
                if(vertex)
                {
                    vertex.Deselect();
                }
            }
            _selectedVertices.Clear();
        }

        //Сброс всех выделенных рёбер
        public void DeselectEdges()
        {
            foreach ((int FirstId, int SecondId) in _selectedEdges)
            {
                EdgeDisplayObject edge = _graphController.GetEdgeObject(FirstId, SecondId);
                if (edge)
                {
                    edge.Deselect();
                }
            }
            _selectedEdges.Clear();
        }
    }
}