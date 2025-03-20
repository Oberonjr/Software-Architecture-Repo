using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public class TowerPlacer : MonoBehaviour
{
    [SerializedDictionary("Key","Tower")]
    public SerializedDictionary<KeyCode, TowerPreviewController> towersKeyMapping;
    
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
        if(_previewTower != null)Destroy(_previewTower.gameObject);
        if (!towersKeyMapping.ContainsKey(e.key) ||
            !EconomyManager.Instance.CanAfford(towersKeyMapping[e.key].TowerToSpawn.TowerStats.stats.cost))
        {
            EventBus<ToggleHoverEvent>.Publish(new ToggleHoverEvent(false));
            return;
        }
        _holdingTower = true;
        EventBus<ToggleHoverEvent>.Publish(new ToggleHoverEvent(_holdingTower));
        _currentSelectedTower = towersKeyMapping[e.key];
        _previewTower = Instantiate(_currentSelectedTower, Vector3.zero, Quaternion.identity);
        EventBus<ClickNodeEvent>.OnEvent += BuildTower;
    }

    void BuildTower(ClickNodeEvent e)
    {
        if(!_holdingTower || e.clickNode.placedObject != null) return;
        _holdingTower = false;
        EventBus<ToggleHoverEvent>.Publish(new ToggleHoverEvent(_holdingTower));
        EventBus<BuildTowerEvent>.Publish(new BuildTowerEvent(e.clickNode.GridPosition));
        _currentSelectedTower = null;
        _previewTower = null;
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
