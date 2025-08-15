using UnityEngine;
using GraphProject.Graphs;

namespace GraphProject.Tools.States
{
    public class EdgeCreationState : ICreationState
    {
        private readonly GraphController _graphController;
        private readonly GraphPartSelector _partSelector;

        public EdgeCreationState(GraphController controller, GraphPartSelector selector)
        {
            _partSelector = selector;
            _graphController = controller;
        }

        public void EnterState() { }

        public void OnAction(Vector2 position)
        {
            if (_partSelector.SelectedVertices.Count >= 2 || _partSelector.SelectedEdges.Count == 1)
            {
                _partSelector.DeselectVertices();
                _partSelector.DeselectEdges();
            }

            _partSelector.SelectVertex(position);
            if (_partSelector.SelectedVertices.Count >= 2)
            {
                var edge = _graphController.CreateEdge(_partSelector.SelectedVertices[0], _partSelector.SelectedVertices[1]);
                _partSelector.SelectEdge(edge);
            }
        }

        public void ExitState()
        {
            _partSelector.DeselectVertices();
            _partSelector.DeselectEdges();
        }
    }
}