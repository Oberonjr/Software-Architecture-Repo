using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EUnityPathfinding : MonoBehaviour, IPathFinding
{
    private NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public void MoveTowardsTarget(Vector3 targetPosition, float speed)
    {
        if(agent == null) return;
        agent.speed = speed;
        agent.SetDestination(targetPosition);
    }

    private void OnDestroy()
    {
        Destroy(agent);
    }
}
