using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simple projectile that damages enemy on collision
public class SimpleProjectile : ProjectileType
{
    public override void EnemyContact(GameObject enemy)
    {
        base.EnemyContact(enemy);
        if (enemy.TryGetComponent(out EnemyStats targetStats))
        {
            targetStats.TakeDamage(damage);
            foreach (ConditionParameters condition in applyConditions)
            {
                targetStats.ApplyCondition(condition);
            }
        }
        else
        {
            Debug.LogWarning("No stats found on target. Fix your damn code");
        }
    }
}
