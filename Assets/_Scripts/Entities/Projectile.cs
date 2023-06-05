using System.Collections;
using UnityEngine;

public enum TypeOfDamage
{
    Health = 0,
    Energy,
}

public class Projectile : MonoBehaviour
{
    public TypeOfDamage                     TypeOfDamage;
    public GameObject                       CurrentTarget;
    public float                            AttackPower;

    private bool                            _isCollide;
    private Vector3                         _currentTargetPosition;
    [SerializeField] private float          _projectileSpeed;


    private void Start()
    {
        StartCoroutine(ProjectileMoving());
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.TryGetComponent<Player>(out Player player))
        {
            switch (TypeOfDamage)
            {
                case TypeOfDamage.Health:
                    Player.Instance.TakeDamage(AttackPower);
                    break;
                case TypeOfDamage.Energy:
                    Player.Instance.ChangeEnergyValue(-AttackPower);
                    break;
            }
           
            _isCollide = true;
            Destroy(gameObject);
        } 
        else if (collision.collider.TryGetComponent<Environment>(out Environment environment))
        {
            _isCollide = true;
            Destroy(gameObject);
        }
    }

    IEnumerator ProjectileMoving()
    {
        while (!_isCollide)
        {
            if (!Player.Instance.isTeleported)
            {
                _currentTargetPosition = CurrentTarget.transform.position;
            }
            transform.position = Vector3.MoveTowards(transform.position, _currentTargetPosition, _projectileSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

}
