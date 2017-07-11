using UnityEngine;
using System.Collections;

public class VRTime : MonoBehaviour
{
    public System.Action<float> timeChanged;
    public float secAmountForOneDay = 24;
    private float _Time;
    public float Time
    {
        get
        {
            return _Time;
        }
        set
        {
            _Time = value;
            if (timeChanged != null)
            {
                timeChanged(_Time);
            }
        }
    }

    private void Start()
    {
        Time = 1;
        StartCoroutine(IncreaseTime());
    }

    private IEnumerator IncreaseTime()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            Time += 24 / secAmountForOneDay;
        }
    }
}
