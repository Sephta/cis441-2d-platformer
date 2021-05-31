using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
#region Menu Functions
    public void OnPlayerPressStart(string SceneToLoad)
    {
        #if UNITY_EDITOR
        Debug.Log("Player pressed start button...");
        Debug.Log("Preparing to load scene: " + SceneToLoad);
        #endif

        SceneManager.LoadScene(SceneToLoad);
    }

    public void OnPlayerPressOptions()
    {
        #if UNITY_EDITOR
        Debug.Log("Player pressed options button.");
        #endif
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
