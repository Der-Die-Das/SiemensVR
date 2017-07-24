using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class VRSensor : MonoBehaviour
{
    public Action<Collider> triggerEnter;
    public Action<Collider> triggerLeave;
    public Action<Collision> collisionEnter;
    public Action<Collision> collisionLeave;

    private void OnTriggerEnter(Collider other)
    {
        if (triggerEnter != null)
        {
            triggerEnter(other);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (triggerLeave != null)
        {
            triggerLeave(other);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collisionEnter != null)
        {
            collisionEnter(collision);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collisionLeave != null)
        {
            collisionLeave(collision);
        }
    }
}
