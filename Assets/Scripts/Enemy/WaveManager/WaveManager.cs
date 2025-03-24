using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private static WaveManager _instance;
    public static WaveManager Instance => _instance;
    
    //Serialized for the sake of testing
    [SerializeField]
    private int _currentWaveIndex = 0;
    [SerializeField]private List<Wave> waves;
    [HideInInspector] public int currentWaveIndex;
    [HideInInspector] public List<EnemyStats> currentEnemies = new List<EnemyStats>();
    private List<EnemySpawner> spawners = new List<EnemySpawner>();
    private Wave _currentWave;
    private EnemyGroup _currentGroup;
    private int _currentGroupIndex = 0;
    private int _currentSpawnerIndex = 0;
    private bool _spawningGroup = false;
    
    private event System.Action OnGroupEnd;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        EventBus<InitializeEnemySpawnersEvent>.OnEvent += PopulateSpawners;
        EventBus<EnemySpawnEvent>.OnEvent += AddEnemy;
        EventBus<StartWaveEvent>.OnEvent += StartWave;
    }

    void OnDestroy()
    {
        EventBus<InitializeEnemySpawnersEvent>.OnEvent -= PopulateSpawners;
        EventBus<EnemySpawnEvent>.OnEvent -= AddEnemy;
        EventBus<StartWaveEvent>.OnEvent -= StartWave;
    }

    void Start()
    {
        _currentWave = waves[_currentWaveIndex];
        
    }

    void PopulateSpawners(InitializeEnemySpawnersEvent e)
    {
        spawners.Add(e.spawner);
    }

    void AddEnemy(EnemySpawnEvent e)
    {
        currentEnemies.Add(e.enemy);
    }

    void StartWave(StartWaveEvent e)
    {
        StartCoroutine(SpawnWave(_currentWave));
    }
    
    IEnumerator SpawnWave(Wave wave)
    {
        foreach (EnemyGroup group in wave.waveGroups)
        {
            StartCoroutine(SpawnGroup(group));
            yield return new WaitUntil(() => !_spawningGroup);
        }

        yield return new WaitUntil(() => currentEnemies.Count == 0);
        _currentWaveIndex++;
        _currentWave = waves[_currentWaveIndex];
        EventBus<EndWaveEvent>.Publish(new EndWaveEvent(_currentWaveIndex));

    }

    IEnumerator SpawnGroup(EnemyGroup group)
    {
        _spawningGroup = true;
        for (int i = 0; i < group.amount; i++)
        {
            spawners[_currentSpawnerIndex].SpawnEnemy(group.enemyType);
            _currentSpawnerIndex = (_currentSpawnerIndex + 1) % spawners.Count;
            yield return new WaitForSeconds(group.spacing);
        }
        _spawningGroup = false;
        _currentGroupIndex++;
    }
}
