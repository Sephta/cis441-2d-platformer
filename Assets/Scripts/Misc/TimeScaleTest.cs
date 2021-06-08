using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class TimeScaleTest : MonoBehaviour
{    
    private InputManager iManager = null;

    [SerializeField, ReadOnly] private DeathScreenHandler endScreen;

    // Start is called before the first frame update
    void Start()
    {
        if (InputManager._inst != null)
            iManager = InputManager._inst;

        if (DeathScreenHandler._inst != null)
            endScreen = DeathScreenHandler._inst;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(iManager._keyBindings[InputAction.pause]) && !endScreen.isEndScreenActive)
            PauseMenuHandler.TogglePausedGameState?.Invoke();
    }
}
