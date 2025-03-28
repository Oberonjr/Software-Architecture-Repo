using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Stats
{
    public int cost;
    public TowerController upgradeTower;
    
    public int maxIndividualTargets;
    public int towerDamage;
    public float towerRange;
    public float towerAttackInterval;
    public float projectileSpeed;
    public int projectilePierce;
    public List<ConditionParameters> projectileConditions;
    public ExpandingAOEParameters expandingAOEParams;
    public GameObject projectilePrefab;
}

[Serializable]
public class TowerStats
{
    public Stats stats;
    
}
