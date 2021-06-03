using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    [Space]
    [Header("UI")]
    [SerializeField] private GameObject SettingsMenu = null;
    [SerializeField] private GameObject LevelSelectMenu = null;

#region Menu Functions
    public void OnPlayerPressStart()
    {
        if (LevelSelectMenu == null)
            return;

        LevelSelectMenu.SetActive(true);
    }

    public void OnPlayerPressOptions()
    {
        #if UNITY_EDITOR
        Debug.Log("Player pressed options button...");
        #endif

        if (SettingsMenu != null)
        {
            SettingsMenu.SetActive(true);
        }
        else
        {
            #if UNITY_EDITOR
            Debug.Log("SettingsMenu reference in 'MainMenuHandler' is null...");
            #endif
        }
    }

    public void OnPlayerPressQuit()
    {
        #if UNITY_EDITOR
        Debug.Log("Application quitting...");
        #endif

        Application.Quit();
    }
#endregion
}
