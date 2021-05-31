using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Transform _cam = null;


    // PRIVATE VARS
    [SerializeField] private Vector2 parallaxMultiplier = Vector2.zero;
    [SerializeField, ReadOnly] private Vector3 lastCameraPos = Vector3.zero;
    [SerializeField, ReadOnly] private Vector2 textureUnitSize = Vector2.zero;
    [SerializeField, ReadOnly] private float initialHeight = 0f;


    private void Start()
    {
        if (_cam == null && Camera.main != null)
            _cam = Camera.main.transform;
        
        lastCameraPos = _cam.position;

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D tex = sprite.texture;
        textureUnitSize = new Vector2(
            tex.width / sprite.pixelsPerUnit,
            tex.height / sprite.pixelsPerUnit
        );

        initialHeight = transform.position.y;
    }

    private void LateUpdate()
    {
        Vector3 deltaMove = _cam.position - lastCameraPos;
        lastCameraPos = _cam.position;

        transform.position += new Vector3(
            deltaMove.x * parallaxMultiplier.x,
            deltaMove.y * parallaxMultiplier.y,
            0f
        );

        if (Mathf.Abs(_cam.position.x - transform.position.x) >= textureUnitSize.x)
        {
            float offsetPosX = (_cam.position.x - transform.position.x) % textureUnitSize.x;
            transform.position = new Vector3(
                _cam.position.x + offsetPosX,
                transform.position.y,
                transform.position.z
            );
        }

        if (Mathf.Abs(_cam.position.y - transform.position.y) >= textureUnitSize.y)
        {
            float offsetPosY = (_cam.position.y - transform.position.y) % textureUnitSize.y;
            transform.position = new Vector3(
                transform.position.x,
                offsetPosY + initialHeight,
                transform.position.z
            );
        }
    }
}
