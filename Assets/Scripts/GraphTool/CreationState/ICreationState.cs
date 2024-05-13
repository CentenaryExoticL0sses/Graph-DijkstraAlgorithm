using UnityEngine;

public interface ICreationState
{
    void OnAction(Vector2 position);
    void EndState();
}
