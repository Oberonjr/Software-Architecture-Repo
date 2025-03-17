using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{
    public LayerMask ignoreClickLayer;
    private Camera cam;
    private bool _enableHover;

    void Start()
    {
        cam = Camera.main;
        EventBus<ClickNodeEvent>.OnEvent += CheckClickedNode;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndTowerSelection();
        }

        if (Input.anyKeyDown)
        {
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    Debug.Log(key.ToString());
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _enableHover = !_enableHover;
            Debug.Log("Hover is: " + _enableHover);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            EventBus<SelectTowerToBuildEvent>.Publish(new SelectTowerToBuildEvent());
            Debug.Log("Queued dummy tower for building");
        }

        if (Input.GetMouseButtonDown(0))
        {
            Node clickedNode = ClickedNode();
            if(clickedNode != null) EventBus<ClickNodeEvent>.Publish(new ClickNodeEvent(clickedNode));
        }

        if (_enableHover)
        {
            Node hoverNode = ClickedNode();
            if(hoverNode != null) EventBus<HoverNodeEvent>.Publish(new HoverNodeEvent(hoverNode));
        }
        
    }

    Node ClickedNode()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreClickLayer))
        {
            return GridManager.Instance.GetNode(hit.point);
        }
        Debug.LogWarning("No node found");
        return null;
    }
    
    
    public void EndTowerSelection()
    {
        EventBus<DeselectTowerEvent>.Publish(new DeselectTowerEvent());
    }
    
    //TEMP FOR TESTING
    void CheckClickedNode(ClickNodeEvent e)
    {
        if (e.clickNode.placedObject != null)
        {
            Debug.Log(e.clickNode.placedObject.name);
        }
    }
}
