using UnityEngine;

[CreateAssetMenu]
public class Instancer : ScriptableObject
{
    public GameObject prefab;

    public void CreateInstance(Vector3Data obj)
    {
        Instantiate(prefab, obj.value, Quaternion.identity);
    }

    public void CreateInstance(Vector3 obj)
    {
        Instantiate(prefab, obj, Quaternion.identity);
    }

    public void CreateInstance(GameObject obj)
    {
        Instantiate(prefab, obj.transform.position, Quaternion.identity);
    }
}