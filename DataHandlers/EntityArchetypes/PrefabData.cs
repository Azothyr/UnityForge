using UnityEngine;

[CreateAssetMenu(fileName = "PrefabData", menuName = "Data/SingleValueData/PrefabData")]
public class PrefabData : ScriptableObject
{
    public GameObject prefab;
    
    public GameObject obj 
    {
        get => prefab;
        set => prefab = obj;
    }
    
    [SerializeField] private int spawnPriority;
    
    public int priority { get => spawnPriority; set => spawnPriority = value; }
}
