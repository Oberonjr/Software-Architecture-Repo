using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    public static GameManager Instance{get { return _instance; }}
    
    [HideInInspector] public bool isSpedUp;

    [SerializeField] private int maxHealth;
    private int currentHealth;

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
        currentHealth = maxHealth;
        EventBus<SelectTowerEvent>.OnEvent += UpdateCurrentTower;
        EventBus<DeselectTowerEvent>.OnEvent += DeselectTower;
        EventBus<ModifyHealthEvent>.OnEvent += ChangeHealth;
        EventBus<UpdateHealthEvent>.Publish(new UpdateHealthEvent(currentHealth));
    }

    private void OnDestroy()
    {
        EventBus<SelectTowerEvent>.OnEvent -= UpdateCurrentTower;
        EventBus<DeselectTowerEvent>.OnEvent -= DeselectTower;
        EventBus<ModifyHealthEvent>.OnEvent -= ChangeHealth;
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

    public void StartNextWave()
    {
        EventBus<StartWaveEvent>.Publish(new StartWaveEvent());
    }

    void ChangeHealth(ModifyHealthEvent e)
    {
        currentHealth += e.healthChange;
        EventBus<UpdateHealthEvent>.Publish(new UpdateHealthEvent(currentHealth));
        if (currentHealth >= 0)
        {
            EventBus<GameOverEvent>.Publish(new GameOverEvent());
        }
    }
    
}
