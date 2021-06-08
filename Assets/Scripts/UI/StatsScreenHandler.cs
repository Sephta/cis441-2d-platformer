using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using NaughtyAttributes;


public class StatsScreenHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI collectableText = null;
    [SerializeField] private TextMeshProUGUI secretText = null;
    [SerializeField] private TextMeshProUGUI enemyText = null;

    [SerializeField, ReadOnly] private PlayerStatusHandler pHandler = null;

    private CollectableManager cManager = null;

    void Start()
    {
        if (CollectableManager._inst != null)
            cManager = CollectableManager._inst;
        
        if (pHandler == null)
            pHandler = FindObjectOfType<PlayerStatusHandler>();
    }

    void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        collectableText.text = "Collectables collected   " + cManager.NumCollectablesFound + " / " + cManager.MaxCollectables.ToString();
        secretText.text      = "Secrets un-secreted      " + cManager.NumSecretsFound + " / " + cManager.MaxSecrets.ToString();
        enemyText.text       = "Enemies defeated         " + cManager.NumEnemiesKilled + " / " + cManager.MaxEnemies.ToString();
    }
}
