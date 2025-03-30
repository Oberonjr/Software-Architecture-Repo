using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Container for values heavily used in the main script for logic
//Also present on towers as an extra stat for when they use explosions
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

/*
 * Explosion projectile that starts very small and expands to given size by an increment every frame
 * Used on both single and AOE towers
 * AOE towers use it an as aura originating from their shooting point
 * Projectile towers use it to trigger an explosion at a contact point
 * Visualized through the use of VFX
 * It increases alongside a trigger collider, categorizing all enemies within
 * Saves the enemies already hit by the effect as to not double dip on them
 */
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
