using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

[System.Serializable] // This makes GameActionEvent visible in the inspector.
public class GameActionEvent
{
    public GameAction actionObj;
    public UnityEvent onRaiseEvent;
}

[DisallowMultipleComponent]
public class ActionHandler: MonoBehaviour
{
    // This list allows adding multiple GameActionEvent objects from the inspector.
    public List<GameActionEvent> gameActions;

    private void OnEnable()
    {
        // Subscribe to all the events in the list.
        foreach (var gameAction in gameActions)
        {
            gameAction.actionObj.Raise += Raise;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from all the events in the list.
        foreach (var gameAction in gameActions)
        {
            gameAction.actionObj.Raise -= Raise;
        }
    }

    private void Raise(GameAction callingObj)
    {
        // Find the first matching GameAction
        var gameAction = gameActions.FirstOrDefault(action => action.actionObj == callingObj);

        // If found, invoke its onRaiseEvent
        gameAction?.onRaiseEvent.Invoke();
    }
}