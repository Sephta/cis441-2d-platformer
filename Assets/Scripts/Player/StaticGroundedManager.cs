using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class StaticGroundedManager : MonoBehaviour
{
    // STATIC INSTANCE
    public static StaticGroundedManager _inst = null;

    [Header("Grounded Data")]
    [ReadOnly] public bool isGrounded = false;
    [Space, SerializeField] private bool _debug = false;
    [SerializeField] private LayerMask _mask;

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

    private void OnTriggerStay(Collider other)
    {
        if (_debug) Debug.Log("TriggerStay");

        isGrounded = other != null && (((1 << other.gameObject.layer) & _mask) != 0);
    }

    private void OnTriggerExit(Collider other)
    {
        if (_debug) Debug.Log("TriggerExit");

        isGrounded = false;
    }

    // void Start() {}
    // void Update() {}
    // void FixedUpdate() {}
}
