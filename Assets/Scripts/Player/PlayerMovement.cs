using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // PUBLIC VARS
    [Header("Dependencies")]
    public Rigidbody _rb = null;

    [Header("Movement Data")]
    public bool lockZAxis = false;
    [Range(0f, 100f)] public float movementSpeed = 0f;
    [ReadOnly] public Vector3 moveDir = Vector3.zero;

    // PRIVATE VARS
    private Vector2 direction = Vector2.zero;

#region Unity_Functions
    void Awake()
    {
        if (GetComponent<Rigidbody>() != null)
            _rb = GetComponent<Rigidbody>();
    }

    // void Start() {}

    void Update()
    {
        GetDirectionVector();
    }

    void FixedUpdate()
    {
        if (_rb != null)
        {
            if (!lockZAxis)
                moveDir = new Vector3(direction.x, 0, direction.y);
            else
                moveDir = new Vector3(direction.x, 0, 0);
            _rb.AddForce(((moveDir * movementSpeed * Time.fixedDeltaTime) + _rb.velocity) - _rb.velocity, ForceMode.Impulse);
        }
    }
#endregion

#region Custom_Functions
    /// <summary>
    /// Finds and normalizes input direction using custom InputManager system
    /// </summary>
    private void GetDirectionVector()
    {
        if (InputManager._inst == null)
            return;
        
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
        
        direction = direction.normalized;
    }
#endregion
}
