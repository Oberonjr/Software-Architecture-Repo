using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance => _instance;
    
    
    [SerializeField]private LayerMask ignoreClickLayer;
    private Camera cam;
    private bool _enableHover;
    [HideInInspector]public bool canBuild = true;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    
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

        if (canBuild && GridManager.Instance != null)
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
        canBuild = true;
    }

    void DisableBuild(StartWaveEvent e)
    {
        canBuild = false;
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
