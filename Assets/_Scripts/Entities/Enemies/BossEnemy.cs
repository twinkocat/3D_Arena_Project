using UnityEngine;

public class BossEnemy : Unit, IEnemy, IAttackable, IFindTarget
{
    [Space]
    [SerializeField] private float      _attackPower;
    [SerializeField] private float      _attackDelay;
    [SerializeField] private float      _attackRange;
    [Space]
    [SerializeField] private Projectile _projectile;
    [SerializeField] private float      _bounty;

    private GameObject                  _currentTarget;

    protected override void Start()
    {
        base.Start();

        FindTarget();
        Shoot();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    public override void Death()
    {
        base.Death();
        Player.Instance.ChangeEnergyValue(_bounty);
    }

    public void FindTarget()
    {
        _currentTarget = Player.Instance.gameObject;
    }

    public void EnemyMove()
    {
        throw new System.NotImplementedException();
    }

    public void Attack()
    {
        Invoke(nameof(Shoot), _attackDelay);
    }

    private void Shoot()
    {
        Projectile projectile = Instantiate(_projectile, transform.position, Quaternion.identity);

        projectile.CurrentTarget = _currentTarget;
        projectile.AttackPower = _attackPower;
        projectile.TypeOfDamage = TypeOfDamage.Energy;

        Invoke(nameof(Shoot), _attackDelay);
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

    public float Bounty
    {
        get { return _bounty; }
        private set { _bounty = value; }
    }

    public GameObject CurrentTarget
    {
        get { return _currentTarget; }
        private set { _currentTarget = value; }
    }
}
