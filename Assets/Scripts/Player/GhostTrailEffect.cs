using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;


public class GhostTrailEffect : MonoBehaviour
{
    [Space]
    [Header("Dependencies")]
    [SerializeField] private GameObject ghost = null;
    [SerializeField, Required] private Transform ghostParent = null;
    [SerializeField, Required] private SpriteRenderer _sr = null;
    [SerializeField, Required] private PlayerMovement _pm = null;

    [Space]
    [Header("Configurable Data")]
    [SerializeField, ReadOnly] private bool timerActive = false;
    [SerializeField, Range(0f, 5f)] private float destroyDelay = 1f;
    [SerializeField, Range(0f, 1f)] private float delayDuration = 0f;
    [SerializeField, ReadOnly] private float currentDelayTime = 0f;

    void Start()
    {
        ResetDelayTime();
    }

    void Update()
    {
        if (_pm.isDashing)
        {
            if (timerActive)
                UpdateDelayTime();
            else
            {
                SpawnGhost();
                ResetDelayTime();
            }
        }
    }

    public void ResetDelayTime()
    {
        currentDelayTime = (delayDuration / 10);
        timerActive = true;
    }
    private void UpdateDelayTime()
    {
        currentDelayTime = Mathf.Clamp(currentDelayTime - Time.deltaTime, 0f, (delayDuration / 10));

        if (currentDelayTime <= 0)
            timerActive = false;
    }

    private void SpawnGhost()
    {
        var newGhost = Instantiate(ghost, transform.position, Quaternion.identity, ghostParent);

        SpriteRenderer newGhostSR = newGhost.GetComponent<SpriteRenderer>();
        newGhostSR.sprite = _sr.sprite;
        newGhostSR.flipX = _sr.flipX;

        Destroy(newGhost, destroyDelay);
    }

}

