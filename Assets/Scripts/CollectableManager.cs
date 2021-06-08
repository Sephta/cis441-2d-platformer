using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;


public class CollectableManager : MonoBehaviour
{
    public static CollectableManager _inst;

    [Space]
    [Header("Collectables Data")]
    public List<GameObject> collectables = new List<GameObject>();
    public List<GameObject> secrets = new List<GameObject>();
    public int MaxCollectables;
    public int MaxSecrets;
    public int MaxEnemies;
    [SerializeField, ReadOnly] private int numCollectablesFound = 0;
    [SerializeField, ReadOnly] private int numSecretsFound = 0;
    [SerializeField, ReadOnly] private int numEnemiesKilled = 0;

    public int NumCollectablesFound => numCollectablesFound;
    public int NumSecretsFound => numSecretsFound;
    public int NumEnemiesKilled => numEnemiesKilled;

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
    }

    void Start()
    {
        ResetNumCollectablesFound();
        ResetNumSecretsFound();
        ResetNumEnemiesKilled();

        MaxCollectables = collectables.Count;
        MaxSecrets = secrets.Count;
    }

    public void IncrementNumEnemiesKilled()
        => numEnemiesKilled += 1;
    
    public void ResetNumEnemiesKilled()
        => numEnemiesKilled = 0;
    
    public void IncrementNumCollectablesFound()
        => numCollectablesFound += 1;
    
    public void ResetNumCollectablesFound()
        => numCollectablesFound = 0;
    
    public void IncrementNumSecretsFound()
        => numSecretsFound += 1;
    
    public void ResetNumSecretsFound()
        => numSecretsFound = 0;
}
