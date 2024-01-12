using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RBEnemyController : RBControllerBase
{
    public Vector3Data playerV3;
    public UnityEvent onGameOverEvent;

    private EnemyData enemyData;

    private Vector3 playerLocation;
    
    protected override void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        
        enemyData = controllerData as EnemyData;
        
        if (enemyData != null)
        {
            speed = enemyData.speed.value + (enemyData.speed.value * enemyData.speedDelta / 100.0f);
        }
        else
        {
            Debug.LogError("EnemyData not found in controllerData.");
        }
        
        StartMovement();
    }
    
    public override void TriggerDeathEvent()
    {
        deathTriggerEvent.Invoke();
    }

    public void PassScoreValue(IntData scoreContainer)
    {
        scoreContainer.value += enemyData.scoreValue;
    }

    public void PassMonetaryValue(IntData wealthContainer)
    {
        wealthContainer.value += enemyData.currencyValue;
    }
    
    protected override IEnumerator Move()
    {
        while (controllerData.canRun.value)
        {
            SetCurrentV3();
            
            playerLocation = playerV3.value;
            moveDirection = (playerLocation - transform.position).normalized;
            rigidBody.AddForce(moveDirection * speed, ForceMode.Acceleration);

            if (rigidBody.position.y > 0)
            {
                rigidBody.position = new Vector3(currentLocation.x, 0, currentLocation.z);
            }

            yield return wffuObj;
        }
        GameOverCheck();
    }

    private void GameOverCheck()
    {
        if (controllerData.gameOver.value)
        {
            onGameOverEvent.Invoke();
        }
    }
}

