using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

using NaughtyAttributes;


public class PlayerStatusHandler : MonoBehaviour
{
    [SerializeField] private Animator _anim = null;
    [SerializeField] private CinemachineVirtualCamera virtualCam = null;
    [SerializeField] private Camera playerCam = null;
    [SerializeField] private Vector3 spawnLocation = Vector3.zero;
    [SerializeField] private Vector3 camLocation = Vector3.zero;

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
    }

    void Update()
    {
        if (currHealth == 0)
            OnPlayerDeath();
    }

    private void OnPlayerDeath()
    {
        _anim.SetTrigger("OnPlayerDeath");
        foreach (var component in componentsToDisable)
        {
            component.enabled = false;
        }

        if (DeathScreenHandler._inst != null)
            DeathScreenHandler._inst.DisplayDeathScreen();
    }

    public void OnPlayerRevive()
    {
        _anim.SetTrigger("OnPlayerRevive");
        
        PlayerInteractionHandler.isPlayerHurting = false;

        SetHealth(maxHealth);

        foreach(var component in componentsToDisable)
        {
            component.enabled = true;
        }

        this.transform.position = spawnLocation;

        if (virtualCam != null)
        {
            // virtualCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = camLocation;
            virtualCam.enabled = false;
            virtualCam.transform.SetPositionAndRotation(camLocation, Quaternion.Euler(8.13f, 0f, 0f));
            playerCam.transform.SetPositionAndRotation(camLocation, Quaternion.Euler(8.13f, 0f, 0f));
            virtualCam.enabled = true;
        }
    }

    public void SetHealth(int amount) =>
        currHealth = amount;

    public void UpdateHealth(int amount) =>
        currHealth = (int) Mathf.Clamp((float)(currHealth - amount), 0f, (float) maxHealth);
}
