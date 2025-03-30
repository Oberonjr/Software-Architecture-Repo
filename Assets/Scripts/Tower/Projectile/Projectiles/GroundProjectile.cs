using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Projectile that has an effect when hitting the ground as well as an enemy
 * Mostly used with an ExpandingAOE for an explosive projectile tower
 */
public class GroundProjectile : ProjectileType
{
    [SerializeField] private ExpandingAOE _explosion;
    
    public ExpandingAOE Explosion{get {return _explosion;} }
    
    public override void GroundContact()
    {
        Instantiate<ExpandingAOE>(_explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    public override void EnemyContact(GameObject enemy)
    {
        GroundContact();
    }
}
