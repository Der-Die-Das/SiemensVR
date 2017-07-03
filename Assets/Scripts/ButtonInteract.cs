using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (Controller.GetHairTriggerDown())
        {
            if (buttonInRange != null)
            {
                buttonInRange.Interact();
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        VRButton button = other.GetComponent<VRButton>();
        if (button != null && buttonInRange == null)
        {
            buttonInRange = button;
            button.OnControllerEnter();
        }
    }

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

    private void OnTriggerExit(Collider other)
    {
        VRButton button = other.GetComponent<VRButton>();
        if (button == buttonInRange)
        {
            buttonInRange = null;
            button.OnControllerLeave();
        }
    }


}
