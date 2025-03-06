using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
