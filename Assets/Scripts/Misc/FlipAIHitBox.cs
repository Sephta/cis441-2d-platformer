using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipAIHitBox : MonoBehaviour
{
    [Header("Configurable Data")]
    [SerializeField] private Rigidbody _rb = null;
    [SerializeField] private BoxCollider _col = null;
    [SerializeField] private Vector3 colliderPos = Vector3.zero;

    // void Start() {}

    void Update()
    {
        FlipHitbox();
    }

    private void FlipHitbox()
    {
        if (_rb == null)
            return;
        
        if (_rb.velocity.x < 0)
            _col.center = new Vector3(
                -colliderPos.x,
                colliderPos.y,
                colliderPos.z
            );
        else if  (_rb.velocity.x > 0)
            _col.center = colliderPos;
    }
}
