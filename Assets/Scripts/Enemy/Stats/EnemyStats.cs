using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Main script of the enemy, that mostly handles the stats
 * Hosts the status and visuals handlers
 * Communication between the various components are mostly done through events
 * Main functions are TakeDamage and Die
 */
[RequireComponent(typeof(EnemyController), typeof(EnemyDebuffHandler), typeof(EnemyVisualsHandler))]
public class EnemyStats : MonoBehaviour, IDamageable
{
    [SerializeField]private int maxHealth;
    [SerializeField]private int maxSpeed;
    [SerializeField]private int deathReward;
    [SerializeField]private int damage;

    private int _currentHealth;
    private EnemyController _enemyController;
    private EnemyDebuffHandler debuffHandler;
    private EnemyVisualsHandler visualsHandler;
    
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

        debuffHandler = GetComponent<EnemyDebuffHandler>();
        _enemyController.ClearSlow += debuffHandler.ClearConditions;
        visualsHandler = GetComponent<EnemyVisualsHandler>();
        visualsHandler.SetHealth(maxHealth);
    }

    private void Start()
    {
        EventBus<EnemySpawnEvent>.Publish(new EnemySpawnEvent(this));
    }

    private void OnDestroy()
    {
        _enemyController.ClearSlow -= debuffHandler.ClearConditions;
        EventBus<EnemyDeathEvent>.Publish(new EnemyDeathEvent(this));
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage + DamageVulnerability;
        visualsHandler.UpdateHealth(_currentHealth);
        if (_currentHealth <= 0)
        {
            Die();
        }
        else
        {
            visualsHandler.ShowDamage(damage + DamageVulnerability);
        }
        //Debug.Log("Enemy took " + (damage + DamageVulnerability) + " damage, and has " + _currentHealth + " remaining health");
    }

    public void ApplyCondition(ConditionParameters param)
    {
        debuffHandler.ApplyCondition(this, param);
    }
    
    private void Die()
    {
        //Debug.Log("Enemy died");
        _enemyController.enabled = false;
        visualsHandler.ShowGold(deathReward);
        Destroy(gameObject, 0.5f);
    }
}
