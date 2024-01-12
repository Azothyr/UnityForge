using UnityEngine;
using UnityEngine.Events;

public class GameTimeOperator : MonoBehaviour
{
    public UnityEvent onPauseEvent, onUnpauseEvent;
    public void Pause()
    {
        onPauseEvent.Invoke();
        Time.timeScale = 0;
    }
    
    public void Unpause()
    {
        Time.timeScale = 1;
        onUnpauseEvent.Invoke();
    }
}
