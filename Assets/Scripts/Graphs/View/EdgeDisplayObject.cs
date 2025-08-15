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
        public EdgeObjectData Data { get; private set; }
        public bool IsSelected { get; private set; }

        [Range(0f, 1f)]
        public float LineWidth = 0.125f;

        public Color DefaultColor = Color.black;
        public Color SelectedColor = Color.red;

        [SerializeField, ReadOnlyInspector]
        private float _weight;

        private LineRenderer _lineRenderer;

        public void Initialize(EdgeObjectData data, float weight, Vector2 firstPosition, Vector2 secondPosition)
        {
            Data = data;
            _weight = weight;
            name = $"Edge{data.FirstVertexID}{data.SecondVertexID}";
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.SetPosition(0, firstPosition);
            _lineRenderer.SetPosition(1, secondPosition);
            _lineRenderer.startWidth = LineWidth;
            _lineRenderer.endWidth = LineWidth;
            SetColor(DefaultColor);
            IsSelected = false;
        }

        public void Select()
        {
            IsSelected = true;
            SetColor(SelectedColor);
        }

        public void Deselect()
        {
            IsSelected = false;
            SetColor(DefaultColor);
        }

        private void SetColor(Color color)
        {
            _lineRenderer.startColor = color;
            _lineRenderer.endColor = color;
        }
    }
}