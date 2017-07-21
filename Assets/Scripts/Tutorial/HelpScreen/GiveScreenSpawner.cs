using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveScreenSpawner : MonoBehaviour
{
    public GameObject spawnerPrefab;
    public Vector3 spawnOffset;
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "controller")
        {
            ControllerGrabObject grabber = other.GetComponent<ControllerGrabObject>();
            grabber.ForceGrab(Instantiate(spawnerPrefab,other.transform.position + spawnOffset, Quaternion.identity));
        }
    }
}
