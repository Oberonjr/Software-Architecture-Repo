using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ExpandingAOEParameters
{
    [HideInInspector]
    public int AOEDamage;
    [HideInInspector]
    public float expansionSpeed;
    public float maxAOERadius;
    public float expansionRadiusIncrement;
}


[RequireComponent(typeof(SphereCollider))]
public class ExpandingAOE : ProjectileType
{
    [HideInInspector]
    public ExpandingAOEParameters _parameters;

    [SerializeField] private GameObject particleEffect;
    private GameObject _effect;

    private SphereCollider _collider;

    private List<EnemyStats> _alreadyDamaged = new List<EnemyStats>();
    private void Start()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.isTrigger = true;
        _collider.radius = Mathf.Epsilon;
        
        InvokeRepeating(nameof(Expand), Mathf.Epsilon, 1/_parameters.expansionSpeed);
        _effect = Instantiate(particleEffect, this.transform);
    }

    public void SetParameters(int damage, float expansionSpeed, float maxAOERadius, float expansionRadiusIncrement)
    {
        _parameters.AOEDamage = damage;
        _parameters.expansionSpeed = expansionSpeed;
        _parameters.maxAOERadius = maxAOERadius;
        _parameters.expansionRadiusIncrement = expansionRadiusIncrement;
    }

    public override void EnemyContact(GameObject enemy)
    {
        EnemyStats stats = enemy.GetComponent<EnemyStats>();
        if (_alreadyDamaged.Contains(stats)) return;
        stats.TakeDamage(_parameters.AOEDamage);
        foreach (ConditionParameters param in applyConditions)
        {
            stats.ApplyCondition(param);
        }
        _alreadyDamaged.Add(stats);
    }
    
    void Expand()
    {
        if (_collider.radius < _parameters.maxAOERadius)
        {
            _collider.radius += _parameters.expansionRadiusIncrement;
            _effect.transform.localScale = _collider.radius * Vector3.one;
        }
        else
        {
            Destroy(this.gameObject, 0.5f);
        }
    }
}
