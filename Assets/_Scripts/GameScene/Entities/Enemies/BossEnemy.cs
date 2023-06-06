using Redcode.Pools;
using UnityEngine;

public class BossEnemy : Unit, IEnemy, IAttackable, IFindTarget, IPoolObject
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
    }

    public void OnGettingFromPool()
    {
        if (Health != MaxHealth)
            TakeHeal(MaxHealth);
        
        FindTarget();
        Attack();
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Shoot));
    }

    public void FindTarget()
    {
        _currentTarget = Player.Instance.gameObject;
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

    public void OnCreatedInPool()
    {
        throw new System.NotImplementedException();
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
