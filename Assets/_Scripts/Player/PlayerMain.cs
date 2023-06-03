using UnityEngine;

public class PlayerMain : Unit
{
    public float            speed = 10f;
    public float            attackPower;
    public bool             isTeleported;

    public static PlayerMain instance; // singleton

    private void Awake()
    {
        if ( instance != null )
        {
            Debug.Log("Player: instance already yet");
            return;
        }
        instance = this;

        attackPower = 50f;

        SetMaxHp(100f, true);             //... start values
        SetMaxEnergy(100f, true);         //... start values

        isTeleported = false;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    public override void EnergyChange(float value)
    {
        base.EnergyChange(value);
    }

    protected override void Death()
    {
        Debug.Log("isDead");
    }
}
