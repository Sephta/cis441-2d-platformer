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
    [Range(0f, 10f)] public float gravityMultiplier = 0f;
    public Vector3 gravityDefault = new Vector3(0f, -9.81f, 0f);
    [Range(0f, 100f)] public float movementSpeed = 0f;
    [Range(0f, 100f)] public float airStrafeSpeed = 0f;
    [ReadOnly] public Vector3 moveDir = Vector3.zero;
    [Range(0f, 1f)] public float moveDirFalloff = 0.5f;
    public bool isDashing = false;
    [Range(0f, 100f)] public float dashStrength = 0f;
    [Range(0f, 10f)] public float dashMaxDistance = 0f;

    [Header("Dash Counter")]
    [Range(0f, 5f)] public int numDashes = 0;
    [SerializeField, ReadOnly] private int currDashCount = 0;

    [Header("Dash Timer")]
    [Range(0f, 5f)] public float maxDashTimer = 0f;
    [SerializeField, ReadOnly] private bool dashTimerActive = false;
    [SerializeField, ReadOnly] private float _currDashTime = 0f;

    [Header("Debug Data")]
    // PRIVATE VARS
    [SerializeField, ReadOnly] private Vector3 cachedVelocity = Vector3.zero;
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
        
        if ((direction.x == 0 || (prevDirection != direction)) && _rb.velocity.y == 0)
        // if ((direction == Vector2.zero || (direction.x == 0 && direction.y != 0)) 
            // && _rb.velocity.y == 0)
        {
            _rb.velocity = new Vector3(_rb.velocity.x * moveDirFalloff, _rb.velocity.y, _rb.velocity.z * moveDirFalloff);
        }

        // If DASH KEY is pressed
        if (Input.GetKeyDown(InputManager._inst._keyBindings[InputAction.run])
            && !iGrounded.isGrounded && currDashCount > 0 && !isDashing && direction != Vector2.zero)
        {
            isDashing = true;

            PlayerAnimationController.AnimatorDashEvent?.Invoke();

            ResetDashTimer();
            currDashCount = Mathf.Clamp(currDashCount - 1, 0, numDashes);

            // Cache players current velocity
            cachedVelocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

            // _rb.AddForce(new Vector3(direction.x, direction.y, 0f) * dashStrength, ForceMode.Impulse);
            Vector2 dashDir = direction.normalized;
            _rb.velocity = new Vector3(
                dashDir.x * dashStrength,
                dashDir.y * dashStrength,
                _rb.velocity.z
            );
        }

        if (_currDashTime == 0) isDashing = false;
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
                                           _rb.velocity.z + (moveDir.z * airStrafeSpeed * Time.fixedDeltaTime));
            }
        }

        // Applies constant gravity to the player (Custom gravity values to help jump feel weightier)
        if (!isDashing)
            _rb.velocity += (_rb.velocity.y < 0) ? gravityDefault * gravityMultiplier * Time.fixedDeltaTime : gravityDefault * Time.fixedDeltaTime;
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

    private void ResetDashTimer()
    {
        _currDashTime = maxDashTimer;
        dashTimerActive = true;
    }

    private void UpdateDashTimer()
    {
        _currDashTime = Mathf.Clamp((_currDashTime - Time.deltaTime), 0f, maxDashTimer);

        if (_currDashTime == 0f)
        {
            dashTimerActive = false;
            // _rb.velocity = cachedVelocity;
            _rb.velocity = new Vector3(
                direction.x * movementSpeed,
                0f,
                direction.y * movementSpeed
            );
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
        
        // direction = direction.normalized;
    }
#endregion
}
