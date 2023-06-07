using UnityEngine;

public class PlayerRotationController : MonoBehaviour
{
    [SerializeField] private Camera         _playerCamera;
    
    private float                           _cameraRotation;
    private float                           _playerRotation;
    private static float                    _sensitivityX;
    private static float                    _sensitivityY;
    private float                           _boundCameraAngle = 85f;

    private void Start()
    {
        SaveSensValues();
    }

    private void Update()
    {
        _playerRotation += RotationJoystick.GetHorizontalAxis() * _sensitivityX * Time.deltaTime;
        _cameraRotation += -RotationJoystick.GetVerticalAxis() * _sensitivityY * Time.deltaTime;

        _cameraRotation = Mathf.Clamp(_cameraRotation, -_boundCameraAngle, _boundCameraAngle);

        transform.rotation = Quaternion.Euler(0f, _playerRotation, 0f);
        _playerCamera.transform.localRotation = Quaternion.Euler(_cameraRotation, 0f, 0f);
    }

    public static void SaveSensValues()
    {
        _sensitivityX = GameManager.Instance.SensX;
        _sensitivityY = GameManager.Instance.SensY;
    }
}
