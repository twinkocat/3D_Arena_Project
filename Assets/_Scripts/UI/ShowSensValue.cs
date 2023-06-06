using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowSensValue : MonoBehaviour
{
    [SerializeField] private Slider _sensitivitySlider;
    
    private TMP_Text                _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void UpdateText()
    {
        _text.text = _sensitivitySlider.value.ToString();
    }
    
}
