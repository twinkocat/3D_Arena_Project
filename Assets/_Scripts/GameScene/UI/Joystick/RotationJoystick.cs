using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotationJoystick : VirtualJoystick
{
    private static Vector3      _inputVector;
    private Image               _touchArea;
    private Image               _joystickBackgroundImage;
    private Image               _joystickHandle;
    private Vector2             _defaultJoystickBackgroundImagePosition;

    private void Awake()
    {
        _touchArea = GetComponent<Image>();
        _joystickBackgroundImage = transform.GetChild(0).GetComponent<Image>();
        _joystickHandle = _joystickBackgroundImage.transform.GetChild(0).GetComponent<Image>();

        _defaultJoystickBackgroundImagePosition = _joystickBackgroundImage.rectTransform.anchoredPosition;
    }

    public static float GetVerticalAxis()
    {
        return _inputVector.z;
    }

    public static float GetHorizontalAxis()
    {
        return _inputVector.x;
    }

    override public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackgroundImage.rectTransform
                                                                    ,eventData.position
                                                                    ,eventData.pressEventCamera
                                                                    ,out pos))
        {
            pos.x = (pos.x / (_joystickBackgroundImage.rectTransform.sizeDelta.x * 0.33f));  // 0.33f its a multiplier for shortly axis range
            pos.y = (pos.y / (_joystickBackgroundImage.rectTransform.sizeDelta.y * 0.33f));

            _inputVector = new Vector3(Mathf.Clamp(pos.x, -1f, 1f),                          // bounded -1 to 1 for comfortable rotation & movement values
                                        0f,
                                        Mathf.Clamp(pos.y, -1f, 1f));

            _joystickHandle.rectTransform.anchoredPosition = new Vector2(
                                                        _inputVector.x * (_joystickBackgroundImage.rectTransform.sizeDelta.x * 0.33f),
                                                        _inputVector.z * (_joystickBackgroundImage.rectTransform.sizeDelta.y * 0.33f));
        }
    }

    override public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 pos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_touchArea.rectTransform
                                                                    ,eventData.position
                                                                    ,eventData.pressEventCamera
                                                                    ,out pos))
        {
            _joystickBackgroundImage.rectTransform.localPosition = pos;
        }
        OnDrag(eventData);
    }

    override public void OnPointerUp(PointerEventData eventData)
    {
        _inputVector = Vector3.zero;
        _joystickBackgroundImage.rectTransform.anchoredPosition = _defaultJoystickBackgroundImagePosition;
        _joystickHandle.rectTransform.anchoredPosition = Vector2.zero;
    }
}
