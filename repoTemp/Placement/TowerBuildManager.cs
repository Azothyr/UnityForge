using UnityEngine;

public class TowerBuildManager : BuildingManager
{
    public GameAction TriggerNavRebuild;
    public TileArrayData grid;
    [SerializeField] private GameObject _towerPrefab;
    private UIInterface _uiInterface;
    
    private void OnEnable()
    {
        _uiInterface = FindObjectOfType<UIInterface>();
        if (_uiInterface != null)
        {
            _uiInterface.SendClickDataToTower += OnClickDataReceived;
        }
    }

    private void OnDisable()
    {
        _uiInterface.SendClickDataToTower -= OnClickDataReceived;
    }

    private void OnClickDataReceived(ClickData data)
    {
        Vector2Int gridLocation = data.gridLocation;
        PlaceAtGridPoint(gridLocation.x, gridLocation.y, data.hitPoint, _towerPrefab);
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
