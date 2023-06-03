using TMPro;
using UnityEngine;

public class FPSMonitor : MonoBehaviour
{
    private TMP_Text        _text;
    private const float     _polingTime = 1f;
    private float           _time;
    private int             _frameCount;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        _time += Time.deltaTime;
        _frameCount++;

        if (_time >= _polingTime)
        {
            int frameRate = Mathf.RoundToInt(_frameCount / _time);
            _text.text = $"{frameRate} FPS";
        }
    }
}
