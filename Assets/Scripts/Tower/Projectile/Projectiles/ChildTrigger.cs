using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildTrigger : MonoBehaviour
{
    ProjectileType _projectileType;
    // Start is called before the first frame update
    void Start()
    {
        _projectileType = GetComponentInParent<ProjectileType>();
    }

    //HALLOOOO PAPA YAHHH I WANT DA LOLLIPOP
    void OnTriggerEnter(Collider other)
    {
        if(_projectileType != null)
            _projectileType.HandleTrigger(other);
    }
}
