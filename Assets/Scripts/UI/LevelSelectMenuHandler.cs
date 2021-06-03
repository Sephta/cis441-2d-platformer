using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectMenuHandler : MonoBehaviour
{
    public void CloseSelf()
    {
        gameObject.SetActive(false);
    }

    public void LoadScene(string SceneToLoad)
    {
        #if UNITY_EDITOR
        Debug.Log("Player pressed start button...");
        Debug.Log("Preparing to load scene: " + SceneToLoad);
        #endif

        SceneManager.LoadScene(SceneToLoad);
    }
}
