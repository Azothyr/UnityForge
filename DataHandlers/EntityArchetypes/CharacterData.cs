using UnityEngine;

[CreateAssetMenu (fileName = "CharacterData", menuName = "Data/ControllerData/CharacterData")]

public class CharacterData : ScriptableObject
{
    public ID id;
    public FloatData speed, topSpeed;
    public float knockBackPower, knockBackResistance;
    public BoolData canRun, gameOver;
}
