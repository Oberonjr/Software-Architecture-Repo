using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPreviewController : MonoBehaviour
{
    [SerializeField] private TowerController towerToSpawn;
    [SerializeField] private SpriteRenderer rangeIndicator;

    public TowerController TowerToSpawn => towerToSpawn;
    public void Awake()
    {
        EventBus<BuildTowerEvent>.OnEvent += PlaceTower;
        rangeIndicator.transform.localScale = new Vector3(towerToSpawn.TowerStats.stats.towerRange * 2, towerToSpawn.TowerStats.stats.towerRange * 2, 1);
    }

    void OnDestroy()
    {
        EventBus<BuildTowerEvent>.OnEvent -= PlaceTower;
    }
    
    public void PlaceTower(BuildTowerEvent e)
    {
        TowerController tower = Instantiate(towerToSpawn, e.position, Quaternion.identity);
        EconomyManager.Instance.SpendMoney(towerToSpawn.TowerStats.stats.cost);
        GridManager.Instance.GetNode(e.position).placedObject = tower.gameObject;
        Destroy(gameObject);
    }
}
