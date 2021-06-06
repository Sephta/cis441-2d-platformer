using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using NaughtyAttributes;


public class PlayerAnimationController : MonoBehaviour
{
    public static UnityAction AnimatorJumpEvent;
    public static UnityAction AnimatorDashEvent;

    private StaticGroundedManager iGrounded = null;
    private ComboManager iCombo = null;

    [Header("Dependencies")]
    [SerializeField] private PlayerMovement _pm = null;
    [SerializeField] private PlayerJump _pj = null;
    [SerializeField] private Animator _anim = null;

    [Header("Tweakable Data")]
    [SerializeField, Range(0f, 1f)] private float movementEpsilon = 0f;

    [SerializeField, ReadOnly] public Vector2 direction = Vector2.zero;
    [SerializeField, ReadOnly] private Vector2 prevDirection = Vector2.zero;

    private void Awake()
    {
        if (_anim == null && GetComponent<Animator>() != null)
            _anim = GetComponent<Animator>();
        
        if (_pm == null) _pm = GetComponentInParent<PlayerMovement>();
        if (_pj == null) _pj = GetComponentInParent<PlayerJump>();
    }

    private void Start()
    {
        if (StaticGroundedManager._inst != null)
            iGrounded = StaticGroundedManager._inst;
        
        if (ComboManager._inst != null)
            iCombo = ComboManager._inst;
    }

    private void OnEnable()
    {
        AnimatorJumpEvent += TriggerJumpEvent;
        AnimatorDashEvent += TriggerDashEvent;
    }
    
    private void OnDisable()
    {
        AnimatorJumpEvent -= TriggerJumpEvent;
        AnimatorDashEvent -= TriggerDashEvent;
    }

    private void Update()
    {
        UpdateDirectionVector();
        ChangeAnimationState();
    }

    private void ChangeAnimationState()
    {
        iCombo.canRecieveInput = iGrounded.isGrounded;

        _anim.SetFloat("playerDirectionX", direction.x);
        _anim.SetFloat("playerDirectionY", direction.y);

        _anim.SetBool("isGrounded", iGrounded.isGrounded);

        _anim.SetFloat("playerVelocityY", _pm._rb.velocity.y);

        _anim.SetBool("isMoving", 
            (-movementEpsilon <= _pm._rb.velocity.x && _pm._rb.velocity.x <= movementEpsilon) ? false : true);
        
        _anim.SetBool("isDashing", _pm.isDashing);
    }

    private void TriggerJumpEvent()
    {
        _anim.SetBool("isDashing", false);
        _anim.SetBool("isJumping", true);
    }

    private void TriggerDashEvent()
    {
        _anim.SetBool("isJumping", false);
        _anim.SetBool("isDashing", true);
    }

    public void OnJumpAnimationCompleted() => _anim.SetBool("isJumping", false);
    public void OnDashAnimationCompleted() => _anim.SetBool("isDashing", false);

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
