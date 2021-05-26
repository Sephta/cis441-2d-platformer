using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public enum AttackState
{
    none = 0,
    attacking = 1
}

public class PlayerCombatHandler : MonoBehaviour, ICombatible
{
    [Header("Dependencies")]
    public Rigidbody _rb = null;
    public Camera _cam = null;

    [Header("Player Stats - Temporary")]
    [SerializeField, Range(0f, 1000f)] private int _playerStartingHealth = 100;
    [ReadOnly] public int _currHealth = 1;

    [Header("Attack Data")]
    [ReadOnly] public AttackState _playerAttackState = AttackState.none;
    [Range(0f, 100f)] public float launchPower = 0f;
    [Range(0f, 10f)] public float attackRange = 0f;
    [Range(0f, 1f)] public float attackLerpTime = 0f;

    [Header("Attack Timer")]
    [Range(0f, 5f)] public float maxAtkTimer = 0f;
    [SerializeField, ReadOnly] private bool atkTimerActive = false;
    [SerializeField, ReadOnly] private float _currAttackTime = 0f;

    [SerializeField] private LayerMask _mask;
    private InputManager iManager = null;
    private int entityLayerMask = int.MinValue;

    void Start()
    {
        if (_rb == null && GetComponent<Rigidbody>() != null)
            _rb = GetComponent<Rigidbody>();

        if (iManager == null && InputManager._inst != null)
            iManager = InputManager._inst;
        
        if (entityLayerMask == int.MinValue)
            entityLayerMask = LayerMask.GetMask("Entity");
        
        if (_cam == null)
            _cam = Camera.main;

        _currHealth = _playerStartingHealth;
        // _currAttackTime = maxAtkTimer;
    }

    void Update()
    {
        if (Input.GetKeyDown(iManager._keyBindings[InputAction.attack]))
            OnEntityAttack();
        
        if (atkTimerActive)
            UpdateAttackTimer();
    }

    // void FixedUpdate() {}

    void OnCollisionEnter(Collision other)
    {
        #if UNITY_EDITOR
        Debug.Log("COLLISION ENTER - player");
        #endif

        if (other == null)
            return;

        if (_playerAttackState == AttackState.attacking && other.gameObject != null)
        {
            var entityCombater = other.gameObject.GetComponent<EntityCombatHandler>();
            if (entityCombater is ICombatible)
                entityCombater.OnEntityDefend(this.gameObject);
        }

        if (other.gameObject.layer == entityLayerMask && _playerAttackState == AttackState.none)
        {
            OnEntityDefend();
        }
    }

    // void OnCollisionExit(Collision other) {}

#region Attack_Defend
    public void OnEntityAttack()
    {
        var hits = Physics.SphereCastAll(transform.position, attackRange, transform.up, 0.1f, _mask);

        float currMinDist = float.MaxValue;

        Transform entity = null;

        foreach (var hit in hits)
        {
            Vector3 entityScreenSpace = _cam.WorldToScreenPoint(hit.transform.position);

            float newDist = Vector3.Distance(Input.mousePosition, entityScreenSpace);
            // float newDist = Vector3.Distance(transform.position, hit.transform.position);
            if (newDist < currMinDist)
            {
                entity = hit.transform;
                currMinDist = newDist;
            }
        }

        if (entity != null && _currAttackTime == 0f)
        {
            ResetAttackTimer();
            atkTimerActive = true;

            Vector3 launchDir = (entity.position - transform.position).normalized;

            // _rb.AddForce(launchDir * launchPower, ForceMode.VelocityChange);

            // LeanTween.move(this.gameObject, entity.position, attackLerpTime)

            // LTBezierPath _bPath = new LTBezierPath(new Vector3[]
            // {
            //     this.transform.position,
            //     this.transform.position + new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), 0f),
            //     this.transform.position + new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), 0f),
            //     entity.position
            // });

            LeanTween.move(this.gameObject, entity.position, attackLerpTime)
                .setEase(LeanTweenType.linear)
                .setOnStart(SetAttackStateAttacking)
                .setOnComplete(SetAttackStateNone);
        }
    }

    public void OnEntityDefend()
    {
        _currHealth = Mathf.Clamp((_currHealth - 10), 0, _playerStartingHealth);
    }

    public void OnEntityDefend(GameObject defendFrom) {}
#endregion

    public void SetAttackStateAttacking() => _playerAttackState = AttackState.attacking;
    public void SetAttackStateNone() => _playerAttackState = AttackState.none;

    private void ResetAttackTimer() => _currAttackTime = maxAtkTimer;
    private void UpdateAttackTimer()
    {
        _currAttackTime = Mathf.Clamp((_currAttackTime - Time.deltaTime), 0f, maxAtkTimer);

        if (_currAttackTime == 0f) atkTimerActive = false;
    }
}
