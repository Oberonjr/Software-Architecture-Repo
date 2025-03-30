using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//General interface that could be used for different pathfinding systems for the enemies
public interface IPathFinding
{
    void MoveTowardsTarget(Vector3 targetPosition, float speed);
}
