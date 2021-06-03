using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;


public class RotateEffect : MonoBehaviour
{
    [Space]
    [Header("Configurable Data")]
    [SerializeField] private float rotationTime = 0f;
    [SerializeField] private Vector3 rotationAxis = Vector3.zero;
    [SerializeField] private float rotationAmount = 0f;
    [SerializeField] private LeanTweenType easeType;
    [SerializeField, ReadOnly] private bool isLeanActive = false;


    private void Update()
    {
        if (!isLeanActive)
            // LeanTween.rotateY(this.gameObject, rotationAmount, rotationTime)
            //     .setEase(easeType)
            //     .setOnStart(ActivateLean)
            //     .setOnComplete(DeactivateLean);
        
            LeanTween.rotateAround(this.gameObject, rotationAxis, rotationAmount, rotationTime)
                .setEase(easeType)
                .setOnStart(ActivateLean)
                .setOnComplete(DeactivateLean);
    }

    private void ActivateLean() => isLeanActive = true;
    private void DeactivateLean() => isLeanActive = false;
}
