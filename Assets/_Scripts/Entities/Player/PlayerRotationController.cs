using UnityEngine;

public class PlayerRotationController : MonoBehaviour
{
    [SerializeField] private float          _sensX;
    [SerializeField] private float          _sensY;
    [SerializeField] private Camera         _playerCamera;
    
    private float                           _cameraRotation;
    private float                           _playerRotation;

    private void Update()
    {
        _cameraRotation += -RotationJoystick.GetVerticalAxis() * _sensY * Time.deltaTime;
        _playerRotation += RotationJoystick.GetHorizontalAxis() * _sensX * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0f, _playerRotation, 0f);
        _playerCamera.transform.localRotation = Quaternion.Euler(_cameraRotation, 0f, 0f);
    }
}
