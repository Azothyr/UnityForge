using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabDataList", menuName = "Data/List/PrefabDataList")]
public class PrefabDataList : ScriptableObject
{
    public List<PrefabData> prefabDataList;

    public int Size()
    {
        return prefabDataList.Count;
    }

    public int GetPriority()
    {
        var priority = 0;
        foreach (PrefabData prefabData in prefabDataList)
        {
            priority += prefabData.priority;
        }
        return priority;
    }

    public GameObject GetRandomPrefab()
    {
        GameObject prefab = prefabDataList[Random.Range(0, Size())].obj;
        return prefab;
    }
}