using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;


public class CollectableHandler : MonoBehaviour
{
    private enum CollectableType
    {
        none = 0,
        Collectable = 1,
        Secret = 2
    }

    [SerializeField, Tag] private string tagToDetect;
    [SerializeField] private CollectableType collectableType;
    [SerializeField, ReadOnly] private CollectableManager cManager = null;

    void Start()
    {
        if (cManager == null && CollectableManager._inst != null)
            cManager = CollectableManager._inst;
    }

    void OnTriggerEnter(Collider other)
    {
        if (cManager == null)
            return;

        if (other.gameObject.tag == tagToDetect)
        {
            switch(collectableType)
            {
                case CollectableType.Collectable:
                    cManager.IncrementNumCollectablesFound();
                    break;
                case CollectableType.Secret:
                    cManager.IncrementNumSecretsFound();
                    break;
            }

            cManager.collectables.Remove(this.gameObject);

            Destroy(this.gameObject);
        }
    }
}
