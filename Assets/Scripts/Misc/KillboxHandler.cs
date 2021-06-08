using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;


public class KillboxHandler : MonoBehaviour
{
    [SerializeField, Tag] private string tagToDetect;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == tagToDetect)
        {
            PlayerStatusHandler status = other.gameObject.GetComponent<PlayerStatusHandler>();

            status.SetHealth(0);
            status.OnPlayerDeath();   
        }
    }
}
