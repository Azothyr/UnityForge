using System.Collections.Generic;
using System.Linq;
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
        return prefabDataList.Sum(prefabData => prefabData.priority);
    }

    public GameObject GetRandomPrefab()
    {
        return prefabDataList[Random.Range(0, Size())].obj;
    }
}