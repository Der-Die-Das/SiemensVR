using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRClock: MonoBehaviour
{
    private VRTime timeScript;
    public Transform bigHand;
    public Transform smallHand;

    private void Start()
    {
        timeScript = GameObject.FindObjectOfType<VRTime>();
        timeScript.timeChanged += OnTimeChanged;
    }

    private void Update()
    {
       
    }
    private void OnTimeChanged(float newTime)
    {
        UpdateClock(newTime);
    }
    private void UpdateClock(float time)
    {
        Vector3 rot = bigHand.localRotation.eulerAngles;
        float f = time - Mathf.Floor(time);
        float remaped = Remap(f, 0, 1, 0, 360) + 180;
        rot.z = remaped;
        bigHand.localRotation = Quaternion.Euler(rot);

        rot = smallHand.localRotation.eulerAngles;
        f = Mathf.Floor(time);
        remaped = f * 30;
        rot.z = remaped;
        smallHand.localRotation = Quaternion.Euler(rot);
    }

    private float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}