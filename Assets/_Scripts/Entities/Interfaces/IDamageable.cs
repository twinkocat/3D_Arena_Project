using System;

public interface IDamageable
{
    float Health { get; }
    float MaxHealth { get; }
    void SetMaxHealth (float maxHealth);
    void TakeDamage(float damage);
    void Death();

    event Action<float> OnHealthChanged;
    event Action<float> OnMaxHealthChanged;
}
