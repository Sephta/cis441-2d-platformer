using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

using NaughtyAttributes;


public class PlayerStatusHandler : MonoBehaviour
{
    [SerializeField] private ProgressBar UIHealthBar = null;
    [SerializeField] private Animator _anim = null;
    [SerializeField] private Vector3 spawnLocation = Vector3.zero;

    [Header("Player Stats")]
    public int maxHealth = 0;
    [SerializeField, ReadOnly] private int currHealth = 0;
    [SerializeField] private List<MonoBehaviour> componentsToDisable = new List<MonoBehaviour>();

    void Awake()
    {
        if (_anim == null)
            _anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        SetHealth(maxHealth);
        UIHealthBar.max = maxHealth;
        UIHealthBar.current = maxHealth;
    }

    void OnEnable()
    {
        OnPlayerRevive();
    }

    void Update()
    {
        if (currHealth == 0)
            OnPlayerDeath();
    }

    public void DisablePlayerPlayables()
    {
        foreach (var component in componentsToDisable)
        {
            component.enabled = false;
        }
    }

    public void EnablePlayerPlayables()
    {
        foreach(var component in componentsToDisable)
        {
            component.enabled = true;
        }
    }

    public void OnPlayerDeath()
    {
        _anim.SetTrigger("OnPlayerDeath");
        
        DisablePlayerPlayables();

        if (DeathScreenHandler._inst != null)
            DeathScreenHandler._inst.DisplayDeathScreen();
    }

    public void OnPlayerRevive()
    {
        _anim.SetTrigger("OnPlayerRevive");
        
        PlayerInteractionHandler.isPlayerHurting = false;
        PauseMenuHandler.isPaused = false;
        Time.timeScale = 1f;

        EnablePlayerPlayables();

        SetHealth(maxHealth);

        this.transform.position = spawnLocation;

        UIHealthBar.max = maxHealth;
        UIHealthBar.current = maxHealth;
    }

    public void SetHealth(int amount) =>
        currHealth = amount;

    public void UpdateHealth(int amount)
    {
        currHealth = (int) Mathf.Clamp((float)(currHealth - amount), 0f, (float) maxHealth);
        UIHealthBar.current = currHealth;
    }
}
