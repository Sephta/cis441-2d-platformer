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
    [Range(0f, 5f)] public float sprintMultiplier = 0f;
    [ReadOnly] public Vector3 moveDir = Vector3.zero;
    [Range(0f, 1f)] public float moveDirFalloff = 0.5f;

    [Header("Debug Data")]
    // PRIVATE VARS
    [SerializeField, ReadOnly] private Vector2 direction = Vector2.zero;
    [SerializeField, ReadOnly] private Vector2 prevDirection = Vector2.zero;

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
        
        // if ((direction == Vector2.zero || (prevDirection != direction)) && _rb.velocity.y == 0)
        if (direction == Vector2.zero && _rb.velocity.y == 0)
        {
            _rb.velocity = new Vector3(_rb.velocity.x * moveDirFalloff, _rb.velocity.y, _rb.velocity.z * moveDirFalloff);
        }
    }

    void FixedUpdate()
    {
        if (_rb != null)
        {
            if (!lockZAxis)
                moveDir = new Vector3(direction.x, 0, direction.y);
            else
                moveDir = new Vector3(direction.x, 0, 0);
            
            float move = (Input.GetKey(InputManager._inst._keyBindings[InputAction.run])) ? movementSpeed * sprintMultiplier : movementSpeed;

            // if (_rb.velocity.y == 0)
            _rb.AddForce((moveDir * move * Time.fixedDeltaTime), ForceMode.VelocityChange);
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
        
        direction = direction.normalized;
    }
#endregion
}
