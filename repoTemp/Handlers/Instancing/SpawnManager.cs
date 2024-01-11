using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour, INeedButton
{
    public UnityEvent creepSpawned;

    public SpawnerData spawnerData;
    public Transform pathingToLocation;
    
    public int activeCount, numToSpawn = 10, poolSize = 10;
    public float spawnRate = 0.3f, spawnDelay = 1.0f;

    private int _spawnedCount;

    private WaitForSeconds _waitForSpawnRate, _waitForSpawnDelay, _wfs;
    private List<GameObject> _pooledObjects;

    private void Start()
    {
        _waitForSpawnRate = new WaitForSeconds(spawnRate);
        _waitForSpawnDelay = new WaitForSeconds(spawnDelay);
        _wfs = new WaitForSeconds(1);
        spawnerData.ResetSpawner();
        StartCoroutine(delayPoolCreation());
    }

    public void StartSpawn(int amount)
    {
        numToSpawn = amount;
        StartCoroutine(DelaySpawn());
    }

    private IEnumerator delayPoolCreation()
    {
        yield return _wfs;
        CreatePool();
    }
    
    
    private void CreatePool()
    {
        _pooledObjects = new List<GameObject>();
        
        int totalPriority = spawnerData.prefabDataList.GetPriority();
        
        for (int i = 0; i < poolSize; i++)
        {
            int randomNumber = Random.Range(0, totalPriority);
            int sum = 0;
            foreach (CreepPrefabData creepPrefabData in spawnerData.prefabDataList.prefabDataList)
            {
                sum += creepPrefabData.priority;
                if (randomNumber < sum)
                {
                    GameObject obj = Instantiate(creepPrefabData.obj);
                    creepPrefabData.creepData.totalSpawned += 1;
                    _pooledObjects.Add(obj);
                    obj.GetComponent<NavAgentBehavior>().destination = pathingToLocation;
                    obj.SetActive(false);
                    break;
                }
            }
        }
    }

    private IEnumerator DelaySpawn()
    {
        yield return _waitForSpawnDelay;
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        _spawnedCount = 0;
        while (_spawnedCount < numToSpawn)
        {
            activeCount =  spawnerData.GetAliveCount();
            bool spawned = SpawnFromPool();
            if (!spawned)
            {
                UpdatePoolAndSpawn();
            }
            yield return _waitForSpawnRate;
        }
    }

    private bool SpawnFromPool()
    {
        bool spawn = false;
        foreach (var obj in _pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = transform.position;
                obj.transform.rotation = Quaternion.identity;
                obj.SetActive(true);
                obj.GetComponent<NavAgentBehavior>().Setup(pathingToLocation);
                _spawnedCount++;
                spawnerData.IncrementCreepsAliveCount();
                creepSpawned.Invoke();
                spawn = true;
                break;
            }
        }
        return spawn;
    }

    private void UpdatePoolAndSpawn()
    {
        int totalPriority = spawnerData.prefabDataList.GetPriority();
        int randomNumber = Random.Range(0, totalPriority);
        int sum = 0;
        foreach (CreepPrefabData creepPrefabData in spawnerData.prefabDataList.prefabDataList)
        {
            sum += creepPrefabData.priority;
            if (randomNumber < sum)
            {
                GameObject obj = Instantiate(creepPrefabData.obj);
                creepPrefabData.creepData.totalSpawned += 1;
                _pooledObjects.Add(obj);
                obj.transform.position = transform.position;
                obj.transform.rotation = Quaternion.identity;
                obj.GetComponent<NavAgentBehavior>().destination = pathingToLocation;
                obj.SetActive(true);
                obj.GetComponent<NavAgentBehavior>().Setup(pathingToLocation);
                _spawnedCount++;
                spawnerData.IncrementCreepsAliveCount();
                creepSpawned.Invoke();
                break;
            }
        }
    }
    
    public void ButtonAction()
    {
       StartSpawn(numToSpawn);
    }

    public string GetButtonName()
    {
        return "Spawn";
    }
}