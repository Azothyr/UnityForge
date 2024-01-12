using UnityEngine;

public class CurrencyBehavior : MonoBehaviour
{
    public IntData wealth;
    public EnemyData enemyData;

    private int increaseAmount;
    
    private void OnEnable()
    {
        increaseAmount = enemyData.currencyValue;
        Debug.Log(increaseAmount);
    }
    
    public void IncreaseWeatlh()
    {
        wealth.value += increaseAmount;
        Debug.Log(wealth.value);
    }
}
