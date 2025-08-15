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

        //Выбор режима работы инструмента
        public void CreateVertex()
        {
            CancelAnyState();
            _creationState = new VertexCreationState(_graphController, _partSelector);
        }

        public void CreateEdge()
        {
            CancelAnyState();
            _creationState = new EdgeCreationState(_graphController, _partSelector);
        }

        public void FindPath()
        {
            CancelAnyState();
            _creationState = new PathFindingState(_graphController, _partSelector);
        }

        //Сброс режима работы инструмента
        public void CancelAnyState()
        {
            if (_creationState != null)
            {
                _creationState.EndState();
                _creationState = null;
            }
        }
    }
}