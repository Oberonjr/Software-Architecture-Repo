using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    // [SerializeField] GameObject enemyPrefab;
    // [SerializeField]private float spawnInterval = 3f;  // Time between spawns in seconds
    [SerializeField] private GameObject enemyTarget;
    private float timer = 0f;
    private int index = 0;

    void Start()
    {
        EventBus<InitializeEnemySpawnersEvent>.Publish(new InitializeEnemySpawnersEvent(this));
    }
    
    void Update()
    {
        // // Update the timer
        // timer += Time.deltaTime;
        //
        // // Check if it's time to spawn a new enemy
        // if (timer >= spawnInterval)
        // {
        //     // Spawn an enemy
        //     SpawnEnemy();
        //
        //     // Reset the timer
        //     timer = 0f;
        // }
    }

    public void SpawnEnemy(EnemyStats enemy)
    {
        
        EnemyStats newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
        index++;
        newEnemy.name = "Enemy" + index;
        
        EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
        enemyController.target = enemyTarget;
    }
}
