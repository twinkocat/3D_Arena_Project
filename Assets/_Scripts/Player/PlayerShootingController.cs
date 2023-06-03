using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField] private Transform              _firePoint; 
    [SerializeField] private LayerMask              _targetMask; 
    private float                                   _fireRange = 100f;
    private float                                   _attackPower;

    private void Start()
    {
        _attackPower = PlayerMain.instance.attackPower;
        
    }

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(_firePoint.position, _firePoint.forward, out hit, _fireRange, _targetMask))
        {
            hit.transform.GetComponent<Unit>().TakeDamage(_attackPower);
        }
    }
}
