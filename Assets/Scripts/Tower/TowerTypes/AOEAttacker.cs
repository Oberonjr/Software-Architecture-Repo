using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AOEAttacker", menuName = "Tower Types/AOE Attacker")]
public class AOEAttacker : AbstractAttacker
{
    private ExpandingAOE AOEeffect;
    
    public override void Attack(Transform pSource, TowerStats tower, List<GameObject> pTargetPositions)
    {
        List<EnemyStats> enemies = new List<EnemyStats>();
        foreach (GameObject enemy in pTargetPositions)
        {
            if (enemy.TryGetComponent(out EnemyStats stats))
            {
                enemies.Add(stats);
            }
            else
            {
                Debug.LogError("A non-enemy has been targeted. Fix your damn code");
            }
        }
        AOEAttack(pSource, tower, enemies);
    }

    public void AOEAttack(Transform pSource, TowerStats tower, List<EnemyStats> pTargets)
    {
        Stats stats = tower.stats;
        if (stats.projectilePrefab.TryGetComponent(out ExpandingAOE effect))
        {
            AOEeffect = effect;
            effect.SetParameters(stats.towerDamage, 
                stats.projectileSpeed, stats.towerRange, stats.expandingAOEParams.expansionRadiusIncrement);
        }
        Instantiate<ExpandingAOE>(effect, pSource);
    }
}
