using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;


public class FlipPlayerHitbox : MonoBehaviour
{
    [Header("Configurable Data")]
    // [SerializeField] private Rigidbody _rb = null;
    [SerializeField] private BoxCollider _col = null;
    [SerializeField] private Vector3 colliderPos = Vector3.zero;
    [SerializeField, ReadOnly] public Vector2 direction = Vector2.zero;
    [SerializeField, ReadOnly] private Vector2 prevDirection = Vector2.zero;

    void Update()
    {
        UpdateDirectionVector();
        FlipHitbox();
    }

    private void FlipHitbox()
    {
        if (direction.x > 0)
            _col.center = colliderPos;
        else if (direction.x < 0)
            _col.center = new Vector3(
                -colliderPos.x,
                colliderPos.y,
                colliderPos.z
            );
    }

    /// <summary>
    /// Finds and normalizes input direction using custom InputManager system
    /// </summary>
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
