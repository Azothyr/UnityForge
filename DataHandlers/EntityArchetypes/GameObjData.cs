using UnityEngine;

[CreateAssetMenu (fileName = "GameObjData", menuName = "Data/SingleValueData/GameObjData")]
public class GameObjData : ScriptableObject
{
    public GameObject prefab;
    
    public GameObject obj 
    {
        get => prefab;
        set => prefab = value;
    }
}
