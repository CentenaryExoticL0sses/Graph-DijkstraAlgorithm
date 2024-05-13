using UnityEngine;

public class EdgeCreationState : ICreationState
{
    private GraphContainer _graphContainer;
    private GraphPartSelector _partSelector;

    public EdgeCreationState(GraphContainer container, GraphPartSelector selector)
    {
        _partSelector = selector;
        _graphContainer = container;
    }

    public void OnAction(Vector2 position)
    {
        if(_partSelector.SelectedVertices.Count >= 2 || _partSelector.SelectedEdges.Count == 1)
        {
            _partSelector.DeselectVertices();
            _partSelector.DeselectEdges();
        }

        _partSelector.SelectVertex(position);
        if(_partSelector.SelectedVertices.Count >= 2)
        {
            var edge = _graphContainer.CreateEdge(_partSelector.SelectedVertices[0].Data.ID, _partSelector.SelectedVertices[1].Data.ID);
            _partSelector.SelectEdge(edge);
        }
    }

    public void EndState()
    {
        _partSelector.DeselectVertices();
        _partSelector.DeselectEdges();
    }
}
