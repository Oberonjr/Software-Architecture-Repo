using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileType : MonoBehaviour
{
    [HideInInspector]public int damage;

    public void OnTriggerEnter(Collider col)
    {
        Debug.Log("Collided");
        GameObject collided = col.gameObject;
        if (collided.CompareTag("Enemy"))
        {
            EnemyContact(collided);
        }
        else if (collided.CompareTag("Path"))
        {
            GroundContact();
        }
    }
    
    
    public virtual void EnemyContact(GameObject enemy)
    {
       
    }

    public virtual void GroundContact()
    {
        
    }
    
}
