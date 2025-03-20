using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    [SerializeField] private int startingMoney = 100;
    [HideInInspector] 
    public int Money;

    private static EconomyManager instance;
    public static EconomyManager Instance => instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        Money = startingMoney;
        EventBus<EnemyDeathEvent>.OnEvent += AddMoney;
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
