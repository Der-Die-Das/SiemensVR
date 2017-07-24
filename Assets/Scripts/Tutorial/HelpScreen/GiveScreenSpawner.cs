using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveScreenSpawner : MonoBehaviour
{
    public GameObject spawnerPrefab;
    public Vector3 spawnOffset;
    public ControllerGrabObject activeGrabber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "controller")
        {
            ControllerGrabObject grabber = other.GetComponent<ControllerGrabObject>();
            if (activeGrabber == null)
            {
                activeGrabber = grabber;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "controller")
        {
            ControllerGrabObject grabber = other.GetComponent<ControllerGrabObject>();
            if (activeGrabber == grabber)
            {
                activeGrabber = null;
            }
        }
    }

    private void Update()
    {
        if (activeGrabber != null && activeGrabber.Controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger) && InteractWithHelpScreen.interactingHelpScreen == null)
        {
            activeGrabber.ForceGrab(Instantiate(spawnerPrefab, activeGrabber.transform.position + spawnOffset, Quaternion.identity));
        }
    }
}
