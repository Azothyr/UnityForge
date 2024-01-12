using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "Data/ControllerData/TileData")]
public class TileData : ScriptableObject
{
    public GameObject environmentPrefab, occupiedPrefab;
    [HideInInspector] public Transform transform;
    [HideInInspector] public GridManager grid;
    [HideInInspector] public Vector2Int gridCoord;
    public int priority;
}
