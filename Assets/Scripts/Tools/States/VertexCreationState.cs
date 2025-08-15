using UnityEngine;
using GraphProject.Graphs;

namespace GraphProject.Tools.States
{
    public class VertexCreationState : ICreationState
    {
        private readonly GraphController _graphController;
        private readonly GraphPartSelector _partSelector;

        public VertexCreationState(GraphController controller, GraphPartSelector selector)
        {
            _partSelector = selector;
            _graphController = controller;
        }

        public void EnterState() { }

        public void OnAction(Vector2 position)
        {
            if (_partSelector.SelectedVertices.Count > 0)
            {
                _partSelector.DeselectVertices();
            }
            var vertex = _graphController.CreateVertex(position);
            _partSelector.SelectVertex(vertex);
        }

        public void ExitState()
        {
            _partSelector?.DeselectVertices();
        }

    }
}