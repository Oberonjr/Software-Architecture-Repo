using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject enemyTarget;
    private int index = 0;
    
    #if UNITY_EDITOR
    [SerializeField] private EnemyStats dummyEnemy;
    #endif

    void Start()
    {
        EventBus<InitializeEnemySpawnersEvent>.Publish(new InitializeEnemySpawnersEvent(this));
    }

    public void SpawnEnemy(EnemyStats enemy)
    {
        
        EnemyStats newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
        index++;
        newEnemy.name = "Enemy" + index;
        
        EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
        enemyController.target = enemyTarget;
    }

    #if UNITY_EDITOR
    void DummySpawnEnemy()
    {
        SpawnEnemy(dummyEnemy);
    }
    
    [Button("Manually start spawning", EButtonEnableMode.Playmode)]
    public void ManuallyStartSpawning()
    {
        if (dummyEnemy != null)
        {
            InvokeRepeating(nameof(DummySpawnEnemy), 0f, 1.5f);
        }
        else
        {
            Debug.Log("No dummy enemy provided");   
        }
    }
    #endif
}
