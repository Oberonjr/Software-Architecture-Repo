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
        EventBus<DeselectTowerEvent>.OnEvent += DisableUpgradePanel;
    }

    private void OnDestroy()
    {
        EventBus<SelectTowerEvent>.OnEvent -= EnableUpgradePanel;
        EventBus<DeselectTowerEvent>.OnEvent -= DisableUpgradePanel;
    }

    void EnableUpgradePanel(SelectTowerEvent e)
    {
        Stats tStats = e.tower.TowerStats.stats;
        upgradePanel.SetActive(true);
        towerNameText.text = e.tower.name;
        statsText.text = "Stats: \n" + "Damage: " + tStats.towerDamage + "\nRange: " + tStats.towerRange + "\nAttack interval: " + tStats.towerAttackInterval;
        TowerUpgradeButton upLog = upgradeButton.GetComponent<TowerUpgradeButton>();

        if (tStats.upgradeTower == null || EconomyManager.Instance == null)
        {
            upgradeButton.gameObject.SetActive(false);
            upgradeStatsText.gameObject.SetActive(false);
        }
        else
        {
            upgradeButton.gameObject.SetActive(true);
            upgradeStatsText.gameObject.SetActive(true);
            _isAffordable = EconomyManager.Instance.CanAfford(tStats.upgradeTower.TowerStats.stats.cost);
            upgradeButton.interactable = _isAffordable;
            upLog.towerToUpgrade = e.tower;
            upLog.UpdateCostText();
            Stats uStats = tStats.upgradeTower.TowerStats.stats;
            upgradeStatsText.text = "Upgrade stats: \n" + "Damage: " + uStats.towerDamage + "\nRange: " + uStats.towerRange + "\nAttack interval: " + uStats.towerAttackInterval;
        }
    }

    void DisableUpgradePanel(DeselectTowerEvent e)
    {
        upgradePanel.SetActive(false);
    }
    
    
}
