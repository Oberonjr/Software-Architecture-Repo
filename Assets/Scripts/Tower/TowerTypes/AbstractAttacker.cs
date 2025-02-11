using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAttacker : MonoBehaviour
{
    //protected Transform targetTransform;

    [SerializeField]
    protected float projectileSpeed = 2f;
    
    [SerializeField]
    protected int projectileDamage = 1;

    [SerializeField]
    protected ProjectileController projectilePrefab;

    public void Start()
    {
        projectilePrefab.gameObject.GetComponent<ProjectileType>().damage = projectileDamage;
    }
    
    public abstract void Attack(Transform pSource, Transform pTarget);
}
