using UnityEngine;

public class VertexCreationState : ICreationState
{
    private GraphContainer _graphContainer;
    private GraphPartSelector _partSelector;

    public VertexCreationState(GraphContainer container, GraphPartSelector selector)
    {
        _partSelector = selector;
        _graphContainer = container;
    }

    public void OnAction(Vector2 position)
    {
        if(_partSelector.SelectedVertices.Count > 0)
        {
            _partSelector.DeselectVertices();
        }
        var vertex = _graphContainer.CreateVertex(position);
        _partSelector.SelectVertex(vertex);
    }

    public void EndState()
    {
        _partSelector?.DeselectVertices();
    }

}
