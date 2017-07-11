using UnityEngine;
using System.Collections;

public class VRTime : MonoBehaviour
{
    public System.Action<float> timeChanged;
    public float secAmountForOneDay = 24;
    public float tickSpeed = 1;
    private float _Time;
    public float Time
    {
        get
        {
            return _Time;
        }
        set
        {
            if (value > 13)
            {
                _Time = (value % 12) + (value - 12);
                isNight = !isNight;
            }
            else
            {
                _Time = value;
            }
            if (timeChanged != null)
            {
                timeChanged(_Time);
            }
        }
    }

    private bool isNight = false;

    private void Start()
    {
        Time = 1;
        StartCoroutine(IncreaseTime());
    }

    private IEnumerator IncreaseTime()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(tickSpeed);
            float addAmount = 24 / secAmountForOneDay* tickSpeed;

            Time += addAmount;
        }
    }
}
