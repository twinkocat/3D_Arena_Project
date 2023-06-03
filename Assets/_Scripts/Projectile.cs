using System.Collections;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    public GameObject           currentTarget;
    public float                attackPower;

    private bool                _isCollide;
    private Vector3             _currentTargetPosition;
    private const float         _projectileSpeed = 1f;


    private void Start()
    {
        _isCollide = false;
        StartCoroutine(ProjectileMoving());
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == "Player")
        {
            PlayerMain.instance.EnergyChange(-attackPower);
            Destroy(gameObject);
        } 
        else if (collision.collider.tag == "Environment")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ProjectileMoving()
    {
        while (!_isCollide)
        {
            if (!PlayerMain.instance.isTeleported)
            {
                _currentTargetPosition = currentTarget.transform.position;
            }
            transform.position = Vector3.MoveTowards(transform.position, _currentTargetPosition, _projectileSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

}
