using System;

public interface IEnergy
{
    float Energy { get; }
    float MaxEnergy { get; }
    void SetMaxEnergy(float maxEnergy);
    void ChangeEnergyValue(float value);

    event Action<float> OnEnergyChanged;
    event Action<float> OnMaxEnergyChanged;
}
