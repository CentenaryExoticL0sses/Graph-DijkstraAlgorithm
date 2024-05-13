using System.Collections.Generic;
using UnityEngine;

public class PathFindingState : ICreationState
{
    private GraphContainer _graphContainer;
    private GraphPartSelector _partSelector;

    public PathFindingState(GraphContainer container, GraphPartSelector selector)
    {
        _partSelector = selector;
        _graphContainer = container;
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
            List<int> path = _graphContainer.FindShortestPath(_partSelector.SelectedVertices[0].Data.ID, _partSelector.SelectedVertices[1].Data.ID);
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
            var vertex = _graphContainer.GetVertexObject(path[i]);
            _partSelector.SelectVertex(vertex);
            if(i > 0)
            {
                var edge = _graphContainer.GetEdgeObject(path[i-1], path[i]);
                _partSelector.SelectEdge(edge);
            }
        }
    }
}
