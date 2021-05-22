using System.Collections.Generic;
using UnityEngine;


#region Input Action
// Instead of strings, the keybindings will use a public enum to describe
// each input action in both "written" and numeric form
public enum InputAction
{
    moveUp = 0,
    moveDown = 1,
    moveLeft = 2,
    moveRight = 3,
    jump = 4,
    attack = 5,
    nextItem = 6,
    prevItem = 7
}

#endregion

#region Keybind Immutable Container
// This is a hack-y sort of way to make it simple to manually input defined keybindings
// from the inspector. This struct is an object that simply stores an InputAction and a
// corresponding KeyCode for the action to be bound to.
[System.Serializable] // This allows the struct to be serializable/visible within the Unity inspector window
public struct Keybind
{
    public InputAction action;
    public KeyCode key;
}
#endregion


public class InputManager : MonoBehaviour
{

#region Singleton Instance Member
    [Tooltip("This singleton reference for the input manager.")]
    public static InputManager _inst;
#endregion

    [Header("Keybind Data")]
    
    [Tooltip("Lets the InputManager know if you want to manually initialize new actions into the keyBindings dict.")]
    public bool manualInput = false;
    
    [Tooltip("Number of actions.")]
    public int numActions = 0;

    [Tooltip("Manually Input Keybindings here.")]
    [SerializeField] public List<Keybind> _keyBindings_test = new List<Keybind>();

    // This dictionary is where the actual bindings should be accessed
    public Dictionary<InputAction, KeyCode> _keyBindings = new Dictionary<InputAction, KeyCode>();

#region Unity Functions
    void Awake()
    {
        // Confirm singleton instance is active
        if (_inst == null)
        {
            _inst = this;
            // DontDestroyOnLoad(this);
        }
        else if (_inst != this)
            GameObject.Destroy(this.gameObject);
    }

    void Start()
    {
        // manually feed each input into the dict
        if (manualInput == true)
        {
            foreach (Keybind input in _keyBindings_test)
                _keyBindings.Add(input.action, input.key);
        }
    }

    // void Update() {}
    // void FixedUpdate() {}
#endregion
}