using UnityEngine;

public class CreepSpawnManager : SpawnManager, INeedButton
{
    public Transform pathingToLocation;

    protected override void Spawn(GameObject obj)
    {
        obj.GetComponent<NavAgentBehavior>().destination = pathingToLocation;
        obj.SetActive(true);
        obj.GetComponent<NavAgentBehavior>().Setup(pathingToLocation);
        base.Spawn(obj);
    }
    
    protected override GameObject FetchPrefab(PrefabData data)
    {
        var creepPrefabData = (CreepPrefabData)data;
        creepPrefabData.creepData.totalSpawned += 1;
        return base.FetchPrefab(creepPrefabData);
    }
    
    protected override void AddToPool(GameObject obj)
    {
        obj.GetComponent<NavAgentBehavior>().destination = pathingToLocation;
        base.AddToPool(obj);
    }
}