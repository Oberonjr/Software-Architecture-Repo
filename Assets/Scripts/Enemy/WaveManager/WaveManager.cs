using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Main script that manages Waves progression and spawning
 * Communicates through events when the wave starts and ends
 * Goes through the list of all present SpawnPoints, and cycles through them when spawning enemies
 * For each EnemyGroup in the Wave list, it spawns the whole group
 * It waits for all the currentEnemies to be emptied before sending the signal that the wave is over
 */
public class WaveManager : MonoBehaviour
{
    private static WaveManager _instance;
    public static WaveManager Instance => _instance;

    public List<Wave> Waves => waves;
    public int CurrentWaveIndex => _currentWaveIndex;
    //Serialized for the sake of testing
    [Range(0, 4)]
    [SerializeField]
    private int _currentWaveIndex = 0;
    [SerializeField]private List<Wave> waves;
    [HideInInspector] public List<EnemyStats> currentEnemies = new List<EnemyStats>();
    private List<EnemySpawner> _spawners = new List<EnemySpawner>();
    private Wave _currentWave;
    private EnemyGroup _currentGroup;
    private int _currentSpawnerIndex = 0;
    private bool _spawningGroup = false;
    

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
        EventBus<EnemyDeathEvent>.OnEvent += RemoveEnemy;
    }

    void OnDestroy()
    {
        EventBus<InitializeEnemySpawnersEvent>.OnEvent -= PopulateSpawners;
        EventBus<EnemySpawnEvent>.OnEvent -= AddEnemy;
        EventBus<StartWaveEvent>.OnEvent -= StartWave;
        EventBus<EnemyDeathEvent>.OnEvent -= RemoveEnemy;
    }

    void Start()
    {
        _currentWave = waves[_currentWaveIndex];
        
    }

    void PopulateSpawners(InitializeEnemySpawnersEvent e)
    {
        _spawners.Add(e.spawner);
    }

    void AddEnemy(EnemySpawnEvent e)
    {
        currentEnemies.Add(e.enemy);
    }

    void RemoveEnemy(EnemyDeathEvent e)
    {
        if (currentEnemies.Contains(e.enemy))
        {
            currentEnemies.Remove(e.enemy);
        }
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
        if (_currentWaveIndex + 1 < waves.Count)
        {
            _currentWaveIndex++;
            _currentWave = waves[_currentWaveIndex];
            GameManager.Instance.StartBuildPhase();
        }
        else
        {
            EventBus<WinEvent>.Publish(new WinEvent());
        }

    }

    IEnumerator SpawnGroup(EnemyGroup group)
    {
        _spawningGroup = true;
        for (int i = 0; i < group.amount; i++)
        {
            _spawners[_currentSpawnerIndex].SpawnEnemy(group.enemyType);
            _currentSpawnerIndex = (_currentSpawnerIndex + 1) % _spawners.Count;
            yield return new WaitForSeconds(group.spacing);
        }
        _spawningGroup = false;
    }
}
