using UnityEngine;

[CreateAssetMenu(fileName = "QuaternionData", menuName = "Data/Base/QuaternionData")]
public class QuaternionData : ScriptableObject
{
    [SerializeField] private Quaternion quaternion;

    public float x
    { get => quaternion.x; set => quaternion.x = value; }

    public float y
    { get => quaternion.y; set => quaternion.y = value; }

    public float z
    { get => quaternion.z; set => quaternion.z = value; }

    public float w
    { get => quaternion.w; set => quaternion.w = value; }

    public Quaternion value
    {
        get => quaternion;
        set
        {
            quaternion = value;
            x = value.x;
            y = value.y;
            z = value.z;
            w = value.w;
        }
    }

    public void Set(Quaternion newValue)
    {
        quaternion = newValue;
        x = newValue.x;
        y = newValue.y;
        z = newValue.z;
        w = newValue.w;
    }
}
