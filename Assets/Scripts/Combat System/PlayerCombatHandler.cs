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
    public CapsuleCollider _col = null;
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

    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _entityMask;
    private InputManager iManager = null;
    private int entityLayerMask = int.MinValue;

    void Awake()
    {
        if (_rb == null && GetComponent<Rigidbody>() != null)
            _rb = GetComponent<Rigidbody>();
        
        if (_col == null && GetComponent<CapsuleCollider>() != null)
            _col = GetComponent<CapsuleCollider>();

        if (_cam == null)
            _cam = Camera.main;
    }

    void Start()
    {
        if (iManager == null && InputManager._inst != null)
            iManager = InputManager._inst;

        if (entityLayerMask == int.MinValue)
            entityLayerMask = LayerMask.GetMask("Entity");

        _currHealth = _playerStartingHealth;
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
        // #if UNITY_EDITOR
        // Debug.Log("COLLISION ENTER - player");
        // #endif

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
        var hits = Physics.SphereCastAll(transform.position, attackRange, transform.up, 0.1f, _entityMask);

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

            Vector3 worldSpaceColLocation = transform.TransformPoint(_col.center);

            var capsuleHits = Physics.CapsuleCastAll(worldSpaceColLocation + new Vector3(0f, _col.height * 0.5f, 0f),
                                                     worldSpaceColLocation, 
                                                     _col.radius,
                                                     (entity.position - transform.position),
                                                     Vector3.Distance(transform.position, entity.position), _groundMask);
            
            bool shouldLean = true;

            foreach (var capHit in capsuleHits)
            {
                // #if UNITY_EDITOR
                // Debug.Log("Hit: " + capHit.collider.gameObject.name);
                // #endif

                // LayerMask.Equals(capHit.collider.gameObject, _groundMask);

                if (capHit.collider.CompareTag("Ground"))
                {
                    // #if UNITY_EDITOR
                    // Debug.Log("I Should Return now... ");
                    // #endif
                    shouldLean = false;
                }
            }


            if (shouldLean)
            {
                #if UNITY_EDITOR
                Debug.Log("Lean... ");
                #endif

                LeanTween.move(this.gameObject, 
                           entity.position - (Vector3.one * entity.GetComponent<SphereCollider>().radius), 
                           attackLerpTime)
                .setEase(LeanTweenType.linear)
                .setOnStart(SetAttackStateAttacking)
                .setOnComplete(SetAttackStateNone);
            }
            else
            {
                #if UNITY_EDITOR
                Debug.Log("No Lean... ");
                #endif
                _rb.AddForce(launchDir * launchPower, ForceMode.Impulse);
            }
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
