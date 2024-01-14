using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabInteraction : MonoBehaviour
{
    private XRGrabInteractable _interactable;

    public UnityEvent onLeftGrab, onLeftRelease, onRightGrab, onRightRelease;

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
        HandleInteraction(true, arg.interactorObject.transform.name);
    }

    private void Release(SelectExitEventArgs arg)
    {
        HandleInteraction(false, arg.interactorObject.transform.name);
    }
    
    private void HandleInteraction(bool grabbing, string side)
    {
        side = side.ToLower();
        Debug.Log($"{grabbing} {side}");
        if (side.Contains("left"))
        {
            if (grabbing)
            {
                onLeftGrab?.Invoke();
            }
            else
            {
                onLeftRelease?.Invoke();
            }
        }
        else if (side.Contains("Right"))
        {
            if (grabbing)
            {
                onRightGrab?.Invoke();
            }
            else
            {
                onRightRelease?.Invoke();
            }
        }
    }
}