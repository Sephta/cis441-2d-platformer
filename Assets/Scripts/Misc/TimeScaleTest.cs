using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class TimeScaleTest : MonoBehaviour
{
    [SerializeField] float defaultTimeScale = 0f;
    [SerializeField, ReadOnly] bool toggleTimeScale = false;
    
    private InputManager iManager = null;

    // Start is called before the first frame update
    void Start()
    {
        if (InputManager._inst != null)
            iManager = InputManager._inst;

        defaultTimeScale = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(iManager._keyBindings[InputAction.pause]))
        {
            toggleTimeScale = !toggleTimeScale;
            Time.timeScale = (toggleTimeScale) ? defaultTimeScale : 0f;
        }
    }
}
