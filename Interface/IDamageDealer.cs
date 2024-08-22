public interface IDamageDealer
{
    float damage { get; set; }
    float health { get; set; }
    void DealDamage(IDamagable target, float amount);
    void DealDamage(IDamagable target);
}
