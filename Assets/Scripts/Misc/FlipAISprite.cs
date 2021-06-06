using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipAISprite : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private SpriteRenderer _sr = null;
    [SerializeField] private Rigidbody _rb = null;

    private void Awake()
    {
        if (_sr == null && GetComponent<SpriteRenderer>() != null)
            _sr = GetComponent<SpriteRenderer>();
        
        if (_rb == null)
            _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {    
        FlipSprite();
    }

    private void FlipSprite()
    {
        if (_rb == null)
            return;
        
        if (_rb.velocity.x < 0)
            _sr.flipX = false;
        else if  (_rb.velocity.x > 0)
            _sr.flipX = true;
    }
}
