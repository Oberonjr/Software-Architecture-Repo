using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject upgradePanel;
    [SerializeField] TMP_Text towerNameText;
    [SerializeField] TMP_Text statsText;
    [SerializeField] Button upgradeButton;
    [SerializeField] TMP_Text buttonText;
    [SerializeField] TMP_Text upgradeStatsText;

    private bool _isAffordable;

    private void Start()
    {
        EventBus<SelectTowerEvent>.OnEvent += EnableUpgradePanel;
    }

    private void OnDestroy()
    {
        EventBus<SelectTowerEvent>.OnEvent -= EnableUpgradePanel;
    }

    void EnableUpgradePanel(SelectTowerEvent e)
    {
        Stats tStats = e.tower.TowerStats.stats;
        
        upgradePanel.SetActive(true);
        towerNameText.text = e.tower.name;
        statsText.text = "Stats: \n" + "Damage: " + tStats.towerDamage + "\nRange: " + tStats.towerRange + "\nAttack interval: " + tStats.towerAttackInterval;
        /*TODO:
         _isAffordable = e.tower.UpgradeManager.CanAffordUpgrade()
         upgradeButton.isInteractable = _isAffordable
         Stats uStats = e.tower.UpgradeManager.TowerUpgrade.TowerStats.stats
         upgradeStats.text = "Upgarde stats: \n" + ...
         */
    }
    
    
    
}
