using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{
    
    [SerializeField]private LayerMask ignoreClickLayer;
    private Camera cam;
    private bool _enableHover;
    private bool _canBuild = true;

    void Start()
    {
        cam = Camera.main;
        EventBus<ClickNodeEvent>.OnEvent += CheckClickedNode;
        EventBus<ToggleHoverEvent>.OnEvent += ToggleHover;
        EventBus<StartBuildPhaseEvent>.OnEvent += EnableBuild;
        EventBus<StartWaveEvent>.OnEvent += DisableBuild;
    }

    private void OnDestroy()
    {
        EventBus<ClickNodeEvent>.OnEvent -= CheckClickedNode;
        EventBus<ToggleHoverEvent>.OnEvent -= ToggleHover;
        EventBus<StartBuildPhaseEvent>.OnEvent -= EnableBuild;
        EventBus<StartWaveEvent>.OnEvent -= DisableBuild;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndTowerSelection();
        }

        if (_canBuild)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(key))
                    {
                        EventBus<SelectTowerToBuildEvent>.Publish(new SelectTowerToBuildEvent(key));
                    }
                }
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
        
        
    }

    void EnableBuild(StartBuildPhaseEvent e)
    {
        _canBuild = true;
    }

    void DisableBuild(StartWaveEvent e)
    {
        _canBuild = false;
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

    public void ToggleHover(ToggleHoverEvent e)
    {
        _enableHover = e.hoverValue;
    }

    public void SelectTowerButton(KeyCode key)
    {
        EventBus<SelectTowerToBuildEvent>.Publish(new SelectTowerToBuildEvent(key));
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
