using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;


public class FollowBehavior : EntityBehavior
{
    [Space]
    [Header("Connected Behaviors")]
    [SerializeField] private IdleBehavior idleBehavior = null;
    [SerializeField] private AttackBehavior attackBehavior = null;

    [Space]
    [Header("Connectable Elements")]
    [SerializeField] private Rigidbody _rb = null;
    [ReadOnly] public GameObject objectToFollow = null;
    [SerializeField] private Animator _anim = null;

    [Space]
    [Header("Follow Data")]
    [SerializeField, Range(0f, 25f)] private float movementSpeed = 0f;
    [SerializeField, Range(0f, 1f)] private float movementFalloff = 1f;
    [SerializeField, ReadOnly] private Vector3 moveDir = Vector3.zero;

    [Header("Determinate Configurables")]
    [SerializeField, ReadOnly] public float followRange = 0f;
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
        moveDir = (objectToFollow.transform.position - this.transform.position).normalized;
        _rb.velocity = new Vector3(
            moveDir.x * movementSpeed,
            _rb.velocity.y,
            _rb.velocity.z
        );

        // Step 2. Determine if behavior should switch

        var rangeTest = Vector3.Distance(objectToFollow.transform.position, this.transform.position);

        // #if UNITY_EDITOR
        // Debug.Log("Distance: " + rangeTest);
        // #endif

        if ( rangeTest > followRange)
        {
            resultBehavior = idleBehavior;

            // slows the rigidbody...
            _rb.velocity *= movementFalloff;
        }

        foreach (var hit in Physics.SphereCastAll(transform.position, sphereCastRadius, Vector3.up, 0f))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                _rb.velocity *= movementFalloff;
                resultBehavior = attackBehavior;
                _anim.SetTrigger("attack");
                attackBehavior.ResetAtkTimer();
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

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereCastRadius);
    }
    #endif
}
