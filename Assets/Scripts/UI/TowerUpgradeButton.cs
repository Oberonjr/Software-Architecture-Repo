using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Manages the visuals depending on upgrade affordability, and calls the TowerController's Upgrade() on press
//Parented under the UpgradePanel object, so it receives the tower info from UpgradePanelManager
public class TowerUpgradeButton : MonoBehaviour
{
    [SerializeField] TMP_Text costText;
    [HideInInspector]public TowerController towerToUpgrade;

    void Start()
    {
        EventBus<UpdateMoneyBalannceEvent>.OnEvent += UpdateTextColor;
    }

    void OnDestroy()
    {
        EventBus<UpdateMoneyBalannceEvent>.OnEvent -= UpdateTextColor;
    }
    public void UpdateCostText()
    {
        
            costText.text = "Upgrade cost:" + towerToUpgrade.TowerStats.stats.upgradeTower.TowerStats.stats.cost;
            UpdateTextColor();
        
    }

    void UpdateTextColor(UpdateMoneyBalannceEvent e = null)
    {
        if (EconomyManager.Instance.CanAfford(towerToUpgrade.TowerStats.stats.upgradeTower.TowerStats.stats.cost))
        {
            costText.color = Color.black;
        }
        else
        {
            costText.color = Color.red;
        }
    }

    public void UpgradeTower()
    {
        EventBus<DeselectTowerEvent>.Publish(new DeselectTowerEvent());
        towerToUpgrade.UpgradeTower();
    }
}
