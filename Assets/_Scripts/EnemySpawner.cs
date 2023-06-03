using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _platform;
    [SerializeField] private GameObject _kamikazeEnemy;
    [SerializeField] private GameObject _bossEnemy;

    private Vector3         _targetPosition;
    private float           _targetRadius;
    private int             _spawnCounter;
    private float           _spawnDelay;

    private void Awake()
    {
        _targetPosition = _platform.transform.position;
        _targetRadius = _platform.transform.localScale.x * 0.5f;

        _spawnCounter = 0;
        _spawnDelay = 5f;

        Invoke("EnemySpawn", _spawnDelay);
    }

    private Vector3 GetRandomPointInArea()
    {
        Vector3 randomPoint = Random.insideUnitSphere;
        randomPoint = randomPoint * _targetRadius + _targetPosition;
        randomPoint.y = _platform.transform.position.y + 2f;
        return randomPoint;
    }

    private void EnemySpawn()
    {
        Vector3 spawnPoint = GetRandomPointInArea();
        if (_spawnCounter == 5)
        {
            Instantiate(_bossEnemy, spawnPoint, Quaternion.identity);
            _spawnCounter = 0;
        }
        else
        {
            Instantiate(_kamikazeEnemy, spawnPoint, Quaternion.identity);
            _spawnCounter++;
        }
        if (_spawnDelay > 2)
        {
            _spawnDelay -= 0.2f;
        }
        Invoke("EnemySpawn", _spawnDelay);
    }


}
