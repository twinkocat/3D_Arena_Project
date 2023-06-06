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
    private float                                   _nextAttackTime;

    public void Attack()
    {
        Vector3[] points = new Vector3[2];

        if (Time.time >= _nextAttackTime)
        {

            points[0] = _firePoint.transform.position + _laserSpawnPointOffset;
            points[1] = RaycastAndTakeDamage(points, _firePoint.forward);

            if (Random.Range(0, 100) <= Player.Instance.RicochetChance)
                Ricochet(points[1]);
            
            _nextAttackTime = Time.time + AttackDelay;
        }
    }

    private void Ricochet(Vector3 firePoint)
    {
        Unit unit = EnemySpawner.Instance.GetNearestEnemy(firePoint);
        if (unit == null)
        {
            return;
        }

        Vector3[] points = new Vector3[2];
        Vector3 direction = (unit.transform.position - firePoint).normalized;
        points[0] = firePoint;
        RaycastAndTakeDamage(points, direction);
    }

    private Vector3 RaycastAndTakeDamage(Vector3[] points, Vector3 direction)
    {
        RaycastHit hit;

        if (Physics.Raycast(points[0], direction, out hit, float.MaxValue, _enemyTargetMask))
        {
            points[1] = hit.transform.position;
            hit.transform.GetComponent<Unit>().TakeDamage(_attackPower);
        }
        else if (Physics.Raycast(points[0], direction, out hit, float.MaxValue, _environmentTargetMask))
        {
            points[1] = hit.point;
        }
        else
        {
            points[1] = _firePoint.transform.forward * AttackRange;
        }

        ProcessLaser(points);
        return points[1];
    }

    private void ProcessLaser(Vector3[] points)
    {
        LaserVisual laser = Instantiate(_playerLaser, transform);
        laser.Points = points;
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
