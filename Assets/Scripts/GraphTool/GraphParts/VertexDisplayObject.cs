using TMPro;
using UnityEngine;

//Визуальное отображение вершины графа
[RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]
public class VertexDisplayObject : MonoBehaviour
{
    public VertexObjectData Data { get; private set; }
    public bool IsSelected { get; private set; }

    public Color DefaultColor = Color.black;
    public Color SelectedColor = Color.red;

    private SpriteRenderer _renderer;

    public void Initialize(VertexObjectData data)
    {
        Data = data;
        name = $"Vertex{data.ID}";

        var labelText = GetComponentInChildren<TextMeshPro>();
        if(labelText == null)
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
        _renderer.color = DefaultColor;
        IsSelected = false;
    }

    public void Select()
    {
        IsSelected = true;
        _renderer.color = SelectedColor;
    }

    public void Deselect()
    {
        IsSelected = false;
        _renderer.color = DefaultColor;
    }
}
