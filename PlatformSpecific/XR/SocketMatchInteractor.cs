#nullable enable
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketMatchInteractor : XRSocketInteractor
{
    [System.Serializable]
    public struct PossibleMatch
    {
        public ID nameIdObj;
    }

    [SerializeField]
    private List<PossibleMatch>? triggerEnterMatches;

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && CheckId(FetchOtherID(interactable.transform.gameObject));
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return base.CanSelect(interactable) && CheckId(FetchOtherID(interactable.transform.gameObject));
    }

    private static ID? FetchOtherID(GameObject interactable)
    {
        var idBehavior = interactable.transform.GetComponent<IDBehavior>();
        return idBehavior != null ? (ID?)idBehavior.idObj : null;
    }
    
    private bool CheckId(ID? nameId)
    {
        if (triggerEnterMatches == null) return false;
        if (nameId == null) return false;

        return triggerEnterMatches.Any(obj => nameId == obj.nameIdObj);
    }
}