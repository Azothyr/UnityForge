using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileDataList", menuName = "Data/List/TileDataList")]
public class TileDataList : ScriptableObject
{
    public List<TileData> tileDataList;

    public int Size()
    {
        return tileDataList.Count;
    }

    public int GetPriority()
    {
        var priority = 0;
        foreach (TileData tileData in tileDataList)
        {
            priority += tileData.priority;
        }
        return priority;
    }

    public GameObject GetRandomPrefab()
    {
        GameObject prefab = tileDataList[Random.Range(0, Size())].environmentPrefab;
        return prefab;
    }

    public GameObject GetRandomPriorityPrefab()
    { 
        /* CHANCES BASED ON 4 ITEMS
        * First item: 10/(10+5+3+1) = 10/19 = ~52.6% chance
        * Second item: 5/19 = ~26.3% chance
        * Third item: 3/19 = ~15.8% chance
        * Fourth item: 1/19 = ~5.3% chance
        */
        int randomNumber = Random.Range(0, GetPriority());
        int sum = 0;
        foreach (TileData tileData in tileDataList) 
        {
            sum += tileData.priority;
            if (randomNumber < sum)
            {
                return tileData.environmentPrefab; 
            }
        }
        return tileDataList[0].environmentPrefab;
    }


    public GameObject GetOccupiedPrefab()
    {
        GameObject prefab = tileDataList[0].occupiedPrefab;
        return prefab;
    }
}