using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRSensor : MonoBehaviour
{
    public System.Action<GameObject> triggerEnter;
    public System.Action<GameObject> triggerLeave;

    private void OnTriggerEnter(Collider other)
    {
        triggerEnter(other.gameObject);

    }
    private void OnTriggerExit(Collider other)
    {
        triggerLeave(other.gameObject);
    }
}
