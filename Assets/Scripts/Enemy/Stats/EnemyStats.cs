using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController), typeof(EnemyDebuffManager))]
public class EnemyStats : MonoBehaviour, IDamageable
{
    [SerializeField]private int maxHealth;
    [SerializeField]private int maxSpeed;
    [SerializeField]private int deathReward;
    [SerializeField]private int damage;

    private int _currentHealth;
    private EnemyController _enemyController;
    private EnemyDebuffManager _debuffManager;
    
    private System.Action<Conditions> ClearDOT;
    
    [HideInInspector]public int DamageVulnerability = 0;
    public EnemyController EnemyController => _enemyController;
    public int DeathReward => deathReward;
    public int Damage => damage;
    
    void Awake()
    {
        
        _currentHealth = maxHealth;
        if (TryGetComponent(out EnemyController ec))
        {
            _enemyController = ec;
            _enemyController.maxSpeed = maxSpeed;
        }
        else
        {
            Debug.LogError("No enemy controller attached. Which is a wonder, since it's a required component for this script. Fix your damn code jesus christ man.");
        }

        _debuffManager = GetComponent<EnemyDebuffManager>();
        _enemyController.ClearSlow += _debuffManager.ClearConditions;
    }

    private void OnDestroy()
    {
        _enemyController.ClearSlow -= _debuffManager.ClearConditions;
        EventBus<EnemyDeathEvent>.Publish(new EnemyDeathEvent(this));
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage + DamageVulnerability;
        if (_currentHealth <= 0)
        {
            Die();
        }
        Debug.Log("Enemy took " + (damage + DamageVulnerability) + " damage, and has " + _currentHealth + " remaining health");
    }

    public void ApplyCondition(ConditionParameters param)
    {
        _debuffManager.ApplyCondition(this, param);
    }
    
    private void Die()
    {
        Debug.Log("Enemy died");
        
        Destroy(gameObject, 0.5f);
    }
}
