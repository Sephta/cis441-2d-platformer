using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    [Space]
    [Header("UI")]
    [SerializeField] private GameObject SettingsMenu = null;

#region Menu Functions
    public void OnPlayerPressStart(string SceneToLoad)
    {
        #if UNITY_EDITOR
        Debug.Log("Player pressed start button...");
        Debug.Log("Preparing to load scene: " + SceneToLoad);
        #endif

        
        // for (int i = 0; i < SceneManager.sceneCount; i++)
        // {
        //     var currScene = SceneManager.GetSceneAt(i);
        //     if (currScene.name == "GlobalManagers")
        //     {
        //         SceneManager.LoadSceneAsync(SceneToLoad);
        //         return;
        //     }
        // }

        SceneManager.LoadScene(SceneToLoad);
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
