using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static ZpTools.UtilityFunctions;
using Random = UnityEngine.Random;


public class SpawnManager : MonoBehaviour, INeedButton
{
    private bool _destroying;
    public bool allowDebug, allowMultipleSpawnInstances;

    public UnityEvent onSpawn, onSpawningComplete, onFinalSpawnDefeated;

    public SpawnerData spawnerData;
    public bool usePriority, spawnOnStart, randomizeSpawnRate;

    [System.Serializable]
    public class Spawner
    {
        public string spawnerID;
        public Transform spawnLocation, pathingTarget;
        private int _currentSpawnCount;
        public int activeSpawnLimit;

        public int GetAliveCount() { return _currentSpawnCount; }
        public void IncrementCount() { _currentSpawnCount++; }
        public void DecrementCount() { _currentSpawnCount--; }
    }
    public List<Spawner> spawners = new();

    public float poolCreationDelay = 1.0f, spawnDelay = 1.0f, spawnRateMin = 1.0f, spawnRateMax = 1.0f;
    public int numToSpawn = 10;
    [HideInInspector] public int waitingCount;

    private int _poolSize;

    private int poolSize
    {
        get
        {
            int totalPoolSize = 0;
            foreach (var spawner in spawners)
            {
                totalPoolSize += spawner.activeSpawnLimit;
            }

            return totalPoolSize;
        }
    }

    private List<GameObject> _pooledObjects;
    private List<WaitForSeconds> _spawnRates = new();
    private float spawnRate
    {
        get
        {
            var value = Random.Range(spawnRateMin, spawnRateMax);
            return value;
        }
    }

    private WaitForSeconds _waitForSpawnRate, _waitForSpawnDelay, _waitForPoolDelay, _waitForSpawnOffset;
    private WaitForFixedUpdate _wffu;
    private Coroutine _lateStartRoutine, _delaySpawnRoutine,_spawnRoutine,_poolCreationRoutine, _spawnWaitingRoutine;

    private PrefabDataList _prefabSet;
    private GameObject _parentObject;

    private int spawnedCount
    {
        get => spawnerData.activeCount.value;
        set => spawnerData.activeCount.SetValue(value);
    }

    private void Awake()
    {
        _parentObject = new GameObject($"SpawnedObjects_{name}");
        
        _wffu = new WaitForFixedUpdate();
        _waitForSpawnOffset = new WaitForSeconds(1.0f);

        _poolSize = poolSize;
        poolCreationDelay = ToleranceCheck(poolCreationDelay, poolCreationDelay);
        spawnDelay = ToleranceCheck(spawnDelay, spawnDelay);
        _waitForPoolDelay = new WaitForSeconds(poolCreationDelay);

        _waitForSpawnDelay = new WaitForSeconds(spawnDelay);

        SetSpawnRate();

        if (!spawnerData)
        {
            Debug.LogError("SpawnerData not found in " + name);
            return;
        }

        spawnerData.ResetSpawner();
        _prefabSet = spawnerData.prefabList;

        _poolCreationRoutine ??= StartCoroutine(DelayPoolCreation());
    }

    private IEnumerator DelayPoolCreation()
    {
        yield return _waitForPoolDelay;
        _parentObject.transform.SetParent(transform);
        yield return _wffu;
        ProcessPool();
        yield return _wffu;
        _poolCreationRoutine = null;
    }

    private void ProcessPool()
    {
        _pooledObjects ??= new List<GameObject>();
        int iterationCount = _poolSize - _pooledObjects.Count;
        if (iterationCount <= 0) return;
        
        int totalPriority = _prefabSet.GetPriority();

        for (int i = 0; i < iterationCount; i++)
        {
            int randomNumber = Random.Range(0, totalPriority);
            int sum = 0;
            foreach (var _ in _prefabSet.prefabDataList)
            {
                var objData = _prefabSet.GetRandomPrefabData();
                sum += objData.priority;
                if (randomNumber >= sum && usePriority) continue;
                var obj = Instantiate(objData.prefab);
                AddToPool(obj);
                break;
            }
        }
    }

    private void AddToPool(GameObject obj)
    {
        var spawnBehavior = obj.GetComponent<PooledObjectBehavior>();
        spawnBehavior = (spawnBehavior == null) ? obj.AddComponent<PooledObjectBehavior>() : spawnBehavior;
        spawnBehavior.spawned = false;

        _pooledObjects.Add(obj);
        obj.transform.SetParent(_parentObject.transform);
        obj.SetActive(false);
    }

    public void SetSpawnDelay(float newDelay)
    {
        newDelay = ToleranceCheck(spawnDelay, newDelay);
        if (newDelay < 0) return;
        spawnDelay = newDelay;
        _waitForSpawnDelay = new WaitForSeconds(spawnDelay);
    }
    
    private void SetSpawnRate()
    {
        if (!randomizeSpawnRate)
        {
            _waitForSpawnRate = new WaitForSeconds(spawnRate);
            return;
        }
        if (numToSpawn < _spawnRates.Count) return;
        
        var count = numToSpawn - _spawnRates.Count;
        for (var i = 0; i < count; i++)
        {
            _spawnRates.Add(new WaitForSeconds(spawnRate));
        }
    }
    
    public WaitForSeconds GetWaitSpawnRate()
    {
        return randomizeSpawnRate ? _spawnRates[Random.Range(0, _spawnRates.Count)] : _waitForSpawnRate;
    }
    
    private void Start()
    {
        if (spawnOnStart) _lateStartRoutine ??= StartCoroutine(LateStartSpawn());
    }

    private IEnumerator LateStartSpawn()
    {
        yield return _wffu;
        yield return _waitForPoolDelay;
        yield return _wffu;
        yield return _waitForSpawnDelay;
        yield return _wffu;
        StartSpawn();
        yield return _wffu;
        _lateStartRoutine = null;
    }

    public void StartSpawn(int amount)
    {
        if (_spawnRoutine != null)
        {
            if (allowMultipleSpawnInstances) numToSpawn += amount;
            return;
        }
        numToSpawn = (amount > 0) ? amount : numToSpawn;
        StartSpawn();
    }

    public void StartSpawn()
    {
        if (_spawnRoutine != null) return;
        numToSpawn = numToSpawn > 0 ? numToSpawn : 1;
        if (spawnedCount > 0) spawnedCount = 0;
        _delaySpawnRoutine ??= StartCoroutine(DelaySpawn());
    }
    
    public void StopSpawn()
    {
        if (_spawnRoutine == null) return;
        StopCoroutine(_spawnRoutine);
        _spawnRoutine = null;
    }

    private IEnumerator DelaySpawn()
    {
        SetSpawnRate();
        yield return _wffu;
        yield return _waitForSpawnDelay;
        _spawnRoutine ??= StartCoroutine(Spawn());
        yield return _wffu;
        _delaySpawnRoutine = null;
    }
    
    private IEnumerator Spawn()
    {
        yield return _waitForSpawnOffset;
        while (spawnedCount < numToSpawn)
        {
            var waitTime = GetWaitSpawnRate();
            if (allowDebug) Debug.Log($"Spawning... Count: {spawnedCount} Total To Spawn: {numToSpawn} Num Left: {numToSpawn-spawnedCount} PoolSize: {_poolSize} PooledObjects: {_pooledObjects.Count} spawners: {spawners.Count} spawnRate: {waitTime}");
            
            Spawner spawner = GetSpawner();
            if (spawner == null)
            {
                waitingCount = numToSpawn - spawnedCount;
                spawnedCount = 0;
                if (allowDebug) Debug.Log($"All Spawners Active... Killing Process, {waitingCount} spawns waiting for next spawn cycle.");
                _spawnRoutine = null;
                yield break;
            }

            GameObject spawnObj = FetchFromPool();
            _poolSize = _pooledObjects.Count;
            
            if (!spawnObj)
            {
                _poolSize++;
                ProcessPool();
                continue;
            }
            
            var navBehavior = spawner.pathingTarget ? spawnObj.GetComponent<NavAgentBehavior>(): null;
            if (spawner.pathingTarget && navBehavior == null) Debug.LogError($"No NavAgentBehavior found on {spawnObj} though a pathingTarget was found in ProcessSpawnedObject Method");

            Transform objTransform = spawnObj.transform;
            PooledObjectBehavior objBehavior = spawnObj.GetComponent<PooledObjectBehavior>();
                    
            if (objBehavior == null) Debug.LogError($"No SpawnObjectBehavior found on {spawnObj} in ProcessSpawnedObject Method");
            var rb = spawnObj.GetComponent<Rigidbody>();
            
            objBehavior.spawnManager = this;
            objBehavior.spawnerID = spawner.spawnerID;
            objBehavior.spawned = true;
            objBehavior.finalSpawn = spawnedCount == numToSpawn - 1;
            if (allowDebug) objBehavior.allowDebug = true;
            
            if (rb)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            
            if (allowDebug) Debug.Log($"Retrieved Spawner: {spawner.spawnerID} with... Count: {spawner.GetAliveCount()} Limit: {spawner.activeSpawnLimit}"); 
            Transform spawnLocation = spawner.spawnLocation;
            
            objTransform.position = spawnLocation.position;
            objTransform.rotation = spawnLocation.rotation;
            
            if (navBehavior) navBehavior.destination = spawner.pathingTarget;
            spawnObj.SetActive(true);
            if (navBehavior) navBehavior.Setup(spawner.pathingTarget);
            onSpawn.Invoke();
            spawner.IncrementCount();
            spawnedCount++;
            yield return waitTime;
        }
        onSpawningComplete.Invoke();
        _spawnRoutine = null;
    }

    private GameObject FetchFromPool() { return FetchFromList(_pooledObjects, obj => !obj.activeInHierarchy); }

    private Spawner RandomlyFetchFromAllOpenSpawners()
    {
        var availableSpawners = new List<Spawner>();
        foreach (var spawner in spawners)
        {
            if (spawner.GetAliveCount() < spawner.activeSpawnLimit)
            {
                availableSpawners.Add(spawner);
            }
        }
        return (availableSpawners.Count == 0) ? null: availableSpawners[Random.Range(0, availableSpawners.Count)];
    }

    private Spawner GetSpawner()
    {
        var count = spawners.Count;
        switch (count)
        {
            case 0:
                return null;
            case 1:
                return spawners[0];
        }

        var spawner = RandomlyFetchFromAllOpenSpawners();
        return spawner;
    }
    
    private IEnumerator ProcessWaitingSpawns()
    {
        yield return _wffu;
        if (waitingCount <= 0) yield break;
        StartSpawn(waitingCount);
        waitingCount = 0;
        _spawnWaitingRoutine = null;
    }
    
    public void NotifyOfDeath(string spawnerID)
    {
        if (_destroying) return;
        
        if (allowDebug) Debug.Log($"Notified of Death: passed {spawnerID} as spawnerID");
        var activeCount = 0;
        foreach (var spawner in spawners)
        {
            if (spawner.spawnerID != spawnerID)
            {
                activeCount += spawner.GetAliveCount();
            }
            else
            {
                spawner.DecrementCount();
                if (allowDebug) Debug.Log($"Found {spawnerID} in spawner list... Current Count: {spawner.GetAliveCount() + 1} --- New count: {spawner.GetAliveCount()}");
                activeCount += spawner.GetAliveCount();
            }
        }
        if (allowDebug) Debug.Log($"Total active: {activeCount}");
        if (activeCount <= 0 && numToSpawn - spawnedCount <= 0 && waitingCount <= 0)
        {
            if (allowDebug) Debug.Log($"NOTIFIED: {spawnerID} WAS THE FINAL SPAWN");
            onFinalSpawnDefeated.Invoke();
        }
        else
        {
            if (waitingCount <= 0) return;
            _spawnWaitingRoutine ??= StartCoroutine(ProcessWaitingSpawns());
        }
    }

    private void OnDestroy()
    {
        _destroying = true;
    }

    public List<(System.Action, string)> GetButtonActions()
    {
        return new List<(System.Action, string)> { (() => StartSpawn(numToSpawn), "Spawn") };
    }
}

namespace ZpTools
{
    public static class UtilityFunctions
    {
        public static float ToleranceCheck(float value, float newValue, float tolerance = 0.1f)
        {
            return System.Math.Abs(value - newValue) < tolerance ? value : newValue;
        }
        
        public static T FetchFromList<T>(List<T> listToProcess, System.Func<T, bool> condition)
        {
            if (listToProcess == null || listToProcess.Count == 0) return default;
            foreach (var obj in listToProcess)
            {
                if (condition(obj)) return obj;
            }
            return default;
        }
    }
}