using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Extra script used for simulating an OnTriggerEnter on the projectile parent
 * Rotating the parent for proper visuals messed up it's velocity
 * So I rotate the child VFX instead
 * Put a trigger collider on the child to have the most precise hitbox collision
 */
public class ChildTrigger : MonoBehaviour
{
    ProjectileType _projectileType;
    void Start()
    {
        _projectileType = GetComponentInParent<ProjectileType>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(_projectileType != null)
            _projectileType.HandleTrigger(other);
    }
}
