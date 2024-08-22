using UnityEngine;
using UnityEngine.Events;

public class PooledObjectBehavior : MonoBehaviour
{
    public UnityEvent onSpawn;
    public SpawnManager spawnManager { get; set; }
    public string spawnerID { get; set; }
    public bool spawned { get; set; }
    public bool finalSpawn { get; set; }
    public bool allowDebug { get; set; }
    
    private bool _justInstantiated, _bypassDisable, _respawnTriggered;

    private void Awake()
    {
        _justInstantiated = false;
        _bypassDisable = false;
        _respawnTriggered = false;
    }

    public void SetSpawnDelay(float respawnTime)
    {
        if (spawnManager == null)
        {
            Debug.LogWarning("SpawnManager is null on " + name + " SpawnedObjectBehavior.");
            return;
        }
        spawnManager.SetSpawnDelay(respawnTime);
    }

    public void TriggerRespawn()
    {
        if (_respawnTriggered) return;
        _respawnTriggered = true;
        _bypassDisable = true;
        if (spawnManager == null)
        {
            Debug.LogWarning("SpawnManager is null" + name + " SpawnedObjectBehavior.");
            return;
        }
        spawnManager.NotifyOfDeath(spawnerID);
        spawnManager.StartSpawn(1);
    }

    private void OnEnable()
    {
        if (_justInstantiated)
        {
            _justInstantiated = false;
            return;
        }
        spawned = true;
        _bypassDisable = false;
        onSpawn.Invoke();
    }

    public void InvalidateDeath()
    {
        finalSpawn = false;
        spawnManager.numToSpawn++;
        spawnManager.waitingCount++;
    }

    private void OnDisable()
    {
        _respawnTriggered = false;
        if (allowDebug) Debug.Log($"OnDisable of {name} called");
        if (!spawned) return;
        if (_bypassDisable)
        {
            _bypassDisable = false;
            return;
        }
        spawnManager.NotifyOfDeath(spawnerID);
        spawned = false;
    }

    private void OnDestroy()
    {
        // Stops errors from being thrown on closing the game        
        spawned = false;
    }
}