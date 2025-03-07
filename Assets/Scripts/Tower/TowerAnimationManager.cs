using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAnimationManager : MonoBehaviour
{
    [SerializeField] private Transform ToRotate;
    
    private Animator animator = null;

    void Start()
    {
        if (TryGetComponent(out Animator a))
        {
            animator = a;
        }
    }

    public void RotateToTarget(Vector3 target)
    {
        if (ToRotate != null)
        {
            ToRotate.LookAt(target);
            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }
        }
    }
}
