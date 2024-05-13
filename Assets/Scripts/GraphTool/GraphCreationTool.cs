using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GraphCreationTool : MonoBehaviour
{
    [SerializeField]
    private GraphContainer _graphContainer;

    private GraphPartSelector _partSelector;
    private ICreationState _creationState;
    private GraphActions _actions;
    private bool _isOverUI;

    public void Initialize()
    {
        _creationState = null;
        _partSelector = new GraphPartSelector();
        if(_graphContainer == null)
        {
            _graphContainer = new GameObject("GraphContainer").AddComponent<GraphContainer>();
            _graphContainer.Initialize();
        }

    }

    private void OnEnable()
    {
        _actions = new GraphActions();
        _actions.Tool.MouseAction.performed += GraphMouseAction;
        _actions.Tool.Cancel.performed += CancelMode;
        _actions.Tool.Enable();
    }

    private void OnDisable()
    {
        _actions.Tool.MouseAction.performed -= GraphMouseAction;
        _actions.Tool.Cancel.performed -= CancelMode;
        _actions.Tool.Disable();
    }

    private void CancelMode(InputAction.CallbackContext context)
    {
        CancelAnyState();
    }

    //Выполнение функции выбранного режима по нажатию ЛКМ
    private void GraphMouseAction(InputAction.CallbackContext context)
    {
        if (_isOverUI)
            return;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        if(_creationState != null)
        {
            _creationState.OnAction(worldPosition);
        }
    }

    private void Update()
    {
        if(_creationState != null)
            _isOverUI = EventSystem.current.IsPointerOverGameObject();
    }

    //Выбор режима работы инструмента
    public void CreateVertex()
    {
        CancelAnyState();
        _creationState = new VertexCreationState(_graphContainer, _partSelector);
    }

    public void CreateEdge()
    {
        CancelAnyState();
        _creationState = new EdgeCreationState(_graphContainer, _partSelector);
    }

    public void FindPath()
    {
        CancelAnyState();
        _creationState = new PathFindingState(_graphContainer, _partSelector);
    }

    //Сброс режима работы инструмента
    public void CancelAnyState()
    {
        if(_creationState != null)
        {
            _creationState.EndState();
            _creationState = null;
        }
    }
}
