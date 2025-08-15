using UnityEngine;

namespace GraphProject.Tools.States
{
    public interface ICreationState
    {
        public void EnterState();
        public void OnAction(Vector2 position);
        public void ExitState();
    }
}