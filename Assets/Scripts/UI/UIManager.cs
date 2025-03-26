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
    [SerializeField]private TMP_Text waveText;
    
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
        EventBus<UpdateTimerEvent>.OnEvent += UpdateTimeText;
        EventBus<StartBuildPhaseEvent>.OnEvent += EnableTimerText;
        EventBus<StartWaveEvent>.OnEvent += UpdateWaveText;
    }

    void Start()
    {
        UpdateWaveText();
    }

    void OnDestroy()
    {
        EventBus<UpdateHealthEvent>.OnEvent -= UpdateHealthText;
        EventBus<UpdateMoneyBalannceEvent>.OnEvent -= UpdateMoneyText;
        EventBus<UpdateTimerEvent>.OnEvent -= UpdateTimeText;
        EventBus<StartBuildPhaseEvent>.OnEvent -= EnableTimerText;
        EventBus<StartWaveEvent>.OnEvent -= UpdateWaveText;
    }

    void UpdateHealthText(UpdateHealthEvent e)
    {
        healthText.text = e.currentHealth.ToString();
    }

    void UpdateMoneyText(UpdateMoneyBalannceEvent e)
    {
        moneyText.text = e.amount.ToString();
    }

    void UpdateTimeText(UpdateTimerEvent e)
    {
        timeText.text = "Time left to build: "+ e.time;
    }

    void EnableTimerText(StartBuildPhaseEvent e)
    {
        timeText.gameObject.SetActive(true);
    }

    void UpdateWaveText(StartWaveEvent e = null)
    {
        timeText.gameObject.SetActive(false);
        waveText.text = (WaveManager.Instance.CurrentWaveIndex + 1) + " / " + WaveManager.Instance.Waves.Count;
    }
}
