using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitboxHandler : MonoBehaviour
{
    [SerializeField] private BoxCollider _col = null;

    public void EnableEntityHitbox()
    {
        if (_col != null)
        {
            _col.enabled = true;
        }
    }

    public void DisableEntityHitbox()
    {
        if (_col != null)
        {
            _col.enabled = false;
        }
    }
}
