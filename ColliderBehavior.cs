using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ColliderBehavior : MonoBehaviour
{
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }
    
    public void SetState(bool value)
    {
        _collider.enabled = value;
    }

    private void OnEnable()
    {
        _collider.enabled = true;
    }
}
