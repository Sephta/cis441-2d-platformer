using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using NaughtyAttributes;
using TMPro;


public class PauseMenuHandler : MonoBehaviour
{
    public static bool isPaused = false;
    [SerializeField] private GameObject pauseMenu = null;
    [SerializeField, ReadOnly] private float defaultTimeScale = 1f;

    public static UnityAction TogglePausedGameState;

    private bool prevPausedState = false;

    void Start()
    {
        defaultTimeScale = Time.timeScale;
        prevPausedState = isPaused;
    }

#region EnableDisable
    private void OnEnable()
    {
        TogglePausedGameState += TogglePauseState;
        TogglePausedGameState += TogglePauseMenu;
    }

    private void OnDisable()
    {
        TogglePausedGameState -= TogglePauseState;
        TogglePausedGameState -= TogglePauseMenu;
    }
#endregion

    void LateUpdate()
    {
        if (prevPausedState != isPaused)
            ChangeTimeScale();
    }

    private void ChangeTimeScale() => Time.timeScale = (!isPaused) ? defaultTimeScale : 0f;

#region Event Functions
    private void TogglePauseState()
    {
        prevPausedState = isPaused;
        isPaused = !isPaused;
    }

    private void TogglePauseMenu()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(isPaused);
        }
    }
#endregion
}
