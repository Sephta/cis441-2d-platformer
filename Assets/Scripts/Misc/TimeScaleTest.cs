using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class TimeScaleTest : MonoBehaviour
{    
    private InputManager iManager = null;

    // Start is called before the first frame update
    void Start()
    {
        if (InputManager._inst != null)
            iManager = InputManager._inst;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(iManager._keyBindings[InputAction.pause]))
            PauseMenuHandler.TogglePausedGameState?.Invoke();
    }
}
