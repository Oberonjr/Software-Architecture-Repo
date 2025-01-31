using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    [SerializeField]private float spawnInterval = 3f;  // Time between spawns in seconds
    private float timer = 0f;

    [SerializeField] private GameObject enemyTarget;
    
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

        
        // Optionally, you can set additional properties of the spawned enemy here
        EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
        enemyController.target = enemyTarget;
    }
}
