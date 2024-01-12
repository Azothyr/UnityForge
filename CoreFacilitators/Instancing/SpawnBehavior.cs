using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class SpawnBehavior : MonoBehaviour
{
    public UnityEvent onSpawnEvent, endSpawnEvent;
    [SerializeField] private bool switchOff;
    public Instancer instancer;
    public Vector3Data spawnPointCenterV3, spawnLocation;
    public BoolData canRun;
    public IntData spawnCount, enemiesAlive, roundNum, roundSpawnIncrease, spawnBase, difficultySpawnModifier;
    public FloatData timeElapsed, roundSpawnModifier;
    public float distanceMin, distanceMax, spawnDelay;

    private float lowerRangeMin, upperRangeMax, lowerRangeMax, upperRangeMin, num1, num2, num3;
    private int timeDifficultyModifier, roundModifier, count;
    private Vector3 randomV3, playerLoc;
    private WaitForFixedUpdate wffuObj = new WaitForFixedUpdate();
    private WaitForSeconds wfsObj;

    private void Awake()
    {
        wfsObj = new WaitForSeconds(spawnDelay);
    }

    public void StartSpawning()
    {
        if (!switchOff)
        {
            GenerateSpawnCount();
            StartCoroutine(Spawn());
        }
        else
        {
            Debug.Log("WARNING: Spawner has been manually turned off...");
        }
    }    
    
    public void StopSpawning()
    {
        StopCoroutine(Spawn());
    }
    
    private Vector3 GenerateSpawnV3Value(float rangeFromObjectMin, float rangeFromObjectMax)
    {
        
        lowerRangeMin = (-1 * rangeFromObjectMax);
        upperRangeMax = rangeFromObjectMax;
        lowerRangeMax = (-1 * rangeFromObjectMin);
        upperRangeMin = rangeFromObjectMin;
        
        while (!(num1 >= lowerRangeMin && num1 <= lowerRangeMax || 
                 num1 >= upperRangeMin && num1 <= upperRangeMax))
        {
            num1 = Random.Range(lowerRangeMin, upperRangeMax);
        }       
        
        while (!(num3 >= lowerRangeMin && num3 <= lowerRangeMax || 
                 num3 >= upperRangeMin && num3 <= upperRangeMax))
        {
            num3 = Random.Range(lowerRangeMin, upperRangeMax);
        }
        
        num1 += spawnPointCenterV3.x;
        num2 += spawnPointCenterV3.y;
        num3 += spawnPointCenterV3.z;
        
        randomV3 = new Vector3(num1, num2, num3);

        num1 = new float();
        num2 = new float();
        num3 = new float();
        
        return randomV3;
    }

    private void GenerateSpawnCount()
    {
        if (roundNum.value == 1)
        {
            spawnCount.SetValue(spawnBase);
        }
        else if (roundNum.value > 1)
        {
            timeDifficultyModifier = (int) ((timeElapsed.value / 60) * difficultySpawnModifier.value);
            roundModifier = (int) ((roundNum.value * roundSpawnIncrease.value) * roundSpawnModifier.value);
            count = spawnBase.value + timeDifficultyModifier + roundModifier;
            spawnCount.SetValue(count);
        }
    }
    private IEnumerator Spawn()
    {
        while (canRun.value && spawnCount.value > 0)
        {
            spawnLocation.Set(GenerateSpawnV3Value(distanceMin, distanceMax));
            instancer.CreateInstance(spawnLocation);
                
            enemiesAlive.UpdateValue(1);
            spawnCount.UpdateValue(-1);
            onSpawnEvent.Invoke();
            yield return wfsObj;
        }
        endSpawnEvent.Invoke();
    }
}
