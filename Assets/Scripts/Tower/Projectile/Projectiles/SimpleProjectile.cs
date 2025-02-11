using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : ProjectileType
{
    public override void EnemyContact(GameObject enemy)
    {
        EnemyStats targetStats = enemy.GetComponent<EnemyStats>();
        targetStats.TakeDamage(damage);
    }
}
