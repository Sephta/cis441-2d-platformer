using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;


public class BobUpDownEffect : MonoBehaviour
{
    [Space]
    [Header("Configurable Data")]
    [SerializeField] private float timeInSec = 0f;
    [SerializeField] private float desiredY = 0f;
    [SerializeField] private LeanTweenType easeTypeUp;
    [SerializeField] private LeanTweenType easeTypeDown;

    [SerializeField, ReadOnly] private bool active = false;
    [SerializeField, ReadOnly] private bool state = false;


    private void Update()
    {
        if (!active)
        {
            if (state)
                LeanTween.moveY(gameObject, transform.position.y + desiredY, timeInSec)
                    .setEase(easeTypeUp)
                    .setOnStart(ChangeIsLeanActive)
                    .setOnComplete(SwapState);
            else
                LeanTween.moveY(gameObject, transform.position.y - desiredY, timeInSec)
                    .setEase(easeTypeUp)
                    .setOnStart(ChangeIsLeanActive)
                    .setOnComplete(SwapState);
        }
    }

    private void ChangeIsLeanActive() => active = true;

    private void SwapState()
    {
        state = !state;
        active = false;
    }
}
