using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerSelectButton : MonoBehaviour
{
    [SerializeField] private KeyCode towerSelectKey;
    [SerializeField] private TMP_Text towerCostText;

    private TowerStats towerStats;
    
    //"Why do you have a whole script just for the functionality of this button set up in such an unintuitive way?"
    //I'm tired, boss

    void Start()
    {
        try
        {
            towerStats = TowerPlacer.Instance.towersKeyMapping[towerSelectKey].TowerToSpawn.TowerStats;
        }
        catch (Exception e)
        {
            Debug.LogError("Couldn't find the key passed on button: " + this.gameObject.name + " in the dictionary");
            return;
        }
        towerCostText.text = towerStats.stats.cost.ToString();
        CanAffordTower();
        EventBus<UpdateMoneyBalannceEvent>.OnEvent += UpdateColors;
    }

    void OnDestroy()
    {
        EventBus<UpdateMoneyBalannceEvent>.OnEvent -= UpdateColors;
    }
    
    public void QueueTower()
    {
        if(InputManager.Instance.canBuild)
            EventBus<SelectTowerToBuildEvent>.Publish(new SelectTowerToBuildEvent(towerSelectKey));
    }

    void CanAffordTower()
    {
        if (towerStats == null) return;
        if (EconomyManager.Instance.CanAfford(towerStats.stats.cost))
        {
            towerCostText.color = Color.white;
        }
        else
        {
            towerCostText.color = Color.red;
        }
    }

    public void UpdateColors(UpdateMoneyBalannceEvent e)
    {
        CanAffordTower();
    }
}
