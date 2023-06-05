using System;
using UnityEngine;

public class Player : Unit, IEnergy
{
    public bool isTeleported;

    [SerializeField] private UnitBarValueController _energyBar;
    [SerializeField] private float _energy;
    [SerializeField] private float _maxEnergy;


    public event Action<float> OnEnergyChanged;
    public event Action<float> OnMaxEnergyChanged;


    public static Player Instance; // singleton

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
    }

    public void ChangeEnergyValue(float value)
    {
        Energy = Mathf.Clamp(Energy + value, 0f, MaxEnergy);
        OnEnergyChanged(Energy);
    }

    public void SetMaxEnergy(float value)
    {
        MaxEnergy = value;
        OnMaxEnergyChanged(MaxEnergy);
    }

    public override void Death() // event
    {
        Debug.Log("isDead");
    }

    public float Energy
    {
        get { return _energy; }
        private set { _energy = Mathf.Clamp(value, 0f, MaxEnergy); }
    }

    public void Attack()
    {

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

   

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _energyBar.OnEnergyHandler(this, true);
        _energyBar.OnMaxEnergyHandler(this, true);
    }
}
