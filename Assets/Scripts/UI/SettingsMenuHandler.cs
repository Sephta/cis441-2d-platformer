using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using NaughtyAttributes;
using TMPro;


public class SettingsMenuHandler : MonoBehaviour
{
    [Space]
    [Header("Settings UI Objects")]
    [SerializeField] private Toggle showFPSToggle = null;


    [SerializeField, ReadOnly] private GameSettingsHandler GameSettings = null;


    private void Start()
    {
        if (GameSettingsHandler._inst != null)
            GameSettings = GameSettingsHandler._inst;

        if (GameSettings != null)
            InitSettingsMenu();
    }

    public void OnFPSToggleValueChanged()
    {
        if (GameSettings == null)
        {
            #if UNITY_EDITOR
            Debug.Log("<OnFPSToggleValueChanged> - Reference to GameSettingsHandler in 'SettingsMenuHandler' is null...");
            #endif
            return;
        }

        #if UNITY_EDITOR
        Debug.Log("FPS toggle value changed...");
        #endif

        GameSettings.showFPS = showFPSToggle.isOn;
    }

    public void OnPlayerPressedCloseMenu()
    {
        gameObject.SetActive(false);
    }

    private void InitSettingsMenu()
    {
        if (showFPSToggle != null)
            showFPSToggle.isOn = GameSettings.showFPS;
    }
}
