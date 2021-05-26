using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCombatHandler : MonoBehaviour, ICombatible
{
    [Header("Dependencies")]
    public Rigidbody _rb = null;

    [Header("Defend Data")]
    [Range(0f, 100f)] public float hitForce = 0f;

    void Start()
    {
        if (_rb == null && GetComponent<Rigidbody>() != null)
            _rb = GetComponent<Rigidbody>();
    }

    // void Update() {}
    // void FixedUpdate() {}

#region Attack_Defend
    public void OnEntityAttack()
    {
        
    }

    public void OnEntityDefend() {}

    public void OnEntityDefend(GameObject defendFrom)
    {
        if (defendFrom.tag == "Player")
        {
            _rb.AddForce((this.transform.position - defendFrom.transform.position) * hitForce, ForceMode.Impulse);
        }
    }
#endregion
}
