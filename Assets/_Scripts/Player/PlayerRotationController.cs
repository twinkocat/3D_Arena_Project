using UnityEngine;

public class PlayerRotationController : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private float _sensX;
    [SerializeField] private float _sensY;

    private Camera                  _playerCamera;
    private float                   _xRotation;
    private float                   _yRotation;

    private void Start()
    {
        _playerCamera = Camera.main;
        
        if (_playerCamera == null)
        {
            Debug.Log("Set tag <Main camera> to the desired camera");
            return;
        }
    }

    private void Update()
    {
        _xRotation += -RotationJoystick.inputVector.z * _sensY * Time.deltaTime;
        _yRotation += RotationJoystick.inputVector.x * _sensX * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0f, _yRotation, 0f);
        _playerCamera.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
    }
}
