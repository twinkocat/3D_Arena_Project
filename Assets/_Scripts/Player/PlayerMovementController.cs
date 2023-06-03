using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private float                   _xAxis;
    private float                   _yAxis;
    private Vector3                 _move;

    private void FixedUpdate()
    {
        _xAxis = MovementJoystick.inputVector.x * PlayerMain.instance.speed * Time.deltaTime;
        _yAxis = MovementJoystick.inputVector.z * PlayerMain.instance.speed * Time.deltaTime;

        _move = transform.right * _xAxis + transform.forward * _yAxis;

        transform.position += _move * Time.deltaTime;
    }
}
