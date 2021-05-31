using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FlipPlayerSprite : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private SpriteRenderer _sr = null;
    [SerializeField] private Rigidbody _parentRB = null;

    private void Awake()
    {
        if (_sr == null && GetComponent<SpriteRenderer>() != null)
            _sr = GetComponent<SpriteRenderer>();
        
        if (_parentRB == null)
            _parentRB = GetComponentInParent<Rigidbody>();
    }

    private void Update()
    {    
        FlipSprite();
    }

    private void FlipSprite()
    {
        if (_parentRB == null)
            return;
        
        if (_parentRB.velocity.x > 0)
            _sr.flipX = false;
        else if  (_parentRB.velocity.x < 0)
            _sr.flipX = true;
    }
}
