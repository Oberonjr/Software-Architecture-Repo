using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowProjectile : ProjectileType
{
    public override void EnemyContact(GameObject enemy)
    {
        if (enemy.TryGetComponent(out EnemyController controller))
        {
            controller.ChangeSpeed(damage, 10f);
        }
        else
        {
            Debug.LogError("Target is not an enemy with a movement controller. Fix your damn code");
        }
        
    }
}
