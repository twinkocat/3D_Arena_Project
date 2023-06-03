using UnityEngine;

public class Unit : MonoBehaviour
{
    public BarValueUpdater          heathBarUpdater;
    public BarValueUpdater          energyBarUpdater;

    public float                    curEnergy;
    public float                    maxEnergy;
    public float                    maxHealth;
    public float                    curHealth;


    protected virtual void Start()
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

    public virtual void TakeDamage(float damage)
    {
        curHealth -= damage;
        if (curHealth <= 0)
        {
            curHealth = 0;
            Death();
        }
        heathBarUpdater.UpdateBarValue(curHealth);
    }

    public virtual void EnergyChange(float value)
    {

        curEnergy += value + curEnergy <= maxEnergy ? value : maxEnergy - curEnergy;
        if (curEnergy <= 0)
        {
            curEnergy = 0;
        }
        energyBarUpdater.UpdateBarValue(curEnergy);
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

    protected virtual void Death() 
    {
        Destroy(gameObject);
    }
}
