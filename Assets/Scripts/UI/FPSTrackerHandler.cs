using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;
using TMPro;


public class FPSTrackerHandler : MonoBehaviour
{
    [SerializeField] private string _defaultTextContent = "FPS - ";
    [SerializeField] private TextMeshProUGUI _fpsText = null;

    [Header("FPS Data")]
    [SerializeField] private bool useTimeInterval = true;
    [SerializeField, Range(0f, 1f)] private float timeInterval = 0f;
    [SerializeField, ReadOnly] private int avgFPS = 0;
    [SerializeField, ReadOnly] private float currTimeTracked = 0f;
    
    void Start()
    {
        #if UNITY_EDITOR
        if (_fpsText == null)
            Debug.LogWarning("Warning - variable \"_fpsText\" in <" + gameObject.name + "> is null.");
        #endif

        currTimeTracked = timeInterval;
    }

    void Update()
    {
        if (_fpsText != null)
            UpdateFPSText();
    }

    private void UpdateFPSText()
    {
        if (useTimeInterval)
            UpdateTimeTracked();

        avgFPS = (int)(1f / Time.unscaledDeltaTime);

        if (currTimeTracked == 0 && useTimeInterval)
        {
            ResetTimeTracked();
            _fpsText.text = _defaultTextContent + avgFPS.ToString() + 
                            " (" + (Mathf.Floor(Time.deltaTime * 1000f)).ToString() + " ms)";
        }
        else if (!useTimeInterval)
            _fpsText.text = _defaultTextContent + avgFPS.ToString() + 
                            " (" + (Mathf.Floor(Time.deltaTime * 1000f)).ToString() + " ms)";
    }

    private void UpdateTimeTracked()
    {
        currTimeTracked = Mathf.Clamp(currTimeTracked - Time.deltaTime, 0f, timeInterval);
    }

    private void ResetTimeTracked() => currTimeTracked = timeInterval;
}
