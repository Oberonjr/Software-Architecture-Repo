using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SingleAttacker", menuName = "Tower Types/Single Attacker")]
public class SingleAttacker : AbstractAttacker
{
    [SerializeField]
    protected ProjectileController projectilePrefab;
    
    public override void Attack(Transform pSource, TowerStats stats, List<GameObject> pTargetPositions)
    {
        Stats workingStats = GetStats(stats);
        foreach (GameObject GO in pTargetPositions)
        {
            Vector3 v = GO.transform.position;
            SingleAttack(pSource, workingStats, v);
        }
    }
    
    public void SingleAttack(Transform pSource, Stats stats, Vector3 pTarget)
    {
        Vector3 projectileVelocity = (pTarget - pSource.position).normalized * stats.projectileSpeed;
        ProjectileController projectileController = Instantiate<ProjectileController>(projectilePrefab);
        projectileController.transform.position = pSource.position;
        projectileController.SetVelocity(projectileVelocity);
        if (projectileController.transform.TryGetComponent<ProjectileType>(out ProjectileType projectileType))
        {
            projectileType.damage = stats.towerDamage;   
        }
        else
        {
            Debug.LogWarning("ProjectileType component not found");
        }
    }
}
