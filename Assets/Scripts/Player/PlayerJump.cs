using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    // PUBLIC VARS
    [Header("Dependencies")]
    public Rigidbody _rb = null;

    [Header("Jump Data")]
    [Range(0f, 1000f)] public float jumpForce = 0f;

    // PRIVATE VARS
    private InputManager iManager = null;

    void Awake()
    {
        if (GetComponent<Rigidbody>() != null)
            _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        if (iManager == null && InputManager._inst != null)
            iManager = InputManager._inst;
    }

    void Update()
    {
        if (iManager != null)
        {
            if (Input.GetKeyDown(iManager._keyBindings[InputAction.jump]))
            {
                _rb.AddForce(((transform.up * jumpForce) + _rb.velocity) - _rb.velocity, ForceMode.Impulse);
            }
        }
    }
    // void FixedUpdate() {}
}
