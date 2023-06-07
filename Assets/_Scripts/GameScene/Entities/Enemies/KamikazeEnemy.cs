using UnityEngine;
using System.Collections;
using Redcode.Pools;

public class KamikazeEnemy : Unit, IEnemy, IFindTarget, IAttackable, IPoolObject
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
    private IEnumerator             _leapCoro;

    public void OnGettingFromPool()
    {
        _leapPoints = new Vector3[3];
        _leapHeight = new Vector3(transform.position.x,
                                    transform.position.y + _leapHeightY,
                                    transform.position.z);
        if (Health != MaxHealth)
            TakeHeal(MaxHealth);

        FindTarget();
        Invoke(nameof(Attack), _attackDelay);
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

        _leapCoro = KamikazeLeap(startTime, _leapTime, u);
        StartCoroutine(_leapCoro);
    }

    IEnumerator KamikazeLeap(float startTime, float moveTime, float u)
    {
        Vector3 p01, p12;

        while (u < 1 && gameObject.activeInHierarchy)
        {
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
