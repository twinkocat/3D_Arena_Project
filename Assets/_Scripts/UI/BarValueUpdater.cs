using UnityEngine;
using UnityEngine.UI;

public class BarValueUpdater : MonoBehaviour
{
    private Slider _barSlider;

    private void Awake()
    {
        _barSlider = GetComponent<Slider>();
    }

    public void UpdateBarValue(float value)
    {
        _barSlider.value = value;
    }

    public void MaxBarValue(float value)
    {
        _barSlider.maxValue = value;
    }
}
