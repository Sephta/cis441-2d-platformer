using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceSpriteToCamera : MonoBehaviour
{
    private Camera _cam = null;

    void Start()
    {
        if (_cam == null && Camera.main != null)
            _cam = Camera.main;
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(_cam.transform.forward);
    }
}
