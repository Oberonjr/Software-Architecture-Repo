using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheck : MonoBehaviour
{
    [HideInInspector] public int enemyCount;
    [HideInInspector] public bool enemyInRange;
    [HideInInspector] public GameObject firstEnemy;
    [HideInInspector] public GameObject lastEnemy;
    [HideInInspector] public GameObject closeEnemy;

    private List<GameObject> enemiesInRange;

    private void Start()
    {
        enemiesInRange = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider col)
    {
        GameObject entObj = col.gameObject;
        if (entObj.CompareTag("Enemy"))
        {
            enemyCount++;
            enemiesInRange.Add(entObj);
            UpdateClosestEnemy();
        }
    }
    
    private void OnTriggerExit(Collider col)
    {
        GameObject entObj = col.gameObject;
        if (entObj.CompareTag("Enemy"))
        {
            enemyCount--;
            enemiesInRange.Remove(entObj);
            UpdateClosestEnemy();
        }
    }

    private void FixedUpdate()
    {
        if (enemiesInRange.Count > 0)
        {
            enemyInRange = true;
            for (int i = 0; i < enemiesInRange.Count; i++)
            {
                firstEnemy = enemiesInRange[0];
                //Debug.Log(firstEnemy);
                lastEnemy = enemiesInRange[i];

            }
        }
        else
        {
            enemyInRange = false;
        }
        UpdateClosestEnemy();
    }

    private void UpdateClosestEnemy()
    {
        if (enemiesInRange.Count > 0)
        {
            closeEnemy = null;
            float closestDistance = float.MaxValue;

            foreach (GameObject enemy in enemiesInRange)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closeEnemy = enemy;
                }
            }
        }
        else
        {
            closeEnemy = null;
        }
    }
}
