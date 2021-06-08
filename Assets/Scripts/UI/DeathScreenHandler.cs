using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using NaughtyAttributes;


public class DeathScreenHandler : MonoBehaviour
{
    public static DeathScreenHandler _inst;

    [Header("Dependencies")]
    [SerializeField] private GameObject endScreen = null;
    [ReadOnly] public bool isEndScreenActive = false;
    [SerializeField, ReadOnly] private float defaultTimeScale = 1f;
    [SerializeField] private PlayerStatusHandler pStats = null;

    void Awake()
    {
        // Confirm singleton instance is active
        if (_inst == null)
        {
            _inst = this;
            // DontDestroyOnLoad(this);
        }
        else if (_inst != this)
            GameObject.Destroy(this.gameObject);
        
        if (pStats == null)
            pStats = FindObjectOfType<PlayerStatusHandler>();
    }

    void Start()
    {
        defaultTimeScale = Time.timeScale;
    }

    void LateUpdate()
    {
        ChangeTimeScale();
    }

    private void ChangeTimeScale() => Time.timeScale = (isEndScreenActive) ? 0f : defaultTimeScale;

    public void DisplayDeathScreen() 
    {
        if (endScreen != null)
            endScreen.SetActive(true);
        
        PauseMenuHandler.isPaused = false;
        
        isEndScreenActive = true;
    }

    public void CloseDeathScreen()
    {
        if (endScreen != null)
            endScreen.SetActive(false);
        
        PauseMenuHandler.isPaused = false;

        pStats.OnPlayerRevive();

        // SceneManager.LoadScene("MainMenu");
        
        isEndScreenActive = false;
    }
}
