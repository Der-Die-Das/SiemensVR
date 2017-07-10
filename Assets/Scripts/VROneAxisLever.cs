using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VROneAxisLever : MonoBehaviour
{
    public int value { get; private set; }

    void Update()
    {
        value = Mathf.RoundToInt(transform.localRotation.eulerAngles.x) - 270;
        
        if (transform.localRotation.eulerAngles.y < 179)
        {
            value *= -1;
        }
    }
}
