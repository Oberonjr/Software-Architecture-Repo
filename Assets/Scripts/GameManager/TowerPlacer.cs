using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    //Made public for testing convenience, consider changing
    public TowerPreviewController towerPrefab;
    
    private TowerPreviewController _currentSelectedTower = null;
    private TowerPreviewController _previewTower = null;
    
    private static TowerPlacer _instance;
    public static TowerPlacer Instance => _instance;
    
    [HideInInspector]
    public bool _holdingTower = false;

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
        EventBus<HoverNodeEvent>.OnEvent += VisualizeTowerLocation;
        EventBus<SelectTowerToBuildEvent>.OnEvent += QueueTowerToBuild;
    }

    private void OnDestroy()
    {
        EventBus<HoverNodeEvent>.OnEvent -= VisualizeTowerLocation;
        EventBus<SelectTowerToBuildEvent>.OnEvent -= QueueTowerToBuild;
    }

    void QueueTowerToBuild(SelectTowerToBuildEvent e)
    {
        if(_previewTower != null)Destroy(_previewTower);
        _currentSelectedTower = towerPrefab;
        _previewTower = Instantiate(_currentSelectedTower, Vector3.zero, Quaternion.identity);
        _holdingTower = true;
        EventBus<ClickNodeEvent>.OnEvent += BuildTower;
    }

    void BuildTower(ClickNodeEvent e)
    {
        if(!_holdingTower || e.clickNode.placedObject != null) return;
        EventBus<BuildTowerEvent>.Publish(new BuildTowerEvent(e.clickNode.GridPosition));
        _currentSelectedTower = null;
        _previewTower = null;
        _holdingTower = false;
        EventBus<ClickNodeEvent>.OnEvent -= BuildTower;
    }

    void VisualizeTowerLocation(HoverNodeEvent e)
    {
        if (_previewTower != null)
        {
            if (e.hoverNode != null)
            {
                _previewTower.enabled = true;
                _previewTower.transform.position = e.hoverNode.GridPosition;
            }
            else
            {
                _previewTower.enabled = false;
            }
            
        }
    }
}
