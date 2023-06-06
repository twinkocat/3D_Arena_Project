using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _platform; 
    [Space]
    [SerializeField] private int        _numberWhenBossSpawn; // every <number> should spawn boss.
    [SerializeField] private float      _minSpawnDelay;
    [SerializeField] private float      _maxSpawnDelay;
    [SerializeField] private float      _spawnTimeReducer;
    [SerializeField] private float      _enemySpawnHeight;
    [Space]

    private int                         _spawnCounter;
    private Vector3                     _platformPosition;
    private float                       _platformRadius;
    [SerializeField]private List<Unit>                  _activeEnemyList = new List<Unit>();
    public static EnemySpawner          Instance; // singleton

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }

        _platformPosition = _platform.transform.position;
        _platformRadius = _platform.transform.localScale.x * 0.5f;

        _spawnCounter = 0;
        _maxSpawnDelay = 5f;

        Invoke(nameof(EnemySpawn), _maxSpawnDelay);
    }

    
    private Vector3 GetRandomPointInArea()
    {
        Vector3 randomPoint = Random.insideUnitSphere;
        randomPoint = randomPoint * _platformRadius + _platformPosition;
        randomPoint.y = _platform.transform.position.y + _enemySpawnHeight;

        // this need for stackless spawn
        Collider[] hitColliders = Physics.OverlapBox(randomPoint, Vector3.one * 0.33f, Quaternion.identity); 
        if (hitColliders.Length > 0)
        {
            randomPoint = GetRandomPointInArea();
        }
        return randomPoint;
    }

    private void EnemySpawn()
    {
        Unit clone;

        if (_spawnCounter == 5)
        {
            clone = PoolController.Instance.PoolManager.GetFromPool<BossEnemy>();
            _spawnCounter = 0;
        }
        else
        {
            clone = PoolController.Instance.PoolManager.GetFromPool<KamikazeEnemy>();
            _spawnCounter++;
        }

        clone.transform.position = GetRandomPointInArea();
        clone.OnUnitDeathFromDamage += Player.Instance.GetBounty;
        clone.OnUnitDeathFromDamage += GameManager.Instance.CounterEnemies;
        clone.OnUnitDeath += PoolController.Instance.BackInPool;
        clone.OnUnitDeath += RemoveEnemyFromList;

        _activeEnemyList.Add(clone);


        if (_maxSpawnDelay > _minSpawnDelay)
        {
            _maxSpawnDelay -= _spawnTimeReducer;
        }
        Invoke(nameof(EnemySpawn), _maxSpawnDelay);
    }

    public void KillAllEnemies()
    {
        CancelInvoke(nameof(EnemySpawn));

        List<Unit> enemyListCopy = new List<Unit>(_activeEnemyList);

        foreach (var enemy in enemyListCopy)
        {
            enemy.Death();
        }

        Invoke(nameof(EnemySpawn), _maxSpawnDelay);
    }

    public Unit GetNearestEnemy(Vector3 point)
    {
        float minimumDistance = float.MaxValue;
        Unit nearestEnemy = null;
        _activeEnemyList.ForEach(enemy =>
        {
            float distance = Vector3.Distance(point, enemy.transform.position);

            if (distance < minimumDistance & distance != 0) 
            // 0 is this unit need for exclude return self unit
            {
                nearestEnemy = enemy;
                minimumDistance = distance;
            }
        });
        return nearestEnemy;
    }

    private void RemoveEnemyFromList(Unit enemy)
    {
        if (_activeEnemyList.Contains(enemy))
        {
            _activeEnemyList.Remove(enemy);

            enemy.OnUnitDeathFromDamage -= Player.Instance.GetBounty;
            enemy.OnUnitDeath -= GameManager.Instance.CounterEnemies;
            enemy.OnUnitDeath -= PoolController.Instance.BackInPool;
            enemy.OnUnitDeath -= RemoveEnemyFromList;
        }
    }
}
