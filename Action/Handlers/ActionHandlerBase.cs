using UnityEngine;
using UnityEngine.Events;

public class ActionHandlerBase : MonoBehaviour
{
    public GameAction action;
    public UnityEvent response;

    private void OnEnable()
    {
        action.Raise += OnEventRaised;
    }

    private void OnDisable()
    {
        action.Raise -= OnEventRaised;
    }

    private void OnEventRaised(GameAction passedAction)
    {
        response.Invoke();
    }
}
