/* controls.GameInputObj.DefaultControls is causing a compiler error and needs to be created as dynamic
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractionHandler : MonoBehaviour
{
    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    
    public UnityAction<ClickData> SendClickDataToTower;
    public GameInputsSO controls;
    private ClickData _clickData;
    private Camera _cameraMain;
    private CameraUtility _cameraUtility;
    private Vector2 _clickPosition;
    private Vector3 _mouseWorldPosition;
    private bool _isHolding;

    public GameObject prefabObj;
    private GameObject _clone;
    
    private void Awake()
    {
        _clickData = ScriptableObject.CreateInstance<ClickData>();
        _cameraUtility = ScriptableObject.CreateInstance<CameraUtility>();
        _cameraMain = Camera.main;
        controls.GameInputsObj.DefaultControls.PrimaryPress.performed += OnClick;
        controls.GameInputsObj.DefaultControls.PrimaryPress.canceled += OffClick;
        controls.GameInputsObj.DefaultControls.PrimaryPosition.performed += context => _clickPosition = context.ReadValue<Vector2>();
    }
    
    private void OnEnable() => controls.GameInputsObj.DefaultControls.Enable();
    
    private void OnDisable() => controls.GameInputsObj.DefaultControls.Disable();

    private void OnClick(InputAction.CallbackContext context)
    {
        GetHitPosition();
        _isHolding = true;
        _clickData.positionStart = _clickPosition;
        _clone = Instantiate(prefabObj, GetHitPosition(), prefabObj.transform.rotation);
        _clone.GetComponentInChildren<Collider>().enabled = false;
        StartCoroutine(UpdateMousePosition());
    }
    
    private IEnumerator UpdateMousePosition()
    {
        while (_isHolding)
        {
            _clone.transform.position = GetHitPosition() + new Vector3(0, _clone.transform.localScale.y, 0);
            yield return _waitForFixedUpdate;
        }
    }
    
    private void OffClick(InputAction.CallbackContext context)
    {
        _isHolding = false;
        _clickData.positionEnd = _clickData.positionCurrent;
        Collider hitObj = GetHitObj();
        if (hitObj != null && hitObj.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            GridManager grid = hitObj.GetComponent<GridManager>();
            Vector2Int gridLocation = grid.GetGridLocation();
            _clickData.hitPoint = GetHitPosition();
            _clickData.hitObjCollider = GetHitObj();

            _clickData.gridLocation = gridLocation;
            SendClickDataToTower(_clickData);
        }

        Destroy(_clone);
    }
    
    private Vector3 GetHitPosition()
    {
        return _cameraUtility.GetHitPosition(_cameraMain, _clickPosition);
    }

    private Collider GetHitObj()
    {
        return _cameraUtility.GetHitCollider(_cameraMain, _clickPosition);
    }
}
*/