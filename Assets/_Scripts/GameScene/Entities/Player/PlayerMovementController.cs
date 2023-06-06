using UnityEngine;

public class PlayerMovementController : MonoBehaviour, IMovable
{
    [SerializeField] private float                  _speed;
    private Rigidbody                               _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        float _xAxis = MovementJoystick.GetHorizontalAxis() * _speed * Time.deltaTime;
        float _yAxis = MovementJoystick.GetVerticalAxis() * _speed * Time.deltaTime;

        Vector3 move = transform.right * _xAxis + transform.forward * _yAxis;
        _rigidbody.velocity = move;
    }

    public float Speed
    {
        get { return _speed; }
        private set { _speed = value; }
    }
}
