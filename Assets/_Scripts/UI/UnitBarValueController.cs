using UnityEngine;
using UnityEngine.UI;

public class UnitBarValueController : MonoBehaviour
{
    private Slider _barSlider;

    private void Awake()
    {
        _barSlider = GetComponent<Slider>();
    }
    
    public void OnHealthChangedHandler(IDamageable unit, bool isUnsubscribe = false)
    {
        if (isUnsubscribe)
        {
            unit.OnHealthChanged -= UpdateBarValue;
            return;
        }
        unit.OnHealthChanged += UpdateBarValue;
    }
    
    public void OnMaxHealthHandler(IDamageable unit, bool isUnsubscribe = false)
    {
        if (isUnsubscribe)
        {
            unit.OnMaxHealthChanged -= SetBarMaxValue;
            return;
        }
        unit.OnMaxHealthChanged += SetBarMaxValue;
    }
    
    public void OnEnergyHandler(IEnergy unit, bool isUnsubscribe = false)
    {
        if (isUnsubscribe)
        {
            unit.OnEnergyChanged -= UpdateBarValue;
            return;
        }
        unit.OnEnergyChanged += UpdateBarValue;
    }
    
    public void OnMaxEnergyHandler(IEnergy unit, bool isUnsubscribe = false)
    {
        if (isUnsubscribe)
        {
            unit.OnMaxEnergyChanged -= SetBarMaxValue;
            return;
        }
        unit.OnMaxEnergyChanged += SetBarMaxValue;
    }

    private void UpdateBarValue(float value)
    {
        _barSlider.value = value;
    }

    private void SetBarMaxValue(float value)
    {
        _barSlider.maxValue = value;
    }
}
