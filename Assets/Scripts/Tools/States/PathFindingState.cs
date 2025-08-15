using System.Collections.Generic;
using UnityEngine;
using GraphProject.Graphs;
using GraphProject.Graphs.View;

namespace GraphProject.Tools.States
{
    public class PathFindingState : ICreationState
    {
        private readonly GraphController _graphController;
        private readonly GraphPartSelector _partSelector;

        public PathFindingState(GraphController controller, GraphPartSelector selector)
        {
            _partSelector = selector;
            _graphController = controller;
        }

        public void OnAction(Vector2 position)
        {
            if (_partSelector.SelectedVertices.Count >= 2)
            {
                _partSelector.DeselectVertices();
                _partSelector.DeselectEdges();
            }

            _partSelector.SelectVertex(position);
            if (_partSelector.SelectedVertices.Count >= 2)
            {
                List<int> path = _graphController.FindShortestPath(_partSelector.SelectedVertices[0], _partSelector.SelectedVertices[1]);
                SelectPath(path);
            }
        }

        public void EndState()
        {
            _partSelector.DeselectVertices();
            _partSelector.DeselectEdges();
        }

        private void SelectPath(List<int> path)
        {
            for (int i = 0; i < path.Count; i++)
            {
                var vertex = _graphController.GetVertexObject(path[i]);
                _partSelector.SelectVertex(vertex);
                if (i > 0)
                {
                    var edge = _graphController.GetEdgeObject(path[i - 1], path[i]);
                    _partSelector.SelectEdge(edge);
                }
            }
        }
    }
}