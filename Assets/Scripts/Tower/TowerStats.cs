using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Stats
{
    public int towerDamage;
    public float towerRange;
    public float towerAttackInterval;
    public float projectileSpeed;
}

[Serializable]
public class TowerStats
{
    public Stats stats;

   
    public void UpdateTowerStats(Stats newStats)
    {
       
    }
}
