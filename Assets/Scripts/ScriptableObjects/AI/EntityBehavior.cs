using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBehavior : MonoBehaviour
{
    public abstract EntityBehavior GetCurrentBehavior();
}
