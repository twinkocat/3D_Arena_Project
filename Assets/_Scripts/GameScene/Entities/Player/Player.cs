using System;
using UnityEditor;
using UnityEngine;

public class Player : Unit, IEnergy
{
    public bool isTeleported;

    [SerializeField] private float _energy;
    [SerializeField] private float _maxEnergy;
    [SerializeField] private UnitBarValueController _energyBar;
    [Space]
    [SerializeField] private float _ricochetChance;
    [SerializeField] private float RICOCHET_MIN_CHANCE;
    [SerializeField] private float RICOCHET_MAX_CHANCE;

    public event Action<float>      OnEnergyChanged;
    public event Action<float>      OnMaxEnergyChanged;


    public static Player            Instance; // singleton

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Player: instance already yet");
            return;
        }
        Instance = this;
    }

    protected override void Start()
    {
        base.Start();

        _energyBar.OnEnergyHandler(this);
        _energyBar.OnMaxEnergyHandler(this);

        SetMaxEnergy(_maxEnergy);
        ChangeEnergyValue(50f); // start value

        RicochetChance = RICOCHET_MIN_CHANCE;
    }

    public void ChangeEnergyValue(float value)
    {
        Energy = Mathf.Clamp(Energy + value, 0f, MaxEnergy);
        OnEnergyChanged?.Invoke(Energy);
    }

    public void SetMaxEnergy(float value)
    {
        MaxEnergy = value;
        OnMaxEnergyChanged?.Invoke(MaxEnergy);
    }

    public void GetBounty(Unit unit)
    {
        Debug.Log(((IEnemy)unit).Bounty);
        ChangeEnergyValue(((IEnemy)unit).Bounty);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        RicochetChance = Mathf.Lerp(RICOCHET_MAX_CHANCE, RICOCHET_MIN_CHANCE, Health / MaxHealth);
    }

    public override void TakeHeal(float value)
    {
        base.TakeHeal(value);
        RicochetChance = Mathf.Lerp(RICOCHET_MAX_CHANCE, RICOCHET_MIN_CHANCE, Health / MaxHealth);
    }

    public override void Death() // event
    {
        Debug.Log("isDead");
    }

    public float RicochetChance
    {
        get { return _ricochetChance; }
        private set { _ricochetChance = value; }
    }

    public float Energy
    {
        get { return _energy; }
        private set { _energy = Mathf.Clamp(value, 0f, MaxEnergy); }
    }

    public float MaxEnergy
    {
        get { return _maxEnergy; }
        private set
        {
            _maxEnergy = value;
            if (!_isInit)
            {
                Energy = value;
                _isInit = true;
            }
        }
    }
}
