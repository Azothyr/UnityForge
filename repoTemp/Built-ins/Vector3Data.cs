using UnityEngine;

[CreateAssetMenu (fileName = "Vector3Data", menuName = "Data/SingleValueData/Vector3Data")]
public class Vector3Data : ScriptableObject
{
    [SerializeField] private Vector3 vector3;
    
    public Vector3 value 
    {
        get => vector3;
        set => vector3 = value;
    }
}