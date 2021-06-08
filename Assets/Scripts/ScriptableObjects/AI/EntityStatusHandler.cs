using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;


public class EntityStatusHandler : MonoBehaviour
{
    [Header("Entity Stats")]
    public int maxHealth = 0;
    [SerializeField, ReadOnly] private int currHealth = 0;

    void Start()
    {
        SetHealth(maxHealth);
    }

    void Update()
    {
        if (currHealth == 0)
            Destroy(this.transform.parent.gameObject);
    }

    public void SetHealth(int amount) =>
        currHealth = amount;

    public void UpdateHealth(int amount) =>
        currHealth = (int) Mathf.Clamp((float)(currHealth - amount), 0f, (float) maxHealth);
}
