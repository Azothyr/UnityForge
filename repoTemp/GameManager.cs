using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent startGameEvent, gameOverEvent, restartGameEvent;
    
    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        startGameEvent.Invoke();
    }

    public void GameOver()
    {
        gameOverEvent.Invoke();
    }

    public void RestartGame()
    {
        restartGameEvent.Invoke();
    }
}
