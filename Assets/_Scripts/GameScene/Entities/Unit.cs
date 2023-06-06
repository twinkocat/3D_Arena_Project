using System;
using UnityEngine;

public class Unit : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private UnitBarValueController _healthBar;
    [SerializeField] private float                  _health;
    [SerializeField] private float                  _maxHealth;

    protected bool                                  _isInit;

    public event Action<Unit>                       OnUnitDeath;
    public event Action<Unit>                       OnUnitDeathFromDamage;
    public event Action<float>                      OnHealthChanged;
    public event Action<float>                      OnMaxHealthChanged;

    protected virtual void Start()
    {
        _healthBar.OnHealthChangedHandler(this);
        _healthBar.OnMaxHealthHandler(this);

        SetMaxHealth(_maxHealth);
        TakeDamage(0f); // initialization
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
        OnHealthChanged?.Invoke(Health);

        if (Health <= 0)
        {
            OnUnitDeathFromDamage?.Invoke(this);
            Death();
        }
    }

    public virtual void TakeHeal(float value)
    {
        Health += Mathf.Clamp(value, 0f, MaxHealth - Health);
        OnHealthChanged?.Invoke(Health);
    }

    public virtual void SetMaxHealth(float value)
    {
        MaxHealth = value;
        OnMaxHealthChanged?.Invoke(MaxHealth);
    }

    public virtual void Death()
    {
        OnUnitDeath?.Invoke(this);
    }

    public float Health 
    { 
        get { return _health; }
        private set { _health = Mathf.Clamp(value, 0f, MaxHealth); }
    }

    public float MaxHealth
    {
        get { return _maxHealth; }
        private set 
        { 
            _maxHealth = value; 
            if(!_isInit)
            {
                Health = value;
                _isInit = true;
            }
        }
    }
}
