using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRLight : MonoBehaviour
{
    public Light controlledLight;
    private float defaultIntensity;
    private VRTime time;

    private int _StartTime;
    public int StartTime
    {
        get
        {
            return _StartTime;
        }
        set
        {
            if (value <= 24 && value > 0)
            {
                _StartTime = value;
                UpdateLightStatus();
            }
        }
    }

    private int _EndTime;
    public int EndTime
    {
        get
        {
            return _EndTime;
        }
        set
        {
            if (value <= 24 && value > 0)
            {
                _EndTime = value;
                UpdateLightStatus();
            }
        }
    }

    private void Start()
    {
        _StartTime = 1;
        _EndTime = 1;
        time = GameObject.FindObjectOfType<VRTime>();
        time.timeChanged += OnTimeChanged;
        defaultIntensity = controlledLight.intensity;
        UpdateLightStatus();
    }

    private void OnTimeChanged(float time)
    {
        UpdateLightStatus();
    }

    private void UpdateLightStatus()
    {
        if ((time.Time > _StartTime && time.Time < _EndTime) || _StartTime == _EndTime || (_StartTime > _EndTime && (time.Time >= _StartTime || time.Time <= _EndTime)))
        {
            controlledLight.intensity = defaultIntensity;
        }
        else
        {
            controlledLight.intensity = 0f;
        }
    }
}
