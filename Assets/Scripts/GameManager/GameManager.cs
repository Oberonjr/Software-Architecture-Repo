using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    public static GameManager Instance{get { return _instance; }}
    
    [HideInInspector] public bool isSpedUp;

    private TowerController currentSelectedTower;

    private void Awake()
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

    private void Start()
    {
        EventBus<SelectTowerEvent>.OnEvent += UpdateCurrentTower;
        EventBus<DeselectTowerEvent>.OnEvent += DeselectTower;
    }

    private void OnDestroy()
    {
        EventBus<SelectTowerEvent>.OnEvent -= UpdateCurrentTower;
        EventBus<DeselectTowerEvent>.OnEvent -= DeselectTower;
    }

    void UpdateCurrentTower(SelectTowerEvent e)
    {
        if (currentSelectedTower != null)
        {
            currentSelectedTower.ToggleRangeIndicator();
        }
        currentSelectedTower = e.tower;
        currentSelectedTower.ToggleRangeIndicator();
    }

    void DeselectTower(DeselectTowerEvent e)
    {
        if (currentSelectedTower != null)
        {
            currentSelectedTower.ToggleRangeIndicator();
        }
        currentSelectedTower = null;
    }

    public void ToggleGameSpeed()
    {
        isSpedUp = !isSpedUp;
        Time.timeScale = isSpedUp ? 3 : 1;
    }
    

    
}
