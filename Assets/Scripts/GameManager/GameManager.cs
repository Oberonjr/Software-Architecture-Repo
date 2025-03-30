using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/*
 * Main Manager of the game, handling the health, gameSpeed and stateMachine switches
 * Has the values that dictate the total health and buildPhase timer
 * Has functions used in various UI elements to manage the gameState
 * Handles the buildPhase timer
 * Handles which tower is currently being held for build Queue
 * Handles tower deselection event for UI related things
 */
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    public static GameManager Instance => _instance; 
    
    [HideInInspector] public bool isSpedUp;

    [SerializeField] private int maxHealth;
    [SerializeField] private int buildTime;

    private int _currentHealth;
    private TowerController _currentSelectedTower;
    private StateMachine _stateMachine;

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
        _stateMachine = new StateMachine();
    }

    private void Start()
    {
        _currentHealth = maxHealth;
        EventBus<SelectTowerEvent>.OnEvent += UpdateCurrentTower;
        EventBus<DeselectTowerEvent>.OnEvent += DeselectTower;
        EventBus<ModifyHealthEvent>.OnEvent += ChangeHealth;
        EventBus<UpdateHealthEvent>.Publish(new UpdateHealthEvent(_currentHealth));
        EventBus<StartBuildPhaseEvent>.OnEvent += StartTimer;
    }

    private void OnDestroy()
    {
        EventBus<SelectTowerEvent>.OnEvent -= UpdateCurrentTower;
        EventBus<DeselectTowerEvent>.OnEvent -= DeselectTower;
        EventBus<ModifyHealthEvent>.OnEvent -= ChangeHealth;
        EventBus<StartBuildPhaseEvent>.OnEvent -= StartTimer;
    }

    void UpdateCurrentTower(SelectTowerEvent e)
    {
        if (_currentSelectedTower != null)
        {
            _currentSelectedTower.ToggleRangeIndicator();
        }
        _currentSelectedTower = e.tower;
        _currentSelectedTower.ToggleRangeIndicator();
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    void DeselectTower(DeselectTowerEvent e)
    {
        if (_currentSelectedTower != null)
        {
            _currentSelectedTower.ToggleRangeIndicator();
        }
        _currentSelectedTower = null;
    }

    void StartTimer(StartBuildPhaseEvent e)
    {
        StartCoroutine(BuildPhaseTimer());
    }
    
    IEnumerator BuildPhaseTimer()
    {
        float timeLeft = buildTime;
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            EventBus<UpdateTimerEvent>.Publish(new UpdateTimerEvent((int)timeLeft));
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }
        StartNextWave();
    }
    
    public void ToggleGameSpeed()
    {
        isSpedUp = !isSpedUp;
        Time.timeScale = isSpedUp ? 3 : 1;
    }

    public void StartNextWave()
    {
        if (InputManager.Instance.canBuild)
        {
            StopAllCoroutines();
            _stateMachine.ChangeState(new CombatState(_stateMachine));
        }
    }

    public void StartBuildPhase()
    {
        _stateMachine.ChangeState(new BuildState(_stateMachine));
    }

    void ChangeHealth(ModifyHealthEvent e)
    {
        _currentHealth += e.healthChange;
        EventBus<UpdateHealthEvent>.Publish(new UpdateHealthEvent(_currentHealth));
        if (_currentHealth <= 0)
        {
            EventBus<GameOverEvent>.Publish(new GameOverEvent());
        }
    }
    
}
