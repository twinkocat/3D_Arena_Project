using System.Collections;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField] private GameObject             _playerLaser;
    [SerializeField] private Transform              _firePoint; 
    [SerializeField] private LayerMask              _enemyTargetMask; 
    [SerializeField] private LayerMask              _environmentTargetMask; 
    private float                                   _fireRange = 50f;
    private float                                   _attackPower;
    private const float                             _laserDuration = 0.25f;
    private Vector3                                 _laserOffset = new Vector3(0.04f, -0.04f, 0f);
    private Vector3[]                               _points;


    private void Start()
    {
        _attackPower = PlayerMain.instance.attackPower;
        _points = new Vector3[2];
    }

    public void Shoot()
    {
        RaycastHit hit;

        _points[0] = _firePoint.transform.position + _laserOffset;

        if (Physics.Raycast(_firePoint.position, _firePoint.forward, out hit, _fireRange, _enemyTargetMask))
        {
            _points[1] = hit.transform.position;
            hit.transform.GetComponent<Unit>().TakeDamage(_attackPower);
        }
        else if (Physics.Raycast(_firePoint.position, _firePoint.forward, out hit, _fireRange, _environmentTargetMask))
        {
            _points[1] = hit.point;
        }
        else
        {
            _points[1] = _firePoint.transform.forward * _fireRange;
        }

        GameObject laser = Instantiate(_playerLaser, transform);
        laser.GetComponent<LaserVisual>().points = _points;
        laser.GetComponent<LaserVisual>().laserDuration = _laserDuration;
    }

    
}
