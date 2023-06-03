using UnityEngine;

public class Unit : MonoBehaviour
{

    public BarValueUpdater          heathBarUpdater;
    public BarValueUpdater          energyBarUpdater;

    public float                    curEnergy;
    public float                    maxEnergy;
    public float                    maxHealth;
    public float                    curHealth;


    private void Start()
    {
        if (heathBarUpdater != null)
        {
            heathBarUpdater.MaxBarValue(maxHealth);
            heathBarUpdater.UpdateBarValue(curHealth);
        }
        if (energyBarUpdater != null)
        {
            energyBarUpdater.MaxBarValue(maxEnergy);
            energyBarUpdater.UpdateBarValue(curEnergy);
        }
    }

    protected virtual void TakeDamage(float damage)
    {
        if (curHealth < 0)
        {
            curHealth = 0;
            Death();
        }
        else
        {
            curHealth -= damage;
            heathBarUpdater.UpdateBarValue(curHealth);
        }
    }

    protected virtual void EnergyChange(float value)
    {
        if (curEnergy < 0)
        {
            curEnergy = 0;
        }
        else
        {
            curEnergy -= value;
            energyBarUpdater.UpdateBarValue(curEnergy);
        }
    }

    protected virtual void SetMaxHp(float value, bool isInit = false)
    {
        maxHealth = value;
        if(isInit)
        {
            curHealth = value;
        }
    }

    protected virtual void SetMaxEnergy(float value, bool isInit = false)
    {
        maxEnergy = value;
        if (isInit)
        {
            curEnergy = value;
        }
    }

    protected virtual void Death() { }
}
