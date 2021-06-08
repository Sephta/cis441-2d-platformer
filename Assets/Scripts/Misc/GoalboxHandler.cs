using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;


public class GoalboxHandler : MonoBehaviour
{
    [SerializeField, Tag] private string tagToDetect;
    [SerializeField] private GameObject scoreScreen = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == tagToDetect)
        {
            if (scoreScreen != null)
                scoreScreen.SetActive(true);
            
            other.gameObject.GetComponent<PlayerStatusHandler>().DisablePlayerPlayables();
        }
    }
}
