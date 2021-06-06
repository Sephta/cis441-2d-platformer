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
    // [SerializeField, ReadOnly] private PlayerMovement _pm = null;

    [SerializeField, ReadOnly] public Vector2 direction = Vector2.zero;
    [SerializeField, ReadOnly] private Vector2 prevDirection = Vector2.zero;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_rb == null)
            _rb = animator.gameObject.GetComponentInParent<Rigidbody>();
        // if (_pm == null)
        //     _pm = animator.gameObject.GetComponentInParent<PlayerMovement>();

        UpdateDirectionVector();

        Vector3 finalForceDirection = new Vector3(
            forceDirection.x != 0 ? direction.x * forceStrength : 0f,
            forceDirection.y != 0 ? direction.y * forceStrength : 0f,
            0f
        );

        _rb.AddForce(finalForceDirection, forceType);
    }

    // override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}

    private void UpdateDirectionVector()
    {
        if (InputManager._inst == null)
            return;
        
        prevDirection = direction;
        
        InputManager iManager = InputManager._inst;

        if (Input.GetKey(iManager._keyBindings[InputAction.moveUp]))
            direction.y = 1.0f;
        else if (Input.GetKey(iManager._keyBindings[InputAction.moveDown]))
            direction.y = -1.0f;
        else
            direction.y = 0;
        if (Input.GetKey(iManager._keyBindings[InputAction.moveLeft]))
            direction.x = -1.0f;
        else if (Input.GetKey(iManager._keyBindings[InputAction.moveRight]))
            direction.x = 1.0f;
        else
            direction.x = 0;
        
        // direction = direction.normalized;
    }
}
