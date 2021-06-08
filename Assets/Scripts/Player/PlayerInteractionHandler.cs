using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using NaughtyAttributes;


public class PlayerInteractionHandler : MonoBehaviour
{
    [SerializeField, Tag] private string tagToDetect;
    [SerializeField] private Animator _playerAnimator = null;
    [SerializeField] private CinemachineCamShake cinemachineCamShake = null;
    [SerializeField, ReadOnly] PlayerStatusHandler playerStatus = null;

    [ReadOnly] public static bool isPlayerHurting = false;
    
    [Space(25)]
    public UnityEvent OnPlayerHit;
    // [SerializeField] private AIBehaviorManager _aiManager = null;

    void Awake()
    {
        if (playerStatus == null)
            playerStatus = GetComponentInParent<PlayerStatusHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(tagToDetect))
        {
            #if UNITY_EDITOR
            Debug.Log("[Player] hit: " + other.gameObject.name);
            #endif

            if (cinemachineCamShake != null)
                cinemachineCamShake.ShakeCamera(6f, 0.1f);

            AIInteractionHandler interactionHandler = other.gameObject.GetComponentInChildren<AIInteractionHandler>();
            interactionHandler.hitByTransform = this.transform.parent;
            interactionHandler.OnEntityHit?.Invoke();
        }
    }

    public void HandlePlayerHitEvent()
    {
        #if UNITY_EDITOR
        Debug.Log("[Player] was hit.");
        #endif

        if (_playerAnimator != null)
        {
            _playerAnimator.SetTrigger("OnPlayerHurt");
        }

        if (playerStatus != null)
            playerStatus.UpdateHealth(10);
    }
}
