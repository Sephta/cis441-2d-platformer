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
    [Range(0f, 100f)] public float airStrafeSpeed = 0f;
    [ReadOnly] public Vector3 moveDir = Vector3.zero;
    [Range(0f, 1f)] public float moveDirFalloff = 0.5f;
    [Range(0f, 100f)] public float dashStrength = 0f;

    [Header("Dash Counter")]
    [Range(0f, 5f)] public int numDashes = 0;
    [SerializeField, ReadOnly] private int currDashCount = 0;

    [Header("Dash Timer")]
    [Range(0f, 5f)] public float maxDashTimer = 0f;
    [SerializeField, ReadOnly] private bool dashTimerActive = false;
    [SerializeField, ReadOnly] private float _currDashTime = 0f;

    [Header("Debug Data")]
    // PRIVATE VARS
    [SerializeField, ReadOnly] private Vector2 direction = Vector2.zero;
    [SerializeField, ReadOnly] private Vector2 prevDirection = Vector2.zero;

    private StaticGroundedManager iGrounded = null;

#region Unity_Functions
    void Awake()
    {
        if (GetComponent<Rigidbody>() != null)
            _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        if (iGrounded == null && StaticGroundedManager._inst != null)
            iGrounded = StaticGroundedManager._inst;
        
        currDashCount = numDashes;
    }

    void Update()
    {
        GetDirectionVector();
        UpdateDashCounter();

        if (dashTimerActive)
            UpdateDashTimer();
        
        if ((direction.x == 0 || (prevDirection != direction)))
        // if ((direction == Vector2.zero || (direction.x == 0 && direction.y != 0)) 
            // && _rb.velocity.y == 0)
        {
            _rb.velocity = new Vector3(_rb.velocity.x * moveDirFalloff, _rb.velocity.y, _rb.velocity.z * moveDirFalloff);
        }

        // If DASH KEY is pressed
        // if (Input.GetKeyDown(InputManager._inst._keyBindings[InputAction.run])
        //     && !iGrounded.isGrounded
        //     && _currDashTime == 0)
        if (Input.GetKeyDown(InputManager._inst._keyBindings[InputAction.run])
            && !iGrounded.isGrounded && currDashCount > 0)
        {
            currDashCount = Mathf.Clamp(currDashCount - 1, 0, numDashes);

            _rb.AddForce(new Vector3(direction.x, direction.y, 0f) * dashStrength, ForceMode.Impulse);
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

            Vector3 forceToAdd = (moveDir * movementSpeed);

            // The maximum speed of the player based on specified movememntSpeed
            float terminalVelocity = ((forceToAdd.magnitude / _rb.drag) - Time.fixedDeltaTime * forceToAdd.magnitude) / _rb.mass;
            
            if (iGrounded.isGrounded)
            {
                _rb.velocity = new Vector3(moveDir.x == 0 ? _rb.velocity.x : moveDir.x * terminalVelocity, 
                                           _rb.velocity.y, 
                                           _rb.velocity.z);
            }
            else
            {
                _rb.velocity = new Vector3(_rb.velocity.x + (moveDir.x * airStrafeSpeed * Time.fixedDeltaTime),
                                           _rb.velocity.y, 
                                        //    _rb.velocity.z);
                                           _rb.velocity.z + (moveDir.z * airStrafeSpeed * Time.fixedDeltaTime));
            }
        }
    }
#endregion

#region Custom_Functions
    private void UpdateDashCounter()
    {
        if (iGrounded.isGrounded && !(_rb.velocity.y > 0))
        {
            currDashCount = numDashes;
        }
    }

    private void ResetDashTimer() => _currDashTime = maxDashTimer;
    private void UpdateDashTimer()
    {
        _currDashTime = Mathf.Clamp((_currDashTime - Time.deltaTime), 0f, maxDashTimer);

        if (_currDashTime == 0f) dashTimerActive = false;
    }

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
        
        // direction = direction.normalized;
    }
#endregion
}
