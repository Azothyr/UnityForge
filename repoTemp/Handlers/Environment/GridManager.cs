using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GridManager : MonoBehaviour, IDrawGizmo
{
    public Vector2Int gridLocation;
    public TileData tileData;
    private TileArrayData _grid;

    private void Start()
    {
        gridLocation = tileData.gridCoord;
    }

    public void SetGrid(TileArrayData grid)
    {
        _grid = grid;
    }
    
    public Vector2Int GetGridLocation()
    {
        return gridLocation;
    }

    public void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (Selection.Contains(gameObject))
        {
            Gizmos.color = Color.red;
            Vector3 position = transform.position;
            Gizmos.DrawWireCube(position, transform.localScale);
        
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.white;
            style.fontSize = 25;
            
            // Draw the text at the top of the cube.
            position.y += transform.localScale.y/-2f - 0.5f; // Add some offset.
        
            // You'll need the UnityEditor namespace for Handles
            Handles.Label(position, gridLocation.x + ", " + gridLocation.y);

        }
#endif
    }
}
