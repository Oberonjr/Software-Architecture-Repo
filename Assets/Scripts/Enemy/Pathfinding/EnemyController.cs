using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private IPathFinding pathFinder;

    [HideInInspector]
    public GameObject target;
    [SerializeField]
    private float maxSpeed = 2f;

    private float currentSpeed;
    
    // Start is called before the first frame update
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

    public void ChangeSpeed(int modifier, float timer)
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
    }
    
    void Update()
    {
        pathFinder.MoveTowardsTarget(target.transform.position, Mathf.Max(0, currentSpeed));
    }
}
