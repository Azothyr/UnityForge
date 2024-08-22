using UnityEngine;
using UnityEngine.Events;

public class HealthBehavior : MonoBehaviour, IDamagable
{
    public UnityEvent onHealthGained, onHealthLost, onThreeQuarterHealth, onHalfHealth, onQuarterHealth, onHealthDepleted;
    
    [SerializeField] [ReadOnly] private float currentHealth;
    private float _previousCheckHealth;
    public float maxHealth;

    public float health
    {
        get => currentHealth;
        set => currentHealth = value;
    }

    private void Start()
    {
        if (maxHealth <= 0) maxHealth = 1;
        health = maxHealth;
    }
    
    private void CheckHealthEvents()
    {
        if (health > maxHealth) health = maxHealth;
        if (health == maxHealth) return;
        if (health < 0) health = 0;
        
        if (health <= maxHealth * 0.75f && health >= maxHealth * 0.5f) onThreeQuarterHealth.Invoke();
        else if (health <= maxHealth * 0.5f && health >= maxHealth * 0.25f) onHalfHealth.Invoke();
        else if (health <= maxHealth * 0.25f && health > maxHealth * 0) onQuarterHealth.Invoke();
        else if (health <= 0) onHealthDepleted.Invoke();
    }
    
    public void SetHealth(float newHealth)
    {
        health = newHealth;
        CheckHealthEvents();
    }
    
    public void SetMaxHealth(float newMax)
    {
        maxHealth = newMax;
    }

    public void AddAmountToHealth(float amount)
    {
        var previousHealth = health;
        health += amount;
        var change = health - previousHealth;
        if (change > 0) onHealthGained.Invoke();
        else if (health > 0) onHealthLost.Invoke();
        CheckHealthEvents();
    }

    public void TakeDamage(float amount)
    {
        if (amount > -1) amount *= -1;
        AddAmountToHealth(amount);
    }

    public void TakeDamage(IDamageDealer dealer)
    {
        var damage = dealer.damage;
        if (damage > -1) damage *= -1;
        AddAmountToHealth(damage);
    }
}