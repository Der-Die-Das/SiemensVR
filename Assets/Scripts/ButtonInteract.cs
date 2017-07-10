using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Script is attached to a Controller.
/// The Script is used to interact with buttons.
/// </summary>
public class ButtonInteract : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }


    private VRButton buttonInRange;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //Here we check if the Trigger is pressed down.
        if (Controller.GetHairTriggerDown())
        {
            if (buttonInRange != null)
            {
                buttonInRange.Interact();
            }

        }
    }

    //If something enters our Trigger..
    private void OnTriggerEnter(Collider other)
    {
        VRButton button = other.GetComponent<VRButton>();
        //we check if it is a button and we are not already interacting with one
        if (button != null && buttonInRange == null)
        {
            buttonInRange = button;
            //And then we apply color to the button..
            button.OnControllerEnter();
        }
    }

    //same as OnTriggerEnter, since we sometimes lose "buttonInRange" and then no new Button is applied
    private void OnTriggerStay(Collider other)
    {
        if (buttonInRange == null)
        {
            VRButton button = other.GetComponent<VRButton>();
            if (button != null)
            {
                buttonInRange = button;
                button.OnControllerEnter();
            }
        }
    }

    //if something left our Trigger
    private void OnTriggerExit(Collider other)
    {
        VRButton button = other.GetComponent<VRButton>();
        //we check if it was a button and if it was the one in range
        if (button != null && button == buttonInRange)
        {
            buttonInRange = null;
            //we get rid of the color again
            button.OnControllerLeave();
        }
    }


}
