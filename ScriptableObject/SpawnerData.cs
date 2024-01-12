using UnityEngine;

[CreateAssetMenu (fileName = "SpawnerData", menuName = "Data/ManagerData/SpawnerData")]
public class SpawnerData : ScriptableObject
{
    [HideInInspector] public int creepsAliveCount;
    public IntData globalCreepsAliveCount;
    public PrefabDataList prefabDataList;

    public void ResetSpawner()
    {
        creepsAliveCount = 0;
    }

    public int GetAliveCount()
    {
        return creepsAliveCount;
    }
    
    public void IncrementCreepsAliveCount()
    {
        globalCreepsAliveCount.UpdateValue(1);
        creepsAliveCount += 1;
    }

    public void CreepKilled()
    {
        DecrementCreepsAliveCount();
    }

    public void CreepEscaped()
    {
        DecrementCreepsAliveCount();
    }

    private void DecrementCreepsAliveCount()
    {
        globalCreepsAliveCount.UpdateValue(-1);
        creepsAliveCount -= 1;
    }

}