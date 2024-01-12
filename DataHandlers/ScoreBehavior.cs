using UnityEngine;

public class ScoreBehavior : MonoBehaviour
{
    public IntData score;
    public EnemyData enemyData;

    private int increaseAmount;
    
    private void OnEnable()
    {
        increaseAmount = enemyData.scoreValue;
        Debug.Log(increaseAmount);
    }
    
    public void IncreaseScore()
    {
        score.value += increaseAmount;
        Debug.Log(score.value);
    }
}
