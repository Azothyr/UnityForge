using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using Object = UnityEngine.Object;

public class SocketMatchInteractor : XRSocketInteractor
{
    [System.Serializable]
    public struct PossibleMatch
    {
        public ID id;
    }

    [SerializeField]
    private List<PossibleMatch> triggerID;
    
    public bool allowDebug;
    public bool disableObjectOnSocket;

    public ID socketID;
    
    public UnityEvent onObjectSocketed;
    public UnityEvent onObjectUnsocketed;
    
    private IDBehavior _idBehavior;
    private WaitForFixedUpdate _wffu = new WaitForFixedUpdate();
    
    private XRGrabInteractable _socketedObject;
    private Collider _socketTrigger;
    
    private Coroutine _removeAndMoveCoroutine;
    
    private new void Awake()
    {
        if (socketID)
        {
            _idBehavior = gameObject.AddComponent<IDBehavior>();
            _idBehavior.id = socketID;
        }
        
        _socketTrigger = GetComponent<Collider>();
        if (_socketTrigger == null)
        {
            if (!allowDebug) Debug.LogWarning("Socket trigger appears to be null, Adding BoxCollider");
            _socketTrigger = gameObject.AddComponent<BoxCollider>();
        }

        if (_socketTrigger.isTrigger == false)
        {
            if (!allowDebug) Debug.LogWarning("Socket trigger appears to be a collider, Setting to Trigger");
            _socketTrigger.isTrigger = true;
        }
        _removeAndMoveCoroutine = null;
        base.Awake();
    }

    protected override void OnEnable()
    {
        GetComponent<XRSocketInteractor>().selectEntered.AddListener(_ => OnObjectSocketed());
        GetComponent<XRSocketInteractor>().selectExited.AddListener(_ => OnObjectUnsocketed());
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        GetComponent<XRSocketInteractor>().selectEntered.RemoveListener(_ => OnObjectSocketed());
        GetComponent<XRSocketInteractor>().selectExited.RemoveListener(_ => OnObjectUnsocketed());
        base.OnDisable();
    }
    
    private void OnObjectSocketed()
    {
        onObjectSocketed.Invoke();
    }
    
    private void OnObjectUnsocketed()
    {
        onObjectUnsocketed.Invoke();
    }
    
    private static ID FetchOtherID(GameObject interactable)
    {
        var idBehavior = interactable.transform.GetComponent<IDBehavior>();
        return idBehavior != null ? idBehavior.id : null;
    }
    
    private bool CheckId(Object nameId)
    {
        if (triggerID == null) return false;
        return nameId != null && triggerID.Any(obj => nameId == obj.id);
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
        if (!disableObjectOnSocket) return base.StartSocketSnapping(interactable);
        StartCoroutine(DisableObject(interactable.gameObject));
        return false;
    }
    
    private IEnumerator DisableObject(GameObject obj)
    {
        yield return _wffu;
        yield return _wffu;
        yield return _wffu;
        obj.SetActive(false);
    }
    
    protected override bool EndSocketSnapping(XRGrabInteractable interactable)
    {
        return base.EndSocketSnapping(interactable);
    }
    
    public void RemoveAndMoveSocketObject(Transform copyTransform)
    {
        if (_socketedObject == null){Debug.LogWarning("SOCKET OBJECT APPEARS TO BE NULL"); return;}
        var obj = _socketedObject.gameObject;
        _socketTrigger.enabled = false;
        if (_removeAndMoveCoroutine != null) return;
        _removeAndMoveCoroutine = StartCoroutine(PerformRemoveAndMove(copyTransform.position, copyTransform.rotation));
    }

    public GameObject RemoveAndMoveSocketObject(Vector3 position, Quaternion rotation)
    {
        if (_socketedObject == null){Debug.LogWarning("SOCKET OBJECT APPEARS TO BE NULL"); return null;}
        var obj = _socketedObject.gameObject;
        _socketTrigger.enabled = false;
        if (_removeAndMoveCoroutine != null) return null;
        _removeAndMoveCoroutine = StartCoroutine(PerformRemoveAndMove(position, rotation));
        return obj;
    }
 
    private IEnumerator PerformRemoveAndMove(Vector3 position, Quaternion rotation)
    {
        var obj = _socketedObject.gameObject;
        yield return _wffu;
        interactionManager.CancelInteractableSelection(interactablesSelected[0]);
        yield return _wffu;
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        yield return _wffu;
        _socketedObject = null;
        yield return _wffu;
        _socketTrigger.enabled = true;
        _removeAndMoveCoroutine = null;
    }
}