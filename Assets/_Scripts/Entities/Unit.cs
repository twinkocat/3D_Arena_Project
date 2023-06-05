using System;
using UnityEngine;

public class Unit : MonoBehaviour, IDamageable
{
    [SerializeField] private UnitBarValueController _healthBar;
    [SerializeField] private float                  _health;
    [SerializeField] private float                  _maxHealth;

    public event Action<float>                      OnHealthChanged;
    public event Action<float>                      OnMaxHealthChanged;

    protected bool                                  _isInit;

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
        OnHealthChanged(Health);
        if (Health <= 0)
        {
            Death();
        }
    }

    public virtual void SetMaxHealth(float value)
    {
        MaxHealth = value;
        OnMaxHealthChanged(MaxHealth);
    }

    public virtual void Death()
    {
        Destroy(gameObject);
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

    protected virtual void OnDestroy()
    {
        _healthBar.OnHealthChangedHandler(this, true);
        _healthBar.OnMaxHealthHandler(this, true);
    }
}
