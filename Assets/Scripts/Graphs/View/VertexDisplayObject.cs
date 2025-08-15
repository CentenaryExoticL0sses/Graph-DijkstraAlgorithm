using GraphProject.Graphs.Data;
using TMPro;
using UnityEngine;

namespace GraphProject.Graphs.View
{

    /// <summary>
    /// Визуальное отображение вершины графа
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]
    public class VertexDisplayObject : MonoBehaviour
    {
        [Header("Настройки")]
        [SerializeField] private Color _defaultColor = Color.black;
        [SerializeField] private Color _selectedColor = Color.red;

        public VertexObjectData Data { get; private set; }
        public bool IsSelected { get; private set; }

        private SpriteRenderer _renderer;

        public void Initialize(VertexObjectData data)
        {
            Data = data;
            name = $"Vertex{data.ID}";

            var labelText = GetComponentInChildren<TextMeshPro>();
            if (labelText == null)
            {
                GameObject label = new GameObject("Label");
                labelText = label.AddComponent<TextMeshPro>();
                labelText.enableAutoSizing = true;
                labelText.fontSizeMin = 2;
                labelText.fontSizeMax = 8;
                labelText.alignment = TextAlignmentOptions.Center;
                var labelTransform = label.GetComponent<RectTransform>();
                labelTransform.SetParent(transform);
                labelTransform.sizeDelta = new Vector2(0.7f, 0.7f);
            }
            labelText.text = data.ID.ToString();
            _renderer = GetComponent<SpriteRenderer>();
            _renderer.color = _defaultColor;
            IsSelected = false;
        }

        public void Select()
        {
            IsSelected = true;
            _renderer.color = _selectedColor;
        }

        public void Deselect()
        {
            IsSelected = false;
            _renderer.color = _defaultColor;
        }
    }
}