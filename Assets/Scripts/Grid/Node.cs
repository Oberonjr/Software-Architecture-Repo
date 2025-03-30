using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Container class for where towers can be built, made in a very simple way
public class Node
{
    public Vector3 GridPosition; 
    public GameObject placedObject; //Check if an object is on the same position as the node

    public Node(Vector3 gridPosition)
    {
        GridPosition = gridPosition;
    }
}