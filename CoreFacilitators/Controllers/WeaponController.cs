using System;
using UnityEngine;
using UnityEngine.Events;

public class WeaponController : MonoBehaviour, IDamagable, IDamageDealer
{
    public WeaponData weaponData;
    public UnityEvent onDamageDealt, onDurabilityDepleted;
    
    public float damage
    {
     get => weaponData.damage;
     set => weaponData.damage = value;
    }

    public float health
    {
     get => weaponData.health;
     set => weaponData.health = value;
    }
    
    public bool canDealDamage { get; set; }

    private void OnCollisionEnter(Collision other)
    {
        var damagableObj = other.gameObject.GetComponent<IDamagable>();
        if (damagableObj != null) { DealDamage(damagableObj); }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0) onDurabilityDepleted.Invoke();
    }

    public void TakeDamage(IDamageDealer dealer)
    {
        TakeDamage(dealer.damage);
    }
    
    public void DealDamage(IDamagable target)
    {
        DealDamage(target, damage);
    }

    public void DealDamage(IDamagable target, float amount)
    {
        if (!canDealDamage) return;
        onDamageDealt.Invoke();
        target.TakeDamage(amount);
    }

    private void OnEnable()
    {
        canDealDamage = true;
    }
}
