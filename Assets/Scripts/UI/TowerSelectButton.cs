using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Logic for the side panel buttons to queue up building the selected tower
 * Also manages the cost text and button being interactable based on affordability
 */
public class TowerSelectButton : MonoBehaviour
{
    [SerializeField] private KeyCode towerSelectKey;
    [SerializeField] private TMP_Text towerCostText;

    private TowerStats towerStats;
    
    //"Why do you have a whole script just for the functionality of this button set up in such an unintuitive way?"
    //I'm tired, boss

    void Start()
    {
        //I did the tower selection logic through the keybinds first. I then realized I needed the buttons for visuals
        //I made the system liste to the events reading a keybind, and that's why the following line of code is SO awkward
        //I should've made an overload for the event that just takes a towerController. Would've been simple, too.
        try
        {
            towerStats = TowerPlacerManager.Instance.towersKeyMapping[towerSelectKey].TowerToSpawn.TowerStats;
        }
        catch (Exception e)
        {
            Debug.Log(e);
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
