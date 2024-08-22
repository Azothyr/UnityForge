using Unity.VisualScripting;
using UnityEngine;

public class TransformBehavior : MonoBehaviour
{
    public Vector3 setPosition;
    public Quaternion setRotation;
    
    private Transform _transform;
    private Vector3 _startTransformPosition;
    private Quaternion _startTransformRotation;
    
    private void Start()
    {
        _transform = transform;
        _startTransformPosition = _transform.position;
        _startTransformRotation = _transform.rotation;
    }
    
    public void SetToInputPosition() { transform.position = setPosition; }
    public void SetToStartPosition() { transform.position = _startTransformPosition; }
    public void SetPosition(Vector3 newPosition) { transform.position = newPosition; }
    public void SetPosition(Vector3Data newPosition) { transform.position = newPosition.value; }
    public void SetPosition(CharacterData data) { transform.position = data.spawnPosition; }
    public void SetPosition(Transform newPosition) { transform.position = newPosition.position; }
    
    public void SetToInputRotation() { transform.rotation = setRotation; }
    public void SetToStartRotation() { transform.rotation = _startTransformRotation; }
    public void SetRotation(Vector3 newRotation) { transform.position = newRotation; }
    public void SetRotation(Vector3Data newRotation) { transform.position = newRotation.value; }
    public void SetRotation(Transform newRotation) { transform.rotation = newRotation.rotation; }
    
    public void SetToInputs()
    {
        SetToInputPosition();
        SetToInputRotation();
    }
    
    public void ResetToStartTransform()
    {
        SetToStartPosition();
        SetToStartRotation();
    }

    public Vector3 GetPosition() { return transform.position; }
    private Vector3 GetStartPosition() { return _startTransformPosition; }
    private Quaternion GetStartRotation() { return _startTransformRotation; }
}
