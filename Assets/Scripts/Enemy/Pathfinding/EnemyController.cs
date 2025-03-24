using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private IPathFinding pathFinder;

    [HideInInspector] 
    public GameObject target;
    [HideInInspector]
    public float maxSpeed;
    
    private float currentSpeed;

    public System.Action<Conditions> ClearSlow;

    void Start()
    {
        pathFinder = GetComponent<IPathFinding>();
        if(pathFinder == null)
        {
            throw new System.Exception("No PathFinder found.");
        }
        if(target == null)
        {
            throw new System.Exception("No Target for the enemy!");
        }
        ResetSpeed();
    }

    private void OnDisable()
    {
        if(pathFinder as EUnityPathfinding) Destroy(pathFinder as EUnityPathfinding);
    }

    public void ChangeSpeed(float modifier, float timer)
    {
        currentSpeed -= modifier;
        float newSpeedTimer = timer;
        if (newSpeedTimer > 0)
        {
            newSpeedTimer -= Time.deltaTime;
        }
        else
        {
            ResetSpeed();
        }
    }

    public void ResetSpeed()
    {
        currentSpeed = maxSpeed;
        ClearSlow?.Invoke(Conditions.SLOW);
    }
    
    void Update()
    {
        pathFinder.MoveTowardsTarget(target.transform.position, Mathf.Max(0, currentSpeed));
    }
}
