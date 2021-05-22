using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Dependencies")]
    public Rigidbody _rb = null;

    [Header("Movement Data")]
    [Range(0f, 100f)] public float movementSpeed = 0f;

    void Awake()
    {
        if (GetComponent<Rigidbody>() != null)
            _rb = GetComponent<Rigidbody>();
    }

    // void Start() {}
    // void Update() {}
    // void FixedUpdate() {}
}
