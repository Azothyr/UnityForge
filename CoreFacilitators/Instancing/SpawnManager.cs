using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour, INeedButton
{
    public UnityEvent onSpawn;

    public SpawnerData spawnerData;

    public bool usePriority;

    [SerializeField] [ReadOnly] private int _activeCount;
    
    public int numToSpawn = 10, poolSize = 10;
    public float spawnRate = 0.3f, spawnDelay = 1.0f;

    private int _spawnedCount;

    private WaitForSeconds _waitForSpawnRate, _waitForSpawnDelay, _wfs;
    private PrefabDataList _prefabSet;
    private List<GameObject> _pooledObjects;

    private void Start()
    {
        _activeCount = 0;
        _waitForSpawnRate = new WaitForSeconds(spawnRate);
        _waitForSpawnDelay = new WaitForSeconds(spawnDelay);
        _wfs = new WaitForSeconds(1);
        spawnerData.ResetSpawner();
        _prefabSet = spawnerData.prefabDataList;
        StartCoroutine(DelayPoolCreation());
    }

    public void StartSpawn(int amount)
    {
        numToSpawn = amount;
        StartCoroutine(DelaySpawn());
    }

    private IEnumerator DelayPoolCreation()
    {
        yield return _wfs;
        CreatePool();
    }

    private IEnumerator DelaySpawn()
    {
        yield return _waitForSpawnDelay;
        StartCoroutine(Spawner());
    }
    
    
    private void CreatePool()
    {
        _pooledObjects = new List<GameObject>();
        
        int totalPriority = _prefabSet.GetPriority();
        
        for (int i = 0; i < poolSize; i++)
        {
            int randomNumber = Random.Range(0, totalPriority);
            int sum = 0;
            foreach (var prefabData in _prefabSet.prefabDataList)
            {
                sum += prefabData.priority;
                if (randomNumber < sum || !usePriority)
                {
                    GameObject obj = Instantiate(prefabData.obj);
                    AddToPool(obj);
                    break;
                }
            }
        }
    }

    private IEnumerator Spawner()
    {
        _spawnedCount = 0;
        while (_spawnedCount < numToSpawn)
        {
            _activeCount =  spawnerData.GetAliveCount();
            var spawnObj = FetchFromPool();
            if (spawnObj) Spawn(spawnObj);
            else IncreasePoolAndSpawn();
            yield return _waitForSpawnRate;
        }
    }
    
    private GameObject FetchFromPool()
    {
        if (_pooledObjects.Count == 0) return null;
        foreach (var obj in _pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }
    
    protected virtual void Spawn(GameObject obj)
    {
        if (!obj) return;
        if (obj.GetComponent<Rigidbody>()) obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.transform.position = transform.position;
        obj.transform.rotation = Quaternion.identity;
        obj.SetActive(true);
        _spawnedCount++;
        spawnerData.IncrementActiveInstancesCount();
        onSpawn.Invoke();
    }

    protected void IncreasePoolAndSpawn()
    {
        int totalPriority = spawnerData.prefabDataList.GetPriority();
        int randomNumber = Random.Range(0, totalPriority);
        int sum = 0;
        foreach (var prefabData in _prefabSet.prefabDataList)
        {
            sum += prefabData.priority;
            if (randomNumber < sum || !usePriority)
            {
                var prefab = FetchPrefab(prefabData);
                AddToPool(prefab);
                Spawn(prefab);
                break;
            }
        }
    }
    
    protected virtual GameObject FetchPrefab(PrefabData data)
    {
        var obj = data.obj;
        return obj;
    }
    
    protected virtual void AddToPool(GameObject obj)
    {
        _pooledObjects.Add(obj);
        obj.SetActive(false);
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