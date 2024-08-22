using System.Collections;
using UnityEngine;

public class WorldSpaceUIBehavior : MonoBehaviour
{
    public Transform mainCamera;
    public Transform canvas;
    
    public Vector3 offset;
    
    public bool performLookAtCamOnStart;
    
    private Transform _localSpace;
    
    private void Start()
    {
        mainCamera = Camera.main.transform;
        _localSpace = transform.parent;
        
        transform.SetParent(canvas);
        
        if (performLookAtCamOnStart) StartCoroutine(LookAtCam());
    }
    
    private IEnumerator LookAtCam()
    {
        while (true)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.position);
            transform.position = _localSpace.position + offset;
            yield return new WaitForFixedUpdate();
        }
    }
}
