using GraphProject.Graphs;
using GraphProject.Services;
using GraphProject.Tools;
using UnityEngine;

namespace GraphProject.Management
{
    public class Bootstrap : MonoBehaviour
    {
        [Header("Компоненты")]
        [SerializeField] private GraphController _graphController;
        [SerializeField] private GraphCreationTool _graphCreationTool;
        [SerializeField] private GraphSaveSystem _graphSaveSystem;

        private void Awake()
        {
            _graphController.Initialize();
            _graphCreationTool.Initialize(_graphController);
            _graphSaveSystem.Initialize(_graphController);
        }
    }
}