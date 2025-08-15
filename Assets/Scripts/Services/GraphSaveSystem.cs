using System.Collections.Generic;
using System.IO;
using UnityEngine;
using GraphProject.Graphs;
using GraphProject.Graphs.Data;

namespace GraphProject.Services
{
    public class GraphSaveSystem : MonoBehaviour
    {
        // Меняем тип зависимости.
        private GraphController _graphController;

        private string _saveFolder;

        // Метод Initialize теперь принимает GraphController.
        public void Initialize(GraphController graphController)
        {
            _graphController = graphController;

            _saveFolder = Application.dataPath + "/SavedGraphs/";

            if (!Directory.Exists(_saveFolder))
            {
                Directory.CreateDirectory(_saveFolder);
            }
        }

        public void SaveGraph()
        {
            // Используем методы контроллера для получения визуальных объектов.
            if (_graphController.VertexObjects.Count > 0)
            {
                List<VertexObjectData> newVertexData = new();
                List<EdgeObjectData> newEdgeData = new();

                foreach (var vertex in _graphController.VertexObjects.Values)
                {
                    newVertexData.Add(vertex.Data);
                }
                foreach (var edge in _graphController.EdgeObjects)
                {
                    newEdgeData.Add(edge.Data);
                }

                GraphData graphData = new()
                {
                    VertexData = newVertexData,
                    EdgeData = newEdgeData
                };

                string json = JsonUtility.ToJson(graphData);
                File.WriteAllText(_saveFolder + "/SavedGraph.json", json);
            }
        }

        public void LoadGraph()
        {
            if (File.Exists(_saveFolder + "/SavedGraph.json"))
            {
                string savedData = File.ReadAllText(_saveFolder + "/SavedGraph.json");
                GraphData graphData = JsonUtility.FromJson<GraphData>(savedData);

                // Вызываем метод загрузки на контроллере.
                _graphController.LoadGraph(graphData.VertexData, graphData.EdgeData);
            }
        }

        private class GraphData
        {
            public List<VertexObjectData> VertexData;
            public List<EdgeObjectData> EdgeData;
        }
    }
}