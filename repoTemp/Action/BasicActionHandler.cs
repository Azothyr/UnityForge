using UnityEngine;
using UnityEngine.Events;

public class BasicActionHandler : MonoBehaviour
{
    public GameAction action;
    public UnityEvent response;

    private void OnEnable()
    {
        action.raise += OnEventRaised;
    }

    private void OnDisable()
    {
        action.raise -= OnEventRaised;
    }

    private void OnEventRaised(GameAction passedAction)
    {
        response.Invoke();
    }
}
