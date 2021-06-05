using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;


public class ComboManager : MonoBehaviour
{
    public enum AttackNames
    {
        NullTrigger = 0,
        Attack01    = 1,
        Attack02    = 2,
        Attack03    = 3,
    }


    [Header("Combo Data")]

    [ReadOnly] public bool canRecieveInput = true;
    [ReadOnly] public bool inputRecieved = false;

    [ReadOnly] public bool isAttacking = false;

    [Header("Instance Data")]
    
    public static ComboManager _inst;

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

        // ToggleCanRecieveInput();
    }

    void Update()
    {
        if(_inst != null && InputManager._inst != null)
            _inst.Attack(Input.GetKeyDown(InputManager._inst._keyBindings[InputAction.attack]));
    }

    public void Attack(bool input)
    {
        if (input)
        {
            if (canRecieveInput)
            {
                inputRecieved = true;
                canRecieveInput = false;
            }
            else
            {
                return;
            }
        }
    }

    public void ToggleCanRecieveInput()
    {
        canRecieveInput = !canRecieveInput;
    }
}
