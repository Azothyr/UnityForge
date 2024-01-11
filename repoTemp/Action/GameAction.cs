using UnityEngine;

[CreateAssetMenu]
public class GameAction : ScriptableObject
{
    // Define a delegate that takes GameAction as a parameter.
    public delegate void GameActionEvent(GameAction action);
    
    // Create an event of type GameActionEvent.
    public event GameActionEvent raise;

    public void RaiseAction()
    {
        // When raising the event, pass 'this' to provide the GameAction instance to the listeners.
        raise?.Invoke(this);
    }
}