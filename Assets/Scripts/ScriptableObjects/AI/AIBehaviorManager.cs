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
}
