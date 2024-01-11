using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileArrayData", menuName = "Data/Array/TileArrayData")]
public class TileArrayData : ScriptableObject
{
    public TileData[,] Array2D;

    public TileData this[int i, int j]
    {
        get => Array2D[i, j];
        set => Array2D[i,j] = value;
    }

    public TileData this[Vector2Int gridLocation] => Array2D[gridLocation.x, gridLocation.y];

    public void InitializeArraySize(int sizeX, int sizeY)
    {
        Array2D = new TileData[sizeX, sizeY];
    }
}