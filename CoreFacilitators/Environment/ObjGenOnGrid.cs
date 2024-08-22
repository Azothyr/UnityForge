using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Generate3DNavMeshSurface))]
public class ObjGenOnGrid : MonoBehaviour, INeedButton
{
    public GameAction triggerNavRebuild;
    private Generate3DNavMeshSurface _navMeshGen;
    public TileDataList instanceTileData;
    public TileArrayData gridArray;
    public Vector3Data navSize;
    private TileData _tileData;
    [Range(0f, 1f)]
    [Step(0.01f)]
    public float heightOffset = 1f;
    [SerializeField]
    private int width = 5;
    [SerializeField]
    private int length = 10;
    [SerializeField]
    private int startHeight = 0;
    
    private GameObject _tileParent, _tilePrefab, _occupiedPrefab;
    private Vector3 _prefabSize;
    private GridManager _grid;
    private bool _isResetting;
    private WaitForFixedUpdate _wffu;
    
    // variables for editor updating
    // private int _prevWidth, _prevLength, _prevHeight;
    // private float _prevHeightOffset, _resetDelay;
    
    private void Awake()
    {
        _tileParent = GameObject.Find("Ground");
        _occupiedPrefab = instanceTileData.GetOccupiedPrefab();
        _navMeshGen = GetComponent<Generate3DNavMeshSurface>();
        _wffu = new WaitForFixedUpdate();
        ResetGround();
    }

    private void CreateGround()
    {
        if (width <= 0 || length <= 0)
        {
            return;
        }

        gridArray.InitializeArraySize(width, length);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                _tileData = ScriptableObject.CreateInstance<TileData>();
                _tileData.gridCoord = new Vector2Int(i, j);
                _tilePrefab = instanceTileData.GetRandomPriorityPrefab();
                var meshRenderer = _tilePrefab.GetComponentInChildren<MeshRenderer>();
                Bounds bounds = meshRenderer.bounds;
                _prefabSize = bounds.size;
                _tilePrefab.transform.localScale = new Vector3(.32f, 1, .32f);

                    float randomHeight = Random.Range(0, heightOffset);
                GameObject cell = Instantiate(_tilePrefab,
                    new Vector3(i * _prefabSize.x - (width * _prefabSize.x) / 2f + _prefabSize.x / 2f,
                         startHeight + randomHeight,
                        j * _prefabSize.z - (length * _prefabSize.z) / 2f + _prefabSize.z / 2f),
                    Quaternion.identity);

                cell.transform.SetParent(_tileParent.transform);
                
                _grid = cell.GetComponent<GridManager>();
                _tileData.transform = cell.transform;
                _tileData.environmentPrefab = _tilePrefab;
                _tileData.occupiedPrefab = _occupiedPrefab;
                _tileData.grid = _grid;

                
                gridArray[i, j] = _tileData;
                _grid.tileData = _tileData;
                _grid.SetGrid(gridArray);
            }
        }
        navSize.value = new Vector3(width * _prefabSize.x, startHeight + heightOffset + 3, length * _prefabSize.z);
        triggerNavRebuild.RaiseAction();
    }

    [ContextMenu("Reset Ground")]
    public void ResetGround()
    {
        if(_tileParent == null)
        {
            _tileParent = new GameObject("Ground");
        }
        else
        {
            foreach (Transform child in _tileParent.transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }
        _tileParent.transform.position = new Vector3(0,0,0);
        CreateGround();
    }
    
    private void OnDisable()
    {
        if (_tileParent != null)
        {
            Destroy(_tileParent);
        }
    }

    public System.Collections.Generic.List<(System.Action, string)> GetButtonActions()
    {
        return new System.Collections.Generic.List<(System.Action, string)>
        {
            (() =>
            {
                if (!_isResetting)
                    CoroutineController.Run(DelayedResetGround());
            }, "Generate New")
        };
    }
/* Use this only to find the height offset you want. It duplicates the grid objects and breaks the navmesh. 
    public void OnValidate()
    {
        _resetDelay = 0.5f;
        if(Application.isPlaying && (_prevWidth != width || _prevLength != length || _prevHeight != startHeight || _prevHeightOffset != heightOffset))
        {
            _prevWidth = width;
            _prevLength = length;
            _prevHeight = startHeight;
            _prevHeightOffset = heightOffset;
            StartCoroutine(DelayedResetGround());
        }
    }
*/
    private IEnumerator DelayedResetGround()
    {
        _isResetting = true;
        yield return _wffu;
        ResetGround();
        _isResetting = false;
    }
}