/* InteractionHandler is causing a comiler error and needs to be updated before this can be used.
using UnityEngine;

public class TowerBuildManager : BuildingManager
{
    public GameAction triggerNavRebuild;
    public TileArrayData grid;
    [SerializeField] private GameObject towerPrefab;
    private InteractionHandler _interactionHandler;
    
    private void OnEnable()
    {
        _interactionHandler = FindObjectOfType<InteractionHandler>();
        if (_interactionHandler != null)
        {
            _interactionHandler.SendClickDataToTower += OnClickDataReceived;
        }
        else
        {
            Debug.LogError("InteractionHandler not found in scene");
        }
    }

    private void OnDisable()
    {
        _interactionHandler.SendClickDataToTower -= OnClickDataReceived;
    }

    private void OnClickDataReceived(ClickData data)
    {
        Vector2Int gridLocation = data.gridLocation;
        PlaceAtGridPoint(gridLocation.x, gridLocation.y, data.hitPoint, towerPrefab);
    }


    public override void Build()
    {
        
    }

    private void PlaceAtGridPoint(int x, int y, Vector3 hitLocation, GameObject prefab)
    {
        TileData data = grid[x, y];
        float transY = prefab.transform.position.y * 2 + hitLocation.y;
        Vector3 _position = new Vector3(data.transform.position.x, transY, data.transform.position.z);

        Instantiate(prefab, _position, Quaternion.identity);
    }
}
*/