using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The parameters of each specific status effect
 * Used on towers to determine what kind of status their projectiles inflict on enemies and for how long
 * Used on enemies to manage their current statuses
 */ 
[Serializable]
public struct ConditionParameters
{
    public Conditions condition;
    public int intensity;
    public float duration;
}
/*
 * Parent script for projectiles
 * Can exhibit different behaviour depending on the collision type
 * Pierce determines how many enemies can the projectile hit before being destroyed
 * Has its own internal stats that it receives from the tower for simpler calculation code
 */
public class ProjectileType : MonoBehaviour
{
    [HideInInspector]public int damage;
    [HideInInspector]public float pierce;
    [HideInInspector]
    public List<ConditionParameters> applyConditions;
    

    public void OnTriggerEnter(Collider col)
    {
        HandleTrigger(col);
    }

    public void HandleTrigger(Collider col)
    {
        GameObject collided = col.gameObject;
        //Debug.Log("Collided with: " + col.gameObject.name);
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
        if (pierce > 0)
        {
            pierce--;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public virtual void GroundContact()
    {
        
    }
    
}
