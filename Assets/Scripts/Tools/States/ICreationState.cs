using UnityEngine;

namespace GraphProject.Tools.States
{
    public interface ICreationState
    {
        void OnAction(Vector2 position);
        void EndState();
    }
}