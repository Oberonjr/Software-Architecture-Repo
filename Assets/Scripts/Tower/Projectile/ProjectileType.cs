using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ConditionParameters
{
    public Conditions condition;
    public int intensity;
    public float duration;
}

public class ProjectileType : MonoBehaviour
{
    [HideInInspector]public int damage;
    [HideInInspector]public float pierce;
    [HideInInspector]public List<ConditionParameters> applyConditions;
    

    public void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Collided with: " + col.gameObject.name);
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
