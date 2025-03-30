using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script that plays the attack animation if the tower has one
public class TowerAnimationHandler : MonoBehaviour
{
    [SerializeField] private Transform ToRotate;
    
    private Animator _animator = null;

    void Start()
    {
        if (TryGetComponent(out Animator a))
        {
            _animator = a;
        }
    }

    public void RotateToTarget(Vector3 target)
    {
        if (ToRotate != null)
        {
            ToRotate.LookAt(target);
            if (_animator != null)
            {
                _animator.SetTrigger("Attack");
            }
        }
    }
}
