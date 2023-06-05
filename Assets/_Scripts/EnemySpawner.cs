using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _platform; 
    [SerializeField] private GameObject _kamikazeEnemy;
    [SerializeField] private GameObject _bossEnemy;
    [Space]
    [SerializeField] private int        _numberWhenBossSpawn; // every <number> should spawn boss.
    [SerializeField] private float      _mixSpawnDelay;
    [SerializeField] private float      _maxSpawnDelay;
    [SerializeField] private float      _spawnTimeReducer;
    [SerializeField] private float      _enemySpawnHeight;

    private int                         _spawnCounter;
    private Vector3                     _targetPosition;
    private float                       _targetRadius;

    private void Awake()
    {
        _targetPosition = _platform.transform.position;
        _targetRadius = _platform.transform.localScale.x * 0.5f;

        _spawnCounter = 0;
        _maxSpawnDelay = 5f;

        Invoke(nameof(EnemySpawn), _maxSpawnDelay);
    }

    //physics overlap
    private Vector3 GetRandomPointInArea()
    {
        Vector3 randomPoint = Random.insideUnitSphere;
        randomPoint = randomPoint * _targetRadius + _targetPosition;
        randomPoint.y = _platform.transform.position.y + _enemySpawnHeight;
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
        if (_maxSpawnDelay > _mixSpawnDelay)
        {
            _maxSpawnDelay -= _spawnTimeReducer;
        }
        Invoke(nameof(EnemySpawn), _maxSpawnDelay);
    }
}
