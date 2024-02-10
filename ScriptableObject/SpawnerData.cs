using System;
using UnityEngine;

[CreateAssetMenu (fileName = "SpawnerData", menuName = "Data/ManagerData/SpawnerData")]
public class SpawnerData : ScriptableObject
{
    [HideInInspector] public int activeInstancesCount;
    public IntData globalActiveInstancesCount;
    public PrefabDataList prefabDataList;

    private void Awake()
    {
        ResetSpawner();
    }

    public void ResetSpawner()
    {
        activeInstancesCount = 0;
    }

    public int GetAliveCount()
    {
        if (activeInstancesCount < 0) activeInstancesCount = 0;
        return activeInstancesCount;
    }
    
    public void IncrementActiveInstancesCount()
    {
        globalActiveInstancesCount.UpdateValue(1);
        activeInstancesCount += 1;
    }

    private void DecrementActiveInstancesCount()
    {
        globalActiveInstancesCount.UpdateValue(-1);
        activeInstancesCount -= 1;
    }

    public void InstanceRemoved()
    {
        DecrementActiveInstancesCount();
    }
}