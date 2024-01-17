using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class ControllerTriggerInteraction : MonoBehaviour
{
    private XRGrabInteractable _interactable;

    public UnityEvent onTriggerDown, onTriggerUp;

    private void OnEnable()
    {
        _interactable = GetComponent<XRGrabInteractable>();
        
        _interactable.activated.AddListener(_ => Perform());
        _interactable.deactivated.AddListener(_ => Stop());
    }
    
    private void OnDisable()
    {
        _interactable.activated.RemoveListener(_ => Perform());
        _interactable.deactivated.RemoveListener(_ => Stop());
    }

    private void Perform()
    {
        onTriggerDown?.Invoke();
    }

    private void Stop()
    {
        onTriggerUp?.Invoke();
    }
}