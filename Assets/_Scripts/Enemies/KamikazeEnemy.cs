using UnityEngine;
using System.Collections;

public class KamikazeEnemy : Unit, IEnemy
{
    private GameObject          _currentTarget;

    private Vector3             _leapJumpHeight;
    private Vector3[]           _leapPoints;
    private const float         _attackPower = 15f;
    private const float         _leapTime = 5f;
    private const float         _leapHeight = 3f;
    private const float         _leapStartDelay = 0.25f; //  player reaction delay


    private void Awake()
    {
        SetMaxHp(50f, true);
    }

    protected override void Start()
    {
        base.Start();
        heathBarUpdater.gameObject.AddComponent<LookAtPlayer>();
        
        _leapPoints = new Vector3[3];

        _leapJumpHeight = new Vector3(transform.position.x, 
                                        transform.position.y + _leapHeight, 
                                        transform.position.z);
        
        FindTarget();
        Invoke("EnemyAttack", _leapStartDelay);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    protected override void Death()
    {
        base.Death();
        PlayerMain.instance.EnergyChange(15);
    }

    public void FindTarget()
    {
        _currentTarget = GameObject.Find("Player");
    }

    public void EnemyMove()
    {

    }

    public void EnemyAttack()
    {
        _leapPoints[0] = transform.position;
        _leapPoints[1] = _leapJumpHeight;

        float startTime = Time.time;
        float u = 0f;

        StartCoroutine(KamikazeLeap(startTime, _leapTime, u));
    }

    IEnumerator KamikazeLeap(float startTime, float moveTime, float u)
    {
        while (u < 1)
        {
            _leapPoints[2] = _currentTarget.transform.position;

            u = Mathf.Min(((Time.time - startTime) / moveTime), 1);
            Vector3 p01, p12;

            p01 = (1 - u) * _leapPoints[0] + u * _leapPoints[1];
            p12 = (1 - u) * _leapPoints[1] + u * _leapPoints[2];
            transform.position = (1 - u) * p01 + u * p12;
            
            yield return new WaitForFixedUpdate();
        }


        _currentTarget.GetComponent<Unit>().TakeDamage(_attackPower);
        Destroy(gameObject);
    }
}
