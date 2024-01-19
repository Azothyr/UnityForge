#nullable enable
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketMatchInteractor : XRSocketInteractor
{
    [System.Serializable]
    public struct PossibleMatch
    {
        public ID nameIdObj;
    }

    [SerializeField]
    private List<PossibleMatch>? triggerID;
    
    private XRGrabInteractable? _socketedObject;
    
    private static ID? FetchOtherID(GameObject interactable)
    {
        var idBehavior = interactable.transform.GetComponent<IDBehavior>();
        return idBehavior != null ? (ID?)idBehavior.idObj : null;
    }
    
    private bool CheckId(Object? nameId)
    {
        if (triggerID == null) return false;
        return nameId != null && triggerID.Any(obj => nameId == obj.nameIdObj);
    }

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && CheckId(FetchOtherID(interactable.transform.gameObject));
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return base.CanSelect(interactable) && CheckId(FetchOtherID(interactable.transform.gameObject));
    }

    protected override bool StartSocketSnapping(XRGrabInteractable interactable)
    {
        _socketedObject = interactable;
        return base.StartSocketSnapping(interactable);
    }
    
    protected override bool EndSocketSnapping(XRGrabInteractable grabInteractable)
    {
        if (grabInteractable != _socketedObject) return base.EndSocketSnapping(grabInteractable);
        _socketedObject = null;
        return base.EndSocketSnapping(grabInteractable);
    }
    
    public void RemoveSocketObject()
    {
        if (_socketedObject == null) return;
        var obj = _socketedObject.transform;
        obj.position = Vector3.zero;
        EndSocketSnapping(_socketedObject);
        obj.gameObject.SetActive(false);
    }
}