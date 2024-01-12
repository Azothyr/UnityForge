using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu (fileName = "Vector3Data", menuName = "Data/Base/Vector3Data")]
public class Vector3Data : ScriptableObject
{
    [SerializeField]
    private Vector3 vector3;

    public float x
    {
        get => vector3.x;
        set => vector3.x = value;
    }

    public float y
    {
        get => vector3.y;
        set => vector3.y = value;
    }

    public float z
    {
        get => vector3.z;
        set => vector3.z = value;
    }

    public Vector3 value
    {
        get => vector3;
        set => vector3 = value;
    }

    public void Set(Vector3 newValue)
    {
        vector3 = newValue;
    }
}