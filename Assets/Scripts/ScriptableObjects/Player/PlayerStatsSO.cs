using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerStats", fileName = "PlayerStatsObject", order = 1)]
public class PlayerStatsSO : ScriptableObject
{
    #if UNITY_EDITOR
    #pragma warning disable 0414
    [Header("Toggle Visible Data")]
    [SerializeField] private bool showAll = false;
    [SerializeField] private bool showMovementData = false;
    [SerializeField] private bool showDashData = false;
    [SerializeField] private bool showJumpData = false;
    #pragma warning restore 0414
    #endif

    [Space]
    [Header("Movement Stats")]
    [ShowIf(EConditionOperator.Or, "showMovementData", "showAll")]
    [SerializeField, Range(0f, 100f)] private float _movementSpeed = 0f;
    [ShowIf(EConditionOperator.Or, "showMovementData", "showAll")]
    [SerializeField, Range(0f, 100f)] private float _airStrafeSpeed = 0f;
    [ShowIf(EConditionOperator.Or, "showMovementData", "showAll")]
    [SerializeField, Range(0f, 1f)]   private float _moveDirFalloff = 0f;

    [Space]
    [Header("Dash Stats")]
    [ShowIf(EConditionOperator.Or, "showDashData", "showAll")]
    [SerializeField, Range(0f, 10f)]  private int _numDashes = 0;
    [ShowIf(EConditionOperator.Or, "showDashData", "showAll")]
    [SerializeField, Range(0f, 100f)] private float _dashStrength = 0f;
    [ShowIf(EConditionOperator.Or, "showDashData", "showAll")]
    [SerializeField, Range(0f, 2f)]   private float _maxDashTimer = 0f;

    [Space]
    [Header("Jump Stats")]
    [ShowIf(EConditionOperator.Or, "showJumpData", "showAll")]
    [SerializeField, Range(0f, 10f)]   private int _numJumps = 2;
    [ShowIf(EConditionOperator.Or, "showJumpData", "showAll")]
    [SerializeField, Range(0f, 1000f)] private float _jumpForce = 0f;
    [ShowIf(EConditionOperator.Or, "showJumpData", "showAll")]
    [SerializeField, Range(0f, 1000f)] private float _jumpForwardForce = 0f;
    [ShowIf(EConditionOperator.Or, "showJumpData", "showAll")]
    [SerializeField, Range(0f, 1f)]    private float _jumpTapFalloff = 0.5f;
    [ShowIf(EConditionOperator.Or, "showJumpData", "showAll")]
    [SerializeField, Range(0f, 1f)]    private float _jumpBufferTime  = 0f;


    /* ------------------------------------------------------------------- */
    /* --------------------------- PUBLIC GETTERS ------------------------ */
    /* ------------------------------------------------------------------- */
    
    // MOVEMENT
    public float MovementSpeed => _movementSpeed;
    public float AirStrafeSpeed => _airStrafeSpeed;
    public float MoveDirFalloff => _moveDirFalloff;

    // DASH
    public int NumDashes => _numDashes;
    public float DashStrength => _dashStrength;
    public float MaxDashTimer => _maxDashTimer;

    // JUMP
    public int NumJumps => _numJumps;
    public float JumpForce => _jumpForce;
    public float JumpForwardForce => _jumpForwardForce;
    public float JumpTapFalloff => _jumpTapFalloff;
    public float JumpBufferTime => _jumpBufferTime;
}
