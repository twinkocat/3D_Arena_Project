using UnityEngine;

public class BossEnemy : Unit, IEnemy
{
    [SerializeField] private GameObject     _projectile;
    private GameObject                      _currentTarget;
    private const float                     _attackPower = 15f;
    private const float                     _attackDelay = 3f;

    private void Awake()
    {
        SetMaxHp(100f, true);
    }

    protected override void Start()
    {
        base.Start();

        heathBarUpdater.gameObject.AddComponent<LookAtPlayer>();

        FindTarget();
        EnemyAttack();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    protected override void Death()
    {
        base.Death();
        PlayerMain.instance.EnergyChange(50);
    }

    public void FindTarget()
    {
        _currentTarget = GameObject.Find("Player");
    }

    public void EnemyMove()
    {
        throw new System.NotImplementedException();
    }

    public void EnemyAttack()
    {
        InvokeRepeating("Shot", _attackDelay, _attackDelay);
    }

    private void Shot()
    {
        GameObject go = Instantiate(_projectile, transform.position, Quaternion.identity);
        go.AddComponent<Projectile>();
        go.GetComponent<Projectile>().currentTarget = _currentTarget;
        go.GetComponent<Projectile>().attackPower = _attackPower;
    }
}
