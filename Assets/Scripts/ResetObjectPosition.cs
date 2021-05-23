using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObjectPosition : MonoBehaviour
{
    public Vector3 position = Vector3.zero;

    void OnTriggerEnter(Collider collider)
    {
        collider.gameObject.transform.position = position;
    }
}
