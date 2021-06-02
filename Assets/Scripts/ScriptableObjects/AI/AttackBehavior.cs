using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;


public class AttackBehavior : EntityBehavior
{
    [Space]
    [Header("Connected Behaviors")]
    [SerializeField] private IdleBehavior idleBehavior = null;
    [SerializeField] private FollowBehavior followBehavior = null;

    [Space]
    [Header("Attack Data")]
    [SerializeField] private float maxAtkTime = 0f;
    [SerializeField, ReadOnly] private float currAtkTime = 0f;

    private void Start()
    {
        ResetAtkTimer();
    }

    public override EntityBehavior GetCurrentBehavior()
    {
        EntityBehavior resultBehavior = this;

        // Step 1. Perform the desired behavior

        UpdateAtkTimer();

        #if UNITY_EDITOR
        Debug.Log("[Entity] attacking [Player]");
        #endif

        // Step 2. Determine if behavior should switch

        if (currAtkTime == 0)
            resultBehavior = idleBehavior;

        // Step 3. return new behavior

        return resultBehavior;
    }

    public void ResetAtkTimer() => currAtkTime = maxAtkTime;
    private void UpdateAtkTimer() => currAtkTime = Mathf.Clamp(currAtkTime - Time.deltaTime, 0f, maxAtkTime);
}
