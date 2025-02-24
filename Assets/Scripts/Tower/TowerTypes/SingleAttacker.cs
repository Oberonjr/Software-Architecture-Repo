using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttacker : AbstractAttacker
{
    public override void Attack(Transform pSource, Vector3 pTarget)
    {
        Vector3 projectileVelocity = (pTarget - pSource.position).normalized * projectileSpeed;
        ProjectileController projectileController = Instantiate<ProjectileController>(projectilePrefab);
        projectileController.transform.position = pSource.position;
        projectileController.SetVelocity(projectileVelocity);
    }
}
