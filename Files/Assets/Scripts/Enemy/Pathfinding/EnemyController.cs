using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private IPathFinding pathFinder;

    [HideInInspector]
    public GameObject target;
    [SerializeField]
    private float speed = 2f;

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
    }

    
    void Update()
    {
        pathFinder.MoveTowardsTarget(target.transform.position, speed);
    }
}
