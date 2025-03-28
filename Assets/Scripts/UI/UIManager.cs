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
    
    [SerializeField]private GameObject winScreen;
    [SerializeField]private GameObject gameOverPanel;
    [SerializeField]private GameObject pausePanel;
    
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
        EventBus<WinEvent>.OnEvent += ShowWinScreen;
        EventBus<GameOverEvent>.OnEvent += ShowGameOverScreen;
        
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
        EventBus<WinEvent>.OnEvent -= ShowWinScreen;
        EventBus<GameOverEvent>.OnEvent -= ShowGameOverScreen;
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
        if (timeText != null)
        {
            timeText.text = "Time left to build: "+ e.time;
        }
    }

    void EnableTimerText(StartBuildPhaseEvent e)
    {
        if (timeText != null)
        {
            timeText.gameObject.SetActive(true);
        }
    }

    void UpdateWaveText(StartWaveEvent e = null)
    {
        if (timeText != null)
        {
            timeText.gameObject.SetActive(false);
            waveText.text = "Wave: " + (WaveManager.Instance.CurrentWaveIndex + 1) + " / " + WaveManager.Instance.Waves.Count;
        }
    }

    void ShowWinScreen(WinEvent e)
    {
        winScreen.SetActive(true);
        Time.timeScale = 0;
    }

    void ShowGameOverScreen(GameOverEvent e)
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    void ShowPauseScreen()
    {
        pausePanel.SetActive(true);
    }
}
