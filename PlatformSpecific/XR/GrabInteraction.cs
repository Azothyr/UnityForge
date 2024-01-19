using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class GrabInteraction : MonoBehaviour
{
    private XRGrabInteractable _interactable;

    public bool toggleGrabbersMeshVisibility;

    public UnityEvent onGrab, onRelease;

    private void OnEnable()
    {
        _interactable = GetComponent<XRGrabInteractable>();
        
        _interactable.selectEntered.AddListener(Grab);
        _interactable.selectExited.AddListener(Release);
    }
    
    private void OnDisable()
    {
        _interactable.selectEntered.RemoveListener(Grab);
        _interactable.selectExited.RemoveListener(Release);
    }

    private void Grab(SelectEnterEventArgs arg)
    {
        if (toggleGrabbersMeshVisibility) ToggleVis(false, arg.interactorObject.transform);
        HandleInteractionEvent(true);
    }

    private void Release(SelectExitEventArgs arg)
    {
        if (toggleGrabbersMeshVisibility) ToggleVis(true, arg.interactorObject.transform);
        HandleInteractionEvent(false);
    }

    private void ToggleVis(bool on, Component interactor)
    {
        var meshBehavior = interactor.GetComponent<InteractorMeshBehavior>();
        if (meshBehavior == null) return;
        if (on) meshBehavior.Show();
        else meshBehavior.Hide();
    }
    

    private void HandleInteractionEvent(bool grabbing)
    {
        if (grabbing)
        {
            onGrab?.Invoke();
        }
        else
        {
            onRelease?.Invoke();
        }
    }
}