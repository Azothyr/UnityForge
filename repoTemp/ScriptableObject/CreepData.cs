using UnityEngine;

[CreateAssetMenu (fileName = "CreepData", menuName = "Data/ControllerData/CreepData")]
public class CreepData : ScriptableObject
{
    public string unitName, type;
    public float speed, height, radius;
    public int totalKilled, totalSpawned, totalEscaped;

    public void IncrementKilledTotal()
    {
        totalKilled++;
    }

    public void IncrementSpawnedTotal()
    {
        totalSpawned++;
    }

    public void IncrementEscapedTotal()
    {
        totalEscaped++;
    }
}