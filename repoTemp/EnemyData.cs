using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/ControllerData/CharacterData/EnemyData")]
public class EnemyData : CharacterData
{
    public GameObject prefab;

    [Range(-100, 100)] // Adjust min and max values as needed
    public int speedDelta;

    public int scoreValue, currencyValue;
}