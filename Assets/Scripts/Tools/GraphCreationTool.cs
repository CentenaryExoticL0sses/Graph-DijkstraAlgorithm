using GraphProject.Graphs;
using GraphProject.Tools.States;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace GraphProject.Tools
{
    public class GraphCreationTool : MonoBehaviour
    {
        private GraphController _graphController;

        private GraphPartSelector _partSelector;
        private ICreationState _creationState;
        private GraphActions _actions;
        private bool _isOverUI;

        public void Initialize(GraphController graphController)
        {
            _creationState = null;
            _partSelector = new GraphPartSelector(graphController);
            _graphController = graphController;

        }

        private void OnEnable()
        {
            _actions = new GraphActions();
            _actions.Tool.MouseAction.performed += OnMouseAction;
            _actions.Tool.Cancel.performed += OnCancel;
            _actions.Tool.Enable();
        }

        private void OnDisable()
        {
            _actions.Tool.MouseAction.performed -= OnMouseAction;
            _actions.Tool.Cancel.performed -= OnCancel;
            _actions.Tool.Disable();
        }

        private void OnCancel(InputAction.CallbackContext context)
        {
            CancelAnyState();
        }

        //Выполнение функции выбранного режима по нажатию ЛКМ
        private void OnMouseAction(InputAction.CallbackContext context)
        {
            if (_isOverUI)
                return;
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            if (_creationState != null)
            {
                _creationState.OnAction(worldPosition);
            }
        }

        private void Update()
        {
            if (_creationState != null)
                _isOverUI = EventSystem.current.IsPointerOverGameObject();
        }

        /// <summary>
        /// Активация режима создания вершин.
        /// </summary>
        public void CreateVertex() => SetState(new VertexCreationState(_graphController, _partSelector));

        /// <summary>
        /// Активация режима создания рёбер.
        /// </summary>
        public void CreateEdge() => SetState(new EdgeCreationState(_graphController, _partSelector));

        /// <summary>
        /// Активация режима поиска пути.
        /// </summary>
        public void FindPath() => SetState(new PathFindingState(_graphController, _partSelector));

        /// <summary>
        /// Сброс режима работы инструмента
        /// </summary>
        public void CancelAnyState() => SetState(null);

        private void SetState(ICreationState state)
        {
            _creationState?.ExitState();
            _creationState = state;
            _creationState?.EnterState();
        }
    }
}