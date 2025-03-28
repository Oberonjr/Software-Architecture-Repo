using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Udar.SceneManager;


public class SceneSwitcherManager : MonoBehaviour
{
    private static SceneSwitcherManager _instance;
    public static SceneSwitcherManager Instance => _instance;
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
        DontDestroyOnLoad(this.gameObject);
        EventBus<SwitchSceneEvent>.OnEvent += LoadLevel;
    }

    void OnDestroy()
    {
        EventBus<SwitchSceneEvent>.OnEvent -= LoadLevel;
    }
    
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel(SceneField scene)
    {
        SceneManager.LoadScene(scene.BuildIndex);
    }

    void LoadLevel(SwitchSceneEvent e)
    {
        LoadLevel(e.scene);
    }
}
