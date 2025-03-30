using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main container class for all tower stats
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

//Class for the Tower's stats, implemented this way due to initial plans of having more functions here
[Serializable]
public class TowerStats
{
    public Stats stats;
    
}
