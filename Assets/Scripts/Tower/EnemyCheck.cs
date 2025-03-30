using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * Script present on a child of the main TowerController
 * Dependent on a trigger SphereColldier
 * Calculates and returns who the first enemy and last enemy to have entered the area are
 * as well as the closest enemy to the tower
 * Updates the TowerController's potential targets
 */
[RequireComponent(typeof(SphereCollider))]
public class EnemyCheck : MonoBehaviour
{
    [HideInInspector] public int enemyCount;
    [HideInInspector] public bool enemyInRange;
    [HideInInspector] public SphereCollider Range;

    public event System.Action<Transform, Transform, Transform, List<EnemyStats>> OnEnemyEnterRange; 
    public event System.Action<Transform> OnEnemyLeaveRange; 
    
    private GameObject firstEnemy;
    private GameObject lastEnemy;
    private GameObject closeEnemy;
    private List<EnemyStats> enemiesInRange;

    private void Awake()
    {
        enemiesInRange = new List<EnemyStats>();
        Range = GetComponent<SphereCollider>();
        Range.isTrigger = true;
        EventBus<EnemyDeathEvent>.OnEvent += UpdateEnemyList;
    }

    private void OnDestroy()
    {
        EventBus<EnemyDeathEvent>.OnEvent -= UpdateEnemyList;
    }

    void UpdateEnemyList(EnemyDeathEvent e)
    {
        if (enemiesInRange.Contains(e.enemy))
        {
            enemiesInRange.Remove(e.enemy);
            OnEnemyLeaveRange?.Invoke(e.enemy.transform);
        }
    }
    
    private void OnTriggerEnter(Collider col)
    {
        GameObject entObj = col.gameObject;
        if (entObj.CompareTag("Enemy"))
        {
            enemyCount++;
            enemiesInRange.Add(entObj.GetComponent<EnemyStats>());
            UpdateClosestEnemy();
            
        }
    }
    
    private void OnTriggerExit(Collider col)
    {
        GameObject entObj = col.gameObject;
        if (entObj.CompareTag("Enemy"))
        {
            enemyCount--;
            enemiesInRange.Remove(entObj.GetComponent<EnemyStats>());
            UpdateClosestEnemy();
            OnEnemyLeaveRange?.Invoke(entObj.transform);
        }
    }

    private void FixedUpdate()
    {
        if (enemiesInRange.Count > 0)
        {
            enemyInRange = true;
            firstEnemy = enemiesInRange[0].transform.gameObject;
            lastEnemy = enemiesInRange.Last().transform.gameObject;
            closeEnemy = UpdateClosestEnemy();
            OnEnemyEnterRange?.Invoke(firstEnemy.transform, lastEnemy.transform, closeEnemy.transform, enemiesInRange);
            
        }
        else
        {
            enemyInRange = false;
        }
    }

    private GameObject UpdateClosestEnemy()
    {
            GameObject closestEnemy = null;
            float closestDistance = float.MaxValue;

            foreach (EnemyStats enemy in enemiesInRange)
            {
                if (enemy == null) continue;
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy.transform.gameObject;
                }
            }
            return closestEnemy;
    }
}
