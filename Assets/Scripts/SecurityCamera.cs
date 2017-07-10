﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecurityCamera : MonoBehaviour
{
    public Camera renderCamera;
    private Vector3 origPos;

    private void Awake()
    {
        origPos = renderCamera.transform.position;
    }

    private void LateUpdate()
    {
        renderCamera.transform.position = origPos;
    }

    public void SetZoom(float zoom)
    {
        renderCamera.fieldOfView += zoom / 10f;
    }
    public void Rotate(Vector2 value)
    {
        transform.Rotate(new Vector3(value.y, 0f, 0f), Space.Self);
        transform.Rotate(new Vector3(0f, value.x, 0f), Space.World);
    }

    public void SetTargetTexture(RenderTexture tex)
    {
        renderCamera.targetTexture = tex;
    }
    public void RemoveTargetTexture()
    {
        renderCamera.targetTexture = null;
    }

}