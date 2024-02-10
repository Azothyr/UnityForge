// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Events;
//
// public class SpawnManager : MonoBehaviour, INeedButton
// {
//     public UnityEvent creepSpawned;
//
//     public SpawnerData spawnerData;
//
//     public bool usePriority;
//
//     [SerializeField] [ReadOnly] public int activeCount;
//     
//     public int numToSpawn = 10, poolSize = 10;
//     public float spawnRate = 0.3f, spawnDelay = 1.0f;
//
//     private int _spawnedCount;
//     private bool spawn;
//
//     private WaitForSeconds _waitForSpawnRate, _waitForSpawnDelay, _wfs;
//     private PrefabDataList _prefabSet;
//     private List<GameObject> _pooledObjects = new();
//     
//     
//     private void OnEnable()
//     {
//         _waitForSpawnRate = new WaitForSeconds(spawnRate);
//         _waitForSpawnDelay = new WaitForSeconds(spawnDelay);
//         _wfs = new WaitForSeconds(0.1f);
//     }
//
//     private void Start()
//     {
//         activeCount = 0;
//         spawnerData.ResetSpawner();
//         _prefabSet = spawnerData.prefabDataList;
//         StartCoroutine(DelayPoolCreation());
//     }
//
//     public void StartSpawn(int amount)
//     {
//         numToSpawn = amount;
//         StartCoroutine(DelaySpawn());
//     }
//
//     private IEnumerator DelayPoolCreation()
//     {
//         yield return _wfs;
//         CreatePool();
//     }
//     
//     
//     private void CreatePool()
//     {
//         _pooledObjects = new List<GameObject>();
//         
//         int totalPriority = spawnerData.prefabDataList.GetPriority();
//         
//         for (int i = 0; i < poolSize; i++)
//         {
//             int randomNumber = Random.Range(0, totalPriority);
//             int sum = 0;
//             foreach (PrefabData prefabData in spawnerData.prefabDataList.prefabDataList)
//             {
//                 sum += prefabData.priority;
//                 if (randomNumber < sum)
//                 {
//                     GameObject obj = Instantiate(prefabData.obj);
//                     spawnerData.IncrementActiveInstancesCount();
//                     _pooledObjects.Add(obj);
//                     obj.SetActive(false);
//                     break;
//                 }
//             }
//         }
//     }
//
//     private IEnumerator DelaySpawn()
//     {
//         yield return _waitForSpawnDelay;
//         StartCoroutine(Spawner());
//     }
//
//     private IEnumerator Spawner()
//     {
//         _spawnedCount = 0;
//         while (_spawnedCount < numToSpawn)
//         {
//             activeCount =  spawnerData.GetAliveCount();
//             var spawnObj = FetchFromPool();
//             // if (spawnObj) Spawn(spawnObj);
//             else IncreasePoolAndSpawn();
//             yield return _waitForSpawnRate;
//         }
//     }
//     
//     private GameObject FetchFromPool()
//     {
//         if (_pooledObjects == null || _pooledObjects.Count == 0) return null;
//         foreach (var obj in _pooledObjects)
//         {
//             if (!obj.activeInHierarchy)
//             {
//                 obj.transform.position = transform.position;
//                 obj.transform.rotation = Quaternion.identity;
//                 obj.SetActive(true);
//                 _spawnedCount++;
//                 spawnerData.IncrementActiveInstancesCount();
//                 creepSpawned.Invoke();
//                 spawn = true;
//                 break;
//             }
//         }
//         return spawn;
//     }
//
//     private void IncreasePoolAndSpawn()
//     {
//         int totalPriority = spawnerData.prefabDataList.GetPriority();
//         int randomNumber = Random.Range(0, totalPriority);
//         int sum = 0;
//         foreach (PrefabData prefabData in spawnerData.prefabDataList.prefabDataList)
//         {
//             sum += prefabData.priority;
//             if (randomNumber < sum)
//             {
//                 GameObject obj = Instantiate(prefabData.obj);
//                 _pooledObjects.Add(obj);
//                 obj.transform.position = transform.position;
//                 obj.transform.rotation = Quaternion.identity;
//                 obj.SetActive(true);
//                 _spawnedCount++;
//                 spawnerData.IncrementActiveInstancesCount();
//                 creepSpawned.Invoke();
//                 break;
//             }
//         }
//     }
//     
//     protected virtual GameObject FetchPrefab(PrefabData data)
//     {
//         var obj = data.obj;
//         return obj;
//     }
//
//     protected virtual void AddToPool(GameObject obj)
//     {
//         if (obj != null)
//         {
//             _pooledObjects.Add(obj);
//             obj.SetActive(false);
//         }
//         else
//         {
//             Debug.LogError("Attempted to add null GameObject to the pool.");
//         }
//     }
//     
//     public void ButtonAction()
//     {
//        StartSpawn(numToSpawn);
//     }
//
//     public string GetButtonName()
//     {
//         return "Spawn";
//     }
// }