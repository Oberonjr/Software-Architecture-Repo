using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildTrigger : MonoBehaviour
{
    ProjectileType projectileType;
    // Start is called before the first frame update
    void Start()
    {
        projectileType = GetComponentInParent<ProjectileType>();
    }

    void OnTriggerEnter(Collider other)
    {
        projectileType.HandleTrigger(other);
    }
}
