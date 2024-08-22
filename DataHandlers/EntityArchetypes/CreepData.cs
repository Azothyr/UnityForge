using UnityEngine;

[CreateAssetMenu (fileName = "CreepData", menuName = "Data/Entity/CreepData")]
public class CreepData : ScriptableObject
{
    public string unitName, type;
    public float speed, height, radius, health, damage, attackSpeed, attackRange,
        attackDuration, attackCooldown, attackDelay, attackDamage;
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