using GraphProject.Graphs;
using GraphProject.Services;
using GraphProject.Tools;
using UnityEngine;

namespace GraphProject.Management
{
    public class Bootstrap : MonoBehaviour
    {
        [Header("����������")]
        [SerializeField] private GraphController _graphController;
        [SerializeField] private GraphCreationTool _graphCreationTool;
        [SerializeField] private GraphSaveSystem _graphSaveSystem;

        private void Awake()
        {
            if (_graphController == null || _graphCreationTool == null || _graphSaveSystem == null)
            {
                Debug.LogError("�� ��� ����������� ���� ����������� � ���������� ��� AppManager.");
                return;
            }

            // �������������� ���������� ����� ������.
            _graphController.Initialize();

            // �������� ���������� � ��������� �������.
            _graphCreationTool.Initialize(_graphController);
            _graphSaveSystem.Initialize(_graphController);
        }
    }
}