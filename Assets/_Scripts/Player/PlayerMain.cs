using UnityEngine;

public class PlayerMain : Unit
{
    public float            speed = 10;
    public float            attackPower = 50;


    public static PlayerMain instance; // singleton


    private void Awake()
    {
        if ( instance != null )
        {
            Debug.Log("Player: instance already yet");
            return;
        }
        instance = this;

        SetMaxHp(100f, true);             //... start values
        SetMaxEnergy(100f, true);         //... start values
    }


    protected override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    protected override void EnergyChange(float value)
    {
        base.EnergyChange(value);
    }

    protected override void Death()
    {
        //.. endgame
    }
}
