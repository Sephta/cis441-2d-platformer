using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;


public class AIBehaviorManager : MonoBehaviour
{
    [Space]
    [Header("Management Configurables")]
    [SerializeField] private EntityBehavior defaultStartingBehavior = null;
    [SerializeField, ReadOnly] private EntityBehavior currentBehavior = null;
    [SerializeField] private BoxCollider _col = null;

    private void Start()
    {
        currentBehavior = defaultStartingBehavior;
    }

    private void Update()
    {
        UpdateBehaviorOnThisFrame();
    }

    private void UpdateBehaviorOnThisFrame()
    {
        EntityBehavior nextBehavior = currentBehavior?.GetCurrentBehavior();

        if (nextBehavior != null)
        {
            BehaviorSwitch(nextBehavior);
        }
    }

    private void BehaviorSwitch(EntityBehavior newBehavior)
    {
        currentBehavior = newBehavior;
    }

    public void OnEntityHit(GameObject hitBy)
    {
        #if UNITY_EDITOR
        Debug.Log("[Entity] hit by: " + hitBy.name);
        #endif
    }

    public void EnableEntityHitbox()
    {
        if (_col != null)
        {
            _col.enabled = true;
        }
    }

    public void DisableEntityHitbox()
    {
        if (_col != null)
        {
            _col.enabled = false;
        }
    }
}
