using UnityEngine;

[CreateAssetMenu (fileName = "SpawnerData", menuName = "Data/ManagerData/SpawnerData")]
public class SpawnerData : ScriptableObject
{
    // ReSharper disable once NotAccessedField.Global
    [SerializeField] [ReadOnly] public int activeInstances;
    public IntData activeCount;
    public PrefabDataList prefabList;

    private void Awake()
    {
        if (activeCount == null) Debug.LogError("Missing IntData for activeCount on SpawnerData" + name);
        activeInstances = activeCount.value;
    }

    public void ResetSpawner()
    {
        activeCount.SetValue(0);
        activeInstances = activeCount.value;
    }

    public int GetAliveCount()
    {
        if (activeCount.value < 0) activeCount.SetValue(0);
        return activeCount.value;
    }
    
    public void IncrementCount()
    {
        activeCount.UpdateValue(1);
        activeInstances = activeCount.value;
    }

    public void DecrementCount()
    {
        activeCount.UpdateValue(-1);
        activeInstances = activeCount.value;
    }
}