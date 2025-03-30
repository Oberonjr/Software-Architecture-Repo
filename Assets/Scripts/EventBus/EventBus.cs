using System;
using System.Collections.Generic;
using UnityEngine;
using Udar.SceneManager;

public abstract class Event{}

/*
 * Main communication avenue for the codebase
 * Uses OnEvent for listeners
 * and Publish for sending out the signals
 */
public class EventBus<T> where T : Event
{
    public static event Action<T> OnEvent;

    public static void Publish(T pEvent)
    {
        OnEvent?.Invoke(pEvent);
    }
}

#region Cheatsheet
//Dummy logic to remember the syntax
//This is not meant to be used nor is the syntax correct, but it's essentially a cheat sheet
//public
class TestEvent : Event
{
    public int testInt;

    public TestEvent(int ptestInt)
    {
        testInt = ptestInt;
    }

    void ListenerScript(TestEvent e)
    {
        Debug.Log("Test listener script, value broadcasted is: " + e.testInt);
    }

    void AddListener()
    {
        EventBus<TestEvent>.OnEvent += ListenerScript;
    }

    void PublishEvent(int value)
    {
        EventBus<TestEvent>.Publish(new TestEvent(value));
    }
}
#endregion
//---------------------ACTUAL EVENTS-------------------------------


//Enemy events
public class EnemySpawnEvent : Event
{
    public EnemyStats enemy;

    public EnemySpawnEvent(EnemyStats pEnemy)
    {
        enemy = pEnemy;
    }
}

public class EnemyDeathEvent: Event
{
    public EnemyStats enemy;

    public EnemyDeathEvent(EnemyStats pEnemy)
    {
        enemy = pEnemy;
    }
}

public class InitializeEnemySpawnersEvent:Event
{
    public EnemySpawner spawner;

    public InitializeEnemySpawnersEvent(EnemySpawner pSpawner)
    {
        spawner = pSpawner;
    }
}

//Tower events
public class SelectTowerEvent : Event
{
    public TowerController tower;

    public SelectTowerEvent(TowerController pTower)
    {
        tower = pTower;
    }
}

public class DeselectTowerEvent : Event{}

public class PlayAttackAnimationEvent : Event
{
    public Vector3 direction;

    public PlayAttackAnimationEvent(Vector3 pDirection)
    {
        direction = pDirection;
    }
}

//Input events
public class ClickNodeEvent : Event
{
    public Node clickNode;

    public ClickNodeEvent(Node pClickNode)
    {
        clickNode = pClickNode;
    }
}

public class HoverNodeEvent : Event
{
    public Node hoverNode;

    public HoverNodeEvent(Node pHoverNode)
    {
        hoverNode = pHoverNode;
    }
}

//Building events
public class ToggleHoverEvent : Event
{
    public bool hoverValue;

    public ToggleHoverEvent(bool pValue)
    {
        hoverValue = pValue;
    }
}

public class SelectTowerToBuildEvent : Event
{
    public KeyCode key;

    public SelectTowerToBuildEvent(KeyCode pKey)
    {
        key = pKey;
    }
}

public class BuildTowerEvent : Event
{
    public Vector3 position;

    public BuildTowerEvent(Vector3 pPosition)
    {
        position = pPosition;
    }
}

//Economy events

public class UpdateMoneyBalannceEvent : Event
{
    public int amount;

    public UpdateMoneyBalannceEvent(int pAmount)
    {
        amount = pAmount;   
    }
}

//Health events
public class ModifyHealthEvent : Event
{
    public int healthChange;

    public ModifyHealthEvent(int pHealthChange)
    {
        healthChange = pHealthChange;
    }
}

public class UpdateHealthEvent : Event
{
    public int currentHealth;

    public UpdateHealthEvent(int pCurrentHealth)
    {
        currentHealth = pCurrentHealth;
    }
}

public class GameOverEvent : Event {}

public class WinEvent: Event{}

//Wave events
public class StartWaveEvent: Event{}

public class EndWaveEvent : Event
{
    public int waveNumber;

    public EndWaveEvent(int pWaveNumber)
    {
        waveNumber = pWaveNumber;
    }
}

public class StartBuildPhaseEvent : Event{}

public class UpdateTimerEvent : Event
{
    public int time;

    public UpdateTimerEvent(int pTime)
    {
        time = pTime;
    }
}

//Scene management
public class SwitchSceneEvent: Event
{
    public SceneField scene;

    public SwitchSceneEvent(SceneField pScene)
    {
        scene = pScene;
    }
}
