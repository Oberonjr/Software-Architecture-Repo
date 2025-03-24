using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance => instance;
    
    [SerializeField]private TMP_Text moneyText;
    [SerializeField]private TMP_Text timeText;
    [SerializeField]private TMP_Text healthText;
    
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
        
        EventBus<UpdateHealthEvent>.OnEvent += UpdateHealthText;
        EventBus<UpdateMoneyBalannceEvent>.OnEvent += UpdateMoneyText;
    }

    void OnDestroy()
    {
        EventBus<UpdateHealthEvent>.OnEvent -= UpdateHealthText;
        EventBus<UpdateMoneyBalannceEvent>.OnEvent -= UpdateMoneyText;
    }

    void UpdateHealthText(UpdateHealthEvent e)
    {
        healthText.text = e.currentHealth.ToString();
    }

    void UpdateMoneyText(UpdateMoneyBalannceEvent e)
    {
        moneyText.text = e.amount.ToString();
            
    }
}
