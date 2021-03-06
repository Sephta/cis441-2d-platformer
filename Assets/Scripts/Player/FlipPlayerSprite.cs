using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FlipPlayerSprite : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private SpriteRenderer _sr = null;
    [SerializeField] private Rigidbody _parentRB = null;

    [SerializeField, ReadOnly] public Vector2 direction = Vector2.zero;
    [SerializeField, ReadOnly] private Vector2 prevDirection = Vector2.zero;

    private void Awake()
    {
        if (_sr == null && GetComponent<SpriteRenderer>() != null)
            _sr = GetComponent<SpriteRenderer>();
        
        if (_parentRB == null)
            _parentRB = GetComponentInParent<Rigidbody>();
    }

    private void Update()
    {    
        FlipSprite();
        UpdateDirectionVector();
    }

    private void FlipSprite()
    {

        if (direction.x > 0)
            _sr.flipX = false;
        else if (direction.x < 0)
            _sr.flipX = true;
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
