using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;


public class IdleBehavior : EntityBehavior
{
    [Space]
    [Header("Connected Behaviors")]
    [SerializeField] private FollowBehavior followBehavior = null;


    [Header("Determinate Configurables")]
    [SerializeField, Range(0f, 5f)] private float sphereCastRadius = 0.5f;
    [Layer, SerializeField] private int _mask;

    #if UNITY_EDITOR
    [Space]
    [Header("Gizmo Data")]
    [SerializeField] private bool showGizmos = true;
    #endif

    public override EntityBehavior GetCurrentBehavior()
    {
        EntityBehavior resultBehavior = this;

        // Step 1. Perform the desired behavior

        // Step 2. Determine if behavior should switch

        foreach (var hit in Physics.SphereCastAll(transform.position, sphereCastRadius, Vector3.up, 0f))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                resultBehavior = followBehavior;
                followBehavior.objectToFollow = hit.collider.gameObject;
                followBehavior.followRange = sphereCastRadius;
            }
        }

        // Step 3. return new behavior

        return resultBehavior;
    }

    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (!showGizmos)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sphereCastRadius);
    }
    #endif
}
