using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecurityCamera : MonoBehaviour
{

    public float yRot = 1f;
    public float zRot = 1f;

    private Camera renderCamera;
    // Use this for initialization
    void Start()
    {
        renderCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(new Vector3(zRot, 0f, yRot), Space.Self);
        //transform.localRotation = Quaternion.Euler(new Vector3(zRot, yRot, 0f)+ transform.localRotation.eulerAngles);
        //Debug.Log(transform.localRotation.eulerAngles);
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

}
