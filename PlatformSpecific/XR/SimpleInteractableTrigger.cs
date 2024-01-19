using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class SimpleInteractableTrigger : MonoBehaviour
{
    public UnityEvent onInteractionPerformed;
    // public UnityEvent onInteractionEnded;

    private void OnEnable()
    {
        GetComponent<XRSimpleInteractable>().selectEntered.AddListener(_ => OnInteractionPerformed());
    }
    
    private void OnDisable()
    {
        GetComponent<XRSimpleInteractable>().selectEntered.RemoveListener(_ => OnInteractionPerformed());
    }
    
    private void OnInteractionPerformed()
    {
        onInteractionPerformed.Invoke();
    }
    
    // private void OnInteractionEnded(XRBaseInteractor obj)
    // {
    //     onInteractionEnded.Invoke();
    // }
}
