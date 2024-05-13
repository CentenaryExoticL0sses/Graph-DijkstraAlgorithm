using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GraphSaveSystem : MonoBehaviour
{
    [SerializeField] private GraphContainer _graphContainer;

    private string _saveFolder;

    private void Awake()
    {
        _saveFolder = Application.dataPath + "/SavedGraphs/";

        if (!Directory.Exists(_saveFolder))
        {
            Directory.CreateDirectory(_saveFolder);
        }
    }

    public void SaveGraph()
    {
        if( _graphContainer.Vertices.Count > 0)
        {
            List<VertexObjectData> newVertexData = new List<VertexObjectData>();
            List<EdgeObjectData> nedEdgeData = new List<EdgeObjectData>();

            foreach (var vertex in _graphContainer.Vertices)
            {
                newVertexData.Add(vertex.Data);
            }
            foreach (var edge in _graphContainer.Edges)
            {
                nedEdgeData.Add(edge.Data);
            }

            GraphData graphData = new GraphData()
            {
                VertexData = newVertexData,
                EdgeData = nedEdgeData
            };

            string json = JsonUtility.ToJson(graphData);
            File.WriteAllText(_saveFolder + "/SavedGraph.json", json);
        }
    }

    public void LoadGraph()
    {
        if(File.Exists(_saveFolder + "/SavedGraph.json"))
        {
            string savedData = File.ReadAllText(_saveFolder + "/SavedGraph.json");
            GraphData graphData = JsonUtility.FromJson<GraphData>(savedData);
            _graphContainer.LoadGraph(graphData.VertexData, graphData.EdgeData);
        }
    }

    private class GraphData
    {
        public List<VertexObjectData> VertexData;
        public List<EdgeObjectData> EdgeData;
    }
}
