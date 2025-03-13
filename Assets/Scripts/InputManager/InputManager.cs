using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{
    public LayerMask ignoreClickLayer;
    private Camera cam;

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

        if (Input.GetMouseButtonDown(0))
        {
            Node clickedNode = ClickedNode();
            if(clickedNode != null) EventBus<ClickNodeEvent>.Publish(new ClickNodeEvent(clickedNode));
        }
    }

    Node ClickedNode()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreClickLayer))
        {
            GameObject clickVisualizer = Instantiate(GridManager.Instance._gridVisualizerPrefab,hit.point, Quaternion.identity);
            GameObject NodeClickVisualizer = Instantiate(GridManager.Instance._gridVisualizerPrefab, GridManager.Instance.GetNode(hit.point).GridPosition, Quaternion.identity);
            clickVisualizer.transform.localScale = new Vector3(4, 4, 4);
            Debug.Log(clickVisualizer.transform.position);
            Destroy(clickVisualizer, 2f);
            NodeClickVisualizer.transform.localScale = new Vector3(4, 4, 4);
            Debug.Log(NodeClickVisualizer.transform.position);
            Destroy(NodeClickVisualizer, 2f);
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
//        
    }
}
