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
            if (_graphController == null || _graphCreationTool == null || _graphSaveSystem == null)
            {
                Debug.LogError("Не все зависимости были установлены в инспекторе для AppManager.");
                return;
            }

            // Инициализируем контроллер графа первым.
            _graphController.Initialize();

            // Передаем контроллер в зависимые системы.
            _graphCreationTool.Initialize(_graphController);
            _graphSaveSystem.Initialize(_graphController);
        }
    }
}