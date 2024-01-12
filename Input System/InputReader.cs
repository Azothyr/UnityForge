using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(fileName = "New Input Reader", menuName = "Input/Input Reader")]
public class InputReader : ScriptableObject, IPlayerActions, IBuildingActions
{ // 
    public event Action BuildEvent;
    public event Action<Vector2> MoveEvent, MoveCamEvent;

    private Controls _controls;

    private Vector2 _mousePosition;
    
    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
            _controls.Building.SetCallbacks(this);
        }
        _controls.Player.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(_mousePosition);
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        
    }

    public void OnCenterCameraToPlayer(InputAction.CallbackContext context)
    {
        
    }

    public void OnMoveCamera(InputAction.CallbackContext context)
    {
        MoveCamEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnOpenBuilding(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        _controls.Player.Disable();
        _controls.Building.Enable();
        BuildEvent?.Invoke();
    }

    public void OnExitBuilding(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        _controls.Building.Disable();
        _controls.Player.Enable();
    }

    public void OnOpenSettings(InputAction.CallbackContext context)
    {
        
    }
}
