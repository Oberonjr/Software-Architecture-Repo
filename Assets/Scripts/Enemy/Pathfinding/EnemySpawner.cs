using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField]private float spawnInterval = 3f;  // Time between spawns in seconds
    [SerializeField] private GameObject enemyTarget;
    private float timer = 0f;
    private int index = 0;
    
    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Check if it's time to spawn a new enemy
        if (timer >= spawnInterval)
        {
            // Spawn an enemy
            SpawnEnemy();

            // Reset the timer
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        // Instantiate the enemy prefab at the spawner's position
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        index++;
        newEnemy.name = "Enemy" + index;
        
        // Optionally, you can set additional properties of the spawned enemy here
        EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
        enemyController.target = enemyTarget;
    }
}
