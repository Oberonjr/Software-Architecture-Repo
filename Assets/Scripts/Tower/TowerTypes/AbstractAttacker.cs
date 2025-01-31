using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAttacker : MonoBehaviour
{
    //protected Transform targetTransform;

    [SerializeField]
    protected float projectileSpeed = 2f;

    [SerializeField]
    protected ProjectileController projectilePrefab;

    // public void SetTarget(Transform pTargetTransform)
    // {
    //     targetTransform = pTargetTransform;
    // }

    public abstract void Attack(Transform pSource, Transform pTarget);
}
