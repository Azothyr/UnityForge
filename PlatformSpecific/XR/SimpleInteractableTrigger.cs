using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class SimpleInteractableTrigger : MonoBehaviour
{
    public UnityEvent onInteractionPerformed;
    public UnityEvent onInteractionEnded;

    private void OnEnable()
    {
        GetComponent<XRSimpleInteractable>().selectEntered.AddListener(_ => OnInteractionPerformed());
        GetComponent<XRSimpleInteractable>().selectExited.AddListener(_ => OnInteractionEnded());
        
    }
    
    private void OnDisable()
    {
        GetComponent<XRSimpleInteractable>().selectEntered.RemoveListener(_ => OnInteractionPerformed());
        GetComponent<XRSimpleInteractable>().selectExited.RemoveListener(_ => OnInteractionEnded());
    }
    
    private void OnInteractionPerformed()
    {
        onInteractionPerformed.Invoke();
    }
    
    private void OnInteractionEnded()
    {
        onInteractionEnded.Invoke();
    }
}