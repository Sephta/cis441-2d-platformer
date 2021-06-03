using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

using NaughtyAttributes;


public class CinemachineCamShake : MonoBehaviour
{
    [Header("Configurable Data")]
    [SerializeField] private CinemachineVirtualCamera _cvc = null;
    
    [SerializeField] private float shakeIntensity = 0f;
    [SerializeField] private float shakeTimer = 0f;
    [SerializeField, ReadOnly] private float currShakeTime = 0f; 

    private void Awake()
    {
        if (_cvc == null && GetComponent<CinemachineVirtualCamera>() != null)
            _cvc = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (currShakeTime >= 0)
        {
            currShakeTime = Mathf.Clamp(currShakeTime - Time.deltaTime, 0f, shakeTimer);

            if (currShakeTime <= 0f)
            {
                CinemachineBasicMultiChannelPerlin mChannelPerlin = 
                    _cvc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
                mChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }

    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin mChannelPerlin = 
            _cvc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
        mChannelPerlin.m_AmplitudeGain = shakeIntensity;
        currShakeTime = shakeTimer;
    }
}
