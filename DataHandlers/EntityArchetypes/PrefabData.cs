using UnityEngine;

[CreateAssetMenu(fileName = "PrefabData", menuName = "Data/SingleValueData/PrefabData")]
public class PrefabData : GameObjData
{
    [SerializeField] private int spawnPriority;

    public int priority
    {
        get => spawnPriority;
        set => spawnPriority = priority;
    }
}
