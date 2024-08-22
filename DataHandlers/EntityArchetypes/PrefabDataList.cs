using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabDataList", menuName = "Data/List/PrefabDataList")]
public class PrefabDataList : ScriptableObject
{
    [HideInInspector] public List<PrefabData> prefabDataList;

    public int Size()
    {
        return prefabDataList.Count;
    }

    public int GetPriority()
    {
        var sum = 0;
        foreach (var prefabData in prefabDataList) sum += prefabData.priority;
        return sum;
    }
    
    public int GetPrefabIndex(GameObject prefab)
    {
        if (prefabDataList == null) return -1;
        for (var i = 0; i < prefabDataList.Count; i++)
        {
            if (prefabDataList[i].prefab == prefab) return i;
        }
        return -1;
    }
    
    public PrefabData GetRandomPrefabData()
    {  
        if (prefabDataList == null) return null;
        if (prefabDataList.Count == 0) return null;
        var randomIndex = Random.Range(0, Size());
        return prefabDataList[randomIndex];
    }
}