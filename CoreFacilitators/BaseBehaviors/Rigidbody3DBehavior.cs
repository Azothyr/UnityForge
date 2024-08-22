using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rigidbody3DBehavior : MonoBehaviour
{
    public Rigidbody rigidBody;
    public bool useGravity;
    
    private void Start()
    {
        if (!rigidBody) rigidBody = GetComponent<Rigidbody>();
        if (!rigidBody) Debug.LogError("No Rigidbody found on " + gameObject.name);
        SetGravity(useGravity);
    }
    
    public void AddForce(Vector3 dir, float power)
    {
        AddForce(dir, power, ForceMode.Impulse);
    }
    
    public void AddForce(Vector3 dir, float power, ForceMode mode)
    {
        rigidBody.AddForce(dir * power, mode);
    }
    
    public void SetGravity(bool value)
    {
        rigidBody.useGravity = value;
    }
    
    public void ToggleGravity()
    {
        rigidBody.useGravity = !rigidBody.useGravity;
    }
    
    public void ZeroOutVelocity()
    {
        rigidBody.velocity = Vector3.zero;
    }
    
    public void ZeroOutAngularVelocity()
    {
        rigidBody.angularVelocity = Vector3.zero;
    }
    
    public void FreezeRigidbody()
    {
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
    }
    
    public void UnFreezeRigidbody()
    {
        rigidBody.constraints = RigidbodyConstraints.None;
    }
}
