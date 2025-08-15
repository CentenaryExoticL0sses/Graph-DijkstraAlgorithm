using GraphProject.Graphs.Data;
using UnityEngine;

namespace GraphProject.Graphs.View
{

    /// <summary>
    /// Визуальное отображение ребра графа
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class EdgeDisplayObject : MonoBehaviour
    {
        [Header("Настройки")]
        [SerializeField] private Color _defaultColor = Color.black;
        [SerializeField] private Color _selectedColor = Color.red;
        [SerializeField, Range(0f, 1f)] private float _lineWidth = 0.125f;

        [Header("Отладка")]
        [SerializeField, ReadOnlyInspector] private float _weight;

        public EdgeObjectData Data { get; private set; }
        public bool IsSelected { get; private set; }


        private LineRenderer _lineRenderer;

        public void Initialize(EdgeObjectData data, float weight, Vector2 firstPosition, Vector2 secondPosition)
        {
            Data = data;
            _weight = weight;
            name = $"Edge{data.FirstVertexID}{data.SecondVertexID}";
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.SetPosition(0, firstPosition);
            _lineRenderer.SetPosition(1, secondPosition);
            _lineRenderer.startWidth = _lineWidth;
            _lineRenderer.endWidth = _lineWidth;
            SetColor(_defaultColor);
            IsSelected = false;
        }

        public void Select()
        {
            IsSelected = true;
            SetColor(_selectedColor);
        }

        public void Deselect()
        {
            IsSelected = false;
            SetColor(_defaultColor);
        }

        private void SetColor(Color color)
        {
            _lineRenderer.startColor = color;
            _lineRenderer.endColor = color;
        }
    }
}