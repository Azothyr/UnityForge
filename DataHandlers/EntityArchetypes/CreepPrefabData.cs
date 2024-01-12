using UnityEngine;

[CreateAssetMenu(fileName = "CreepPrefabData", menuName = "Data/SingleValueData/CreepPrefabData")]
public class CreepPrefabData : PrefabData
{
    [SerializeField] private CreepData creepDataValue;

    public CreepData creepData
    {
        get => creepDataValue;
        set => creepDataValue = creepData;
    }
}
