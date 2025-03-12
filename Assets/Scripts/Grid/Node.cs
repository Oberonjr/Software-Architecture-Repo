using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3 GridPosition; 
    public GameObject placedObject; //Check if an object is on the same position as the node
    public Dictionary<Node, float> neighbours = new Dictionary<Node, float>(); 

    public Node(Vector3 gridPosition)
    {
        GridPosition = gridPosition;
    }
}