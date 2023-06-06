using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Slider _xSensSlider;
    [SerializeField] private Slider _ySensSlider;

    public void ShowSettings()
    {
        gameObject.SetActive(true);

        _xSensSlider.minValue = GameManager.Instance.MIN_SENS;
        _xSensSlider.maxValue = GameManager.Instance.MAX_SENS;
        _xSensSlider.value = GameManager.Instance.SensX;

        _ySensSlider.minValue = GameManager.Instance.MIN_SENS;
        _ySensSlider.maxValue = GameManager.Instance.MAX_SENS;
        _ySensSlider.value = GameManager.Instance.SensY;
    }

    public void HideSetting()
    {
        gameObject.SetActive(false);

        GameManager.Instance.SaveSens(nameof(GameManager.Instance.SensX), _xSensSlider.value);
        GameManager.Instance.SaveSens(nameof(GameManager.Instance.SensY), _ySensSlider.value);
        PlayerRotationController.SaveSensValues();
    }
}
