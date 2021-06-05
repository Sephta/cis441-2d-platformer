using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;


public class ApplyForceBehavior : StateMachineBehaviour
{
    [Header("Configurable Data")]
    [SerializeField] private ForceMode forceType;
    [SerializeField] private Vector3 forceDirection = Vector3.zero;
    [SerializeField] private float forceStrength = 0f;

    [SerializeField, ReadOnly] private Rigidbody _rb = null;
    [SerializeField, ReadOnly] private PlayerMovement _pm = null;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_rb == null)
            _rb = animator.gameObject.GetComponentInParent<Rigidbody>();
        if (_pm == null)
            _pm = animator.gameObject.GetComponentInParent<PlayerMovement>();

        Vector3 finalForceDirection = new Vector3(
            forceDirection.x != 0 ? _pm.direction.x * forceStrength : 0f,
            forceDirection.y != 0 ? _pm.direction.y * forceStrength : 0f,
            0f
        );

        _rb.AddForce(finalForceDirection, forceType);
    }

    // override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}
}
