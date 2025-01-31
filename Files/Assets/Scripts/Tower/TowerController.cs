using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private int TargetingType = 1;
    [SerializeField] private float attackInterval = 1f;
    
    
    [SerializeField] private AbstractAttacker attacker;
    [SerializeField] private EnemyCheck _enemyCheck;
    [SerializeField] private Transform shootingPoint;
    
    
    
    private const int FIRST = 1;
    private const int LAST = 2;
    private const int CLOSE = 3;

    private Transform firstEnemy;
    private Transform lastEnemy;
    private Transform closeEnemy;

    private Transform target;
    

    void Start()
    {
        
        InvokeRepeating(nameof(AttackRepeating), 0f, attackInterval);
        
    }

    private void Update()
    {
        if (_enemyCheck.enemyInRange)
        {
            UpdateEnemy();
            SelectTarget(TargetingType);
        }
    }

    private void SelectTarget(int targetMode)
    {
        switch (targetMode)
        {
            case FIRST:
                target=(firstEnemy);
                break;
            case LAST:
                target=(lastEnemy);
                break;
            case CLOSE:
                target=(closeEnemy);
                break;
            default:
                target=(firstEnemy);
                break;
        }
    }

    private void UpdateEnemy()
    {
        firstEnemy = _enemyCheck.firstEnemy.transform;
        lastEnemy = _enemyCheck.lastEnemy.transform;
        closeEnemy = _enemyCheck.closeEnemy.transform;
    }
    
    private void AttackRepeating()
    {
        if (target != null)
        {
            attacker.Attack(transform, target);
        }
    }
}
