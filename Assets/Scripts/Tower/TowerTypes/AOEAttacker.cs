using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AOEAttacker", menuName = "Tower Types/AOE Attacker")]
public class AOEAttacker : AbstractAttacker
{
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
        foreach (EnemyStats e in pTargets)
        {
            e.TakeDamage(tower.stats.towerDamage);
        }
    }
}
