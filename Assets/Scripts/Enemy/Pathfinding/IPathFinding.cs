using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathFinding
{
    void MoveTowardsTarget(Vector3 targetPosition, float speed);
}
