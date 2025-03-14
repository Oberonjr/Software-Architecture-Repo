using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SingleAttacker", menuName = "Tower Types/Single Attacker")]
public class SingleAttacker : AbstractAttacker
{
    private ProjectileController projectilePrefab;
    
    public override void Attack(Transform pSource, TowerStats stats, List<GameObject> pTargetPositions)
    {
        Stats workingStats = stats.stats;
        if (workingStats.projectilePrefab.TryGetComponent(out ProjectileController prefabController))
        {
            projectilePrefab = prefabController;
        }
        else
        {
            Debug.LogWarning("ProjectileController not found");
        }
        foreach (GameObject GO in pTargetPositions)
        {
            Vector3 v = GO.transform.position;
            SingleAttack(pSource, workingStats, v);
        }
    }
    
    public void SingleAttack(Transform pSource, Stats stats, Vector3 pTarget)
    {
        Vector3 direction = (pTarget - pSource.position);
        Vector3 projectileVelocity = direction.normalized * stats.projectileSpeed;
        EventBus<PlayAttackAnimationEvent>.Publish(new PlayAttackAnimationEvent(direction));
        ProjectileController projectileController = Instantiate<ProjectileController>(projectilePrefab);
        projectileController.transform.position = pSource.position;
        projectileController.SetVelocity(projectileVelocity);
        projectileController.transform.GetChild(0).LookAt(pTarget);
        Debug.DrawRay(pSource.position, direction, Color.red, 2f);
        if (projectileController.transform.TryGetComponent<ProjectileType>(out ProjectileType projectileType))
        {
            projectileType.damage = stats.towerDamage;
            projectileType.pierce = stats.projectilePierce;
            if (projectileType is GroundProjectile)
            {
                GroundProjectile groundProjectile = projectileType as GroundProjectile;
                groundProjectile.Explosion.SetParameters(stats.towerDamage, stats.projectileSpeed, stats.expandingAOEParams.maxAOERadius, stats.expandingAOEParams.expansionRadiusIncrement);
                groundProjectile.Explosion.applyConditions = stats.projectileConditions;
            }
            else
            {
                projectileType.applyConditions = stats.projectileConditions;
            }
            
        }
        else
        {
            Debug.LogWarning("ProjectileType component not found");
        }
    }
}
