using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CannonManager : MonoBehaviour
{
    private WaitForFixedUpdate _wffu;
    
    public UnityEvent onSuccessfulFire;
    
    public GameObject ammoPrefab;
    public float fireForce;
    public Vector3 fireDirection;
    public Transform firePoint;
    public SocketMatchInteractor ammoSocket;
    
    private List <GameObject> _currentAmmoList;
    private bool _isLoaded;
    private GameObject _ammoObj;
    private Coroutine _addForceCoroutine; 

    private void Awake()
    {
        _wffu = new WaitForFixedUpdate();
        _addForceCoroutine = null;
    }

    public void Fire()
    {
        // var ammoObj = ammoSocket.RemoveAndMoveSocketObject(firePoint.position, firePoint.rotation);
        if(_ammoObj == null) {Debug.LogWarning("NO AMMO IN CANNON " + gameObject.name); return;}
        if (!_isLoaded) {Debug.LogWarning($"{gameObject.name} HAS NO AMMO."); return;}
        
        var ammoRb = _ammoObj.GetComponent<Rigidbody>();
        
        if (_addForceCoroutine != null){ _ammoObj.SetActive(false); return;}
        _ammoObj.SetActive(true);
        onSuccessfulFire.Invoke();
        _addForceCoroutine = StartCoroutine(AddForceToAmmo(ammoRb));
        UnloadCannon();
    }

    private GameObject GetAmmo()
    {
        _currentAmmoList ??= new List<GameObject>();
        foreach (var ammoObj in _currentAmmoList)
        {
            if (ammoObj.activeSelf) continue;
            ammoObj.transform.position = firePoint.position;
            ammoObj.transform.rotation = firePoint.rotation;
            return ammoObj;
        }
        var newAmmo = Instantiate(ammoPrefab, firePoint.position, firePoint.rotation);
        _currentAmmoList.Add(newAmmo);
        return newAmmo;
    }
    
    private IEnumerator AddForceToAmmo(Rigidbody ammoRb)
    {
        ammoRb.isKinematic = false;
        yield return _wffu;
        yield return _wffu;
        yield return _wffu;
        yield return null;
        ammoRb.AddForce(fireDirection * fireForce, ForceMode.Impulse);
        _addForceCoroutine = null; 
    }

    public void LoadCannon()
    {
        _isLoaded = true;
        _ammoObj = GetAmmo();
    }

    private void UnloadCannon()
    {
        _isLoaded = false;
    }
}