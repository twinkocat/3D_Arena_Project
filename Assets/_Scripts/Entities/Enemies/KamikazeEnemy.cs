using UnityEngine;
using System.Collections;

public class KamikazeEnemy : Unit, IEnemy, IFindTarget, IAttackable
{
    [SerializeField] private float  _attackPower;
    [SerializeField] private float  _attackDelay;
    [SerializeField] private float  _attackRange;
    [Space]
    [SerializeField] private float  _leapTime;
    [SerializeField] private float  _leapHeightY;
    [SerializeField] private float  _bounty;

    private GameObject              _currentTarget;
    private Vector3                 _leapHeight;
    private Vector3[]               _leapPoints;

    protected override void Start()
    {
        base.Start();
        _leapPoints = new Vector3[3];
        _leapHeight = new Vector3(transform.position.x, 
                                        transform.position.y + _leapHeightY, 
                                        transform.position.z);
        FindTarget();
        Invoke(nameof(Attack), _attackDelay);
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

    public void Attack()
    {
        _leapPoints[0] = transform.position;
        _leapPoints[1] = _leapHeight;

        float startTime = Time.time;
        float u = 0f;

        StartCoroutine(KamikazeLeap(startTime, _leapTime, u));
    }

    IEnumerator KamikazeLeap(float startTime, float moveTime, float u)
    {
        while (u < 1)
        {
            Vector3 p01, p12;
            u = Mathf.Min(((Time.time - startTime) / moveTime), 1);

            _leapPoints[2] = _currentTarget.transform.position;
            p01 = (1 - u) * _leapPoints[0] + u * _leapPoints[1];
            p12 = (1 - u) * _leapPoints[1] + u * _leapPoints[2];
            transform.position = (1 - u) * p01 + u * p12;
            
            yield return new WaitForFixedUpdate();
        }

        _currentTarget.GetComponent<Unit>().TakeDamage(_attackPower);
        Death();
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
