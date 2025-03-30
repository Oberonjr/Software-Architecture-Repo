using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Udar.SceneManager;

//Logic for UI Buttons to change through the scenes
public class SceneSwitchButton : MonoBehaviour
{
    [SerializeField] SceneField scene;

    public void LoadScene()
    {
        Time.timeScale = 1;
        EventBus<SwitchSceneEvent>.Publish(new SwitchSceneEvent(scene));
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneSwitcherManager.Instance.LoadMainMenu();
    }

    public void ReloadLevel()
    {
        Time.timeScale = 1;
        SceneSwitcherManager.Instance.ReloadLevel();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
