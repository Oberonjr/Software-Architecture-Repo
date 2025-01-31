using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileType : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        GameObject collided = col.gameObject;
        if (collided.CompareTag("Enemy"))
        {
            EnemyContact();
        }
        else if (collided.CompareTag("Ground"))
        {
            GroundContact();
        }
    }

    public virtual void EnemyContact()
    {
        
    }

    public virtual void GroundContact()
    {
        
    }
    
}
