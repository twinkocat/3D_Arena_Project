using Redcode.Pools;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    private PoolManager             _poolManager;
    public static PoolController    Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        _poolManager = GetComponent<PoolManager>();
    }


    public void BackInPool<T>(T unit) where T : Unit
    {
        if (unit.TryGetComponent<KamikazeEnemy>(out KamikazeEnemy kamikazeEnemy))
        {
            PoolManager.TakeToPool<KamikazeEnemy>(kamikazeEnemy);
        } 
        else if (unit.TryGetComponent<BossEnemy>(out BossEnemy bossEnemy))
        {
            PoolManager.TakeToPool<BossEnemy>(bossEnemy);
        }
    }

    public PoolManager PoolManager
    { 
        get { return _poolManager; } 
        private set { _poolManager = value; }
    }
}
