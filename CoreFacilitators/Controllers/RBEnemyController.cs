using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RbEnemyController : RbControllerBase
{
    public Vector3Data navigationTarget;
    public UnityEvent onGameOverEvent;
    public bool moveOnEnable;

    private EnemyData _enemyData;
    
    protected override void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        
        _enemyData = controllerData as EnemyData;
        
        if (_enemyData != null)
        {
            speed = _enemyData.speed.value + (_enemyData.speed.value * _enemyData.speedDelta / 100.0f);
        }
        else
        {
            Debug.LogError("EnemyData not found in controllerData.");
        }
    }
    
    private void OnEnable()
    {
        if (moveOnEnable)
        {
            StartMovement();
        }
    }
    
    public override void TriggerDeathEvent()
    {
        deathTriggerEvent.Invoke();
    }

    public void PassScoreValue(IntData scoreContainer)
    {
        scoreContainer.value += _enemyData.scoreValue;
    }

    public void PassMonetaryValue(IntData wealthContainer)
    {
        wealthContainer.value += _enemyData.currencyValue;
    }

    protected override IEnumerator Move()
    {
        // Maximum velocity
        float maxVelocity = 10f;

        while (controllerData.canRun.value)
        {
            SetCurrentV3();

            var goalPosition = navigationTarget.value;
            moveDirection = (goalPosition - transform.position).normalized;

            // Check if current speed is less than maxVelocity
            if (rigidBody.velocity.magnitude < maxVelocity)
            {
                rigidBody.AddForce(moveDirection * speed, ForceMode.Acceleration);
            }

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

