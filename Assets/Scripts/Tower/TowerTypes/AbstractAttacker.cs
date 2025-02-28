using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAttacker : ScriptableObject
{
    
    public Stats GetStats(TowerStats towerStats)
    {
        Stats stats = new Stats();
        stats.towerAttackInterval = towerStats.stats.towerAttackInterval;
        stats.towerDamage = towerStats.stats.towerDamage;
        stats.projectileSpeed = towerStats.stats.projectileSpeed;
        return stats;
    }

    public abstract void Attack(Transform pSource, TowerStats tower, List<GameObject> pTargetPosition = null);

}
