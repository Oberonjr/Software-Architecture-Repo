using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Handles the current money amount of the Player
 * As well as checking if purchases are valid
 * Modifies the remaining money according to relevant inputs
 * And informs the UI parameters through events with the new balance
 */
public class EconomyManager : MonoBehaviour
{
    [SerializeField] private int startingMoney = 100;
    [HideInInspector] 
    public int Money;

    private static EconomyManager _instance;
    public static EconomyManager Instance => _instance;

    void Awake()
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
    
    void Start()
    {
        Money = startingMoney;
        EventBus<EnemyDeathEvent>.OnEvent += AddMoney;
        EventBus<UpdateMoneyBalannceEvent>.Publish(new UpdateMoneyBalannceEvent(Money));
    }

    void OnDestroy()
    {
        EventBus<EnemyDeathEvent>.OnEvent -= AddMoney;
    }
    
    void AddMoney(EnemyDeathEvent e)
    {
        AddMoney(e.enemy.DeathReward);
    }

    public void AddMoney(int amount)
    {
        Money += amount;
        EventBus<UpdateMoneyBalannceEvent>.Publish(new UpdateMoneyBalannceEvent(Money));
    }

    public void SpendMoney(int amount)
    {
        if (!CanAfford(amount))
        {
            Debug.LogError("Trying to spend money you don't have. There is an error in the tower selection process.");
            return;
        }
        Money -= amount;
        EventBus<UpdateMoneyBalannceEvent>.Publish(new UpdateMoneyBalannceEvent(Money));
    }

    public bool CanAfford(int cost)
    {
        return Money >= cost;
    }
}
