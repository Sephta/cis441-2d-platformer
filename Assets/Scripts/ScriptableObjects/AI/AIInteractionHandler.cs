using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using NaughtyAttributes;


public class AIInteractionHandler : MonoBehaviour
{
    [SerializeField, Tag] private string tagToDetect;
    [SerializeField] private float hitBackForce = 0f;
    [SerializeField] private float hitUpForce = 0f;
    [SerializeField, ReadOnly] EntityStatusHandler entityStatus = null;

    [Space(25)]
    public UnityEvent OnEntityHit;
    // [SerializeField] private AIBehaviorManager _aiManager = null;

    [ReadOnly] public Transform hitByTransform = null;


    void Awake()
    {
        if (entityStatus == null)
            entityStatus = GetComponent<EntityStatusHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(tagToDetect))
        {
            #if UNITY_EDITOR
            Debug.Log("[Entity] hit: " + other.gameObject.name);
            #endif
            
            PlayerInteractionHandler interactionHandler = other.gameObject.GetComponentInChildren<PlayerInteractionHandler>();
            interactionHandler.OnPlayerHit?.Invoke();
            hitByTransform = null;
        }
    }

    public void HandleEntityHitEvent()
    {
        #if UNITY_EDITOR
        Debug.Log("[Entity] was hit.");
        #endif

        Rigidbody _parentRB = GetComponentInParent<Rigidbody>();

        Vector3 direction = (hitByTransform != null) ? 
            (this.transform.parent.position - hitByTransform.position) : Vector3.zero; // -_parentRB.velocity.normalized;

        _parentRB.AddForce(direction * hitBackForce, ForceMode.VelocityChange);
        _parentRB.AddForce(Vector3.up * hitUpForce, ForceMode.VelocityChange);

        if (entityStatus != null)
            entityStatus.UpdateHealth(10);
    }
}
