using UnityEngine;

public class PlayerShootingController : MonoBehaviour, IAttackable
{
    [SerializeField] private LaserVisual            _playerLaser;
    [SerializeField] private Transform              _firePoint;
    [Space]
    [SerializeField] private float                  _attackPower;
    [SerializeField] private float                  _attackDelay;
    [SerializeField] private float                  _attackRange;
    [Space]
    [SerializeField] private LayerMask              _enemyTargetMask; 
    [SerializeField] private LayerMask              _environmentTargetMask; 

    private Vector3                                 _laserSpawnPointOffset = new Vector3(0.04f, -0.04f, 0f);
    private Vector3[]                               _points;
    private float                                   _nextAttackTime;


    private void Start()
    {
        _points = new Vector3[2];
    }

    public void Attack()
    {
        if (Time.time >= _nextAttackTime)
        {
            RaycastHit hit;

            _points[0] = _firePoint.transform.position + _laserSpawnPointOffset;

            if (Physics.Raycast(_firePoint.position, _firePoint.forward, out hit, AttackRange, _enemyTargetMask))
            {
                _points[1] = hit.transform.position;
                hit.transform.GetComponent<Unit>().TakeDamage(_attackPower);
            }
            else if (Physics.Raycast(_firePoint.position, _firePoint.forward, out hit, AttackRange, _environmentTargetMask))
            {
                _points[1] = hit.point;
            }
            else
            {
                _points[1] = _firePoint.transform.forward * AttackRange;
            }

            LaserVisual laser = Instantiate(_playerLaser, transform);
            laser.Points = _points;

            _nextAttackTime = Time.time + AttackDelay;
        }
    }

    public float AttackPower
    {
        get { return _attackPower; }
        private set { _attackPower = value; }
    }

    public float AttackDelay
    {
        get { return _attackDelay; }
        private set { _attackDelay = value; }
    }

    public float AttackRange
    {
        get { return _attackRange; }
        private set { _attackRange = value; }
    }
}
