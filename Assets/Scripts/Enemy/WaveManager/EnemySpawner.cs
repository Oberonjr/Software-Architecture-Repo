using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject enemyTarget;
    private int index = 0;

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
}
