using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    // PUBLIC VARS
    [Header("Dependencies")]
    public Rigidbody _rb = null;
    public PlayerMovement _pm = null;
    
    [Space]
    [Expandable, SerializeField] private PlayerStatsSO _ps = null;

    // PRIVATE VARS
    private InputManager iManager = null;
    private StaticGroundedManager iGrounded = null;
    
    [Header("Debug Data")]
    [SerializeField, ReadOnly] private bool jumpBufferActive = false;
    [SerializeField, ReadOnly] private float currJumpBufferCount = 0;
    [SerializeField, ReadOnly] private int currJumpCount = 0;
    [SerializeField, ReadOnly] private Vector2 direction = Vector2.zero;
    [SerializeField, ReadOnly] private Vector2 prevDirection = Vector2.zero;

    void Awake()
    {
        if (GetComponent<Rigidbody>() != null)
            _rb = GetComponent<Rigidbody>();
        if (GetComponent<PlayerMovement>() != null)
            _pm = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        if (iManager == null && InputManager._inst != null)
            iManager = InputManager._inst;
        
        if (iGrounded == null && StaticGroundedManager._inst != null)
            iGrounded = StaticGroundedManager._inst;

        currJumpCount = _ps.NumJumps;
    }

    void Update()
    {
        GetDirectionVector();

        if (iManager != null && iGrounded != null)
        {
            UpdateJumpBuffer();
            UpdateJumpCounter();

            // if ((currJumpBufferCount > 0) 
            //     && (iGrounded.isGrounded || currJumpCount > 0))
            if (Input.GetKeyDown(iManager._keyBindings[InputAction.jump]) 
                && (iGrounded.isGrounded || currJumpCount > 0))
            {
                PlayerAnimationController.AnimatorJumpEvent?.Invoke();

                currJumpCount = Mathf.Clamp((currJumpCount - 1), 0, _ps.NumJumps);

                // Applies upward force 
                _rb.velocity = new Vector3(_rb.velocity.x, _ps.JumpForce * Time.fixedDeltaTime, _rb.velocity.z);

                // Adds force to the jump based on the direction of movement
                if (_pm != null && (_ps.NumJumps >= currJumpCount) && (currJumpCount >= (_ps.NumJumps - 1)))
                {
                    Vector3 moveDir = _pm.lockZAxis ? new Vector3(direction.x, 0f, 0f) : new Vector3(direction.x, 0f, direction.y);
                    _rb.AddForce(moveDir * _ps.JumpForwardForce, ForceMode.Force);
                }
            }
        }
    }

    // void FixedUpdate() {}

#region Custom_Functions
    private void UpdateJumpBuffer()
    {
        if (Input.GetKeyDown(iManager._keyBindings[InputAction.jump]))
        {
            ResetJumpBufferTimer();
            jumpBufferActive = true;
        }
        else if (jumpBufferActive)
            UpdateJumpBufferTimer();
    }

    private void ResetJumpBufferTimer() => currJumpBufferCount = _ps.JumpBufferTime;
    private void UpdateJumpBufferTimer()
    {
        currJumpBufferCount = Mathf.Clamp((currJumpBufferCount - Time.deltaTime), 0f, _ps.JumpBufferTime);

        if (currJumpBufferCount == 0f) jumpBufferActive = false;
    }

    private void UpdateJumpCounter()
    {
        if (iGrounded.isGrounded && !(_rb.velocity.y > 0))
        {
            currJumpCount = _ps.NumJumps;
        }
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
        
        direction = direction.normalized;
    }
#endregion
}
