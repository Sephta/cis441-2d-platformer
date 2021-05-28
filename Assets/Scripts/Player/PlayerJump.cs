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

    [Header("Jump Data")]
    [Range(0f, 10f)] public int numJumps = 2;
    [Range(0f, 1000f)] public float jumpForce = 0f;
    [Range(0f, 100f)] public float jumpForwardForce = 0f;
    [Range(0f, 1f)] public float jumpTapFalloff = 0.5f;
    [Range(0f, 10f)] public float gravityMultiplier = 0f;
    public Vector3 gravityDefault = new Vector3(0f, -9.81f, 0f);
    [Range(0f, 1f)] public float hangTime = 0f;
    [Range(0f, 1f)] public float jumpBufferTime = 0f;

    // PRIVATE VARS
    private InputManager iManager = null;
    private StaticGroundedManager iGrounded = null;
    
    [Header("Debug Data")]
    [SerializeField, ReadOnly] private bool jumpBufferActive = false;
    [SerializeField, ReadOnly] private float currJumpBufferCount = 0;
    [SerializeField, ReadOnly] private int currJumpCount = 0;
    [SerializeField, ReadOnly] private float currHangTime = 0f;
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

        currJumpCount = numJumps;
        currHangTime = hangTime;
    }

    void Update()
    {
        GetDirectionVector();
        UpdateHangTime();

        if (iManager != null && iGrounded != null)
        {
            UpdateJumpBuffer();
            UpdateJumpCounter();

            // if ((currJumpBufferCount > 0) 
            //     && (iGrounded.isGrounded || currJumpCount > 0))
            if (Input.GetKeyDown(iManager._keyBindings[InputAction.jump]) 
                && (iGrounded.isGrounded || currJumpCount > 0))
            {
                currJumpCount = Mathf.Clamp((currJumpCount - 1), 0, numJumps);

                _rb.velocity = new Vector3(_rb.velocity.x, jumpForce * Time.fixedDeltaTime, _rb.velocity.z);

                if (_pm != null)
                {
                    Vector3 moveDir = _pm.lockZAxis ? new Vector3(direction.x, 0f, 0f) : new Vector3(direction.x, 0f, direction.y);
                    _rb.AddForce(moveDir * jumpForwardForce, ForceMode.Force);
                }
            }
            
            if (Input.GetKeyUp(iManager._keyBindings[InputAction.jump]) && _rb.velocity.y > 0)
            {
                _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y * jumpTapFalloff, _rb.velocity.z);
            }

            // Vector3 gravity = (_rb.velocity.y < 0) ? gravityDefault * gravityMultiplier : gravityDefault;
            _rb.AddForce((_rb.velocity.y < 0) ? gravityDefault * gravityMultiplier : gravityDefault, ForceMode.Force);
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

    private void ResetJumpBufferTimer() => currJumpBufferCount = jumpBufferTime;
    private void UpdateJumpBufferTimer()
    {
        currJumpBufferCount = Mathf.Clamp((currJumpBufferCount - Time.deltaTime), 0f, jumpBufferTime);

        if (currJumpBufferCount == 0f) jumpBufferActive = false;
    }

    private void UpdateJumpCounter()
    {
        if (iGrounded.isGrounded && !(_rb.velocity.y > 0))
        {
            currJumpCount = numJumps;
        }
    }

    private void UpdateHangTime()
    {    
        if (iGrounded.isGrounded) currHangTime = hangTime;
        else currHangTime = Mathf.Clamp((currHangTime - Time.deltaTime), 0f, hangTime);
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
