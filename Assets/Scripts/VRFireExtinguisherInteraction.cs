using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFireExtinguisherInteraction : VRInteractionType
{

    [HideInInspector]
    public FireExtinguisher interactingFireExtinguisher;

    private bool interactedThisFrame = false;

    protected override void Start()
    {
        base.Start();
        vrInteraction = GetComponent<VRFireExtinguisherButtonInteraction>();
<<<<<<< Updated upstream
        vrInteraction.onInteract += OnInteract;
    }
    void Update()
    {
        if(vrInteraction.interactingWith && vrInteraction.interactingWith == this)
        {
            if (vrInteraction.Controller.GetPressUp(vrInteraction.menuButton))
            {
                if (!interactedThisFrame)
                {
                    vrInteraction.interactingWith = null;
                    ((VRFireExtinguisherButtonInteraction)vrInteraction).HideButton();
                }
                else
                    interactedThisFrame = !interactedThisFrame;
            }
            if (vrInteraction.Controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip))
            {
                interactingFireExtinguisher.SetActivated(!interactingFireExtinguisher.GetActivated());
            }
        }

=======
>>>>>>> Stashed changes
    }

    protected override void OnInteract(GameObject go, ControllerInformation controller)
    {
        interactingFireExtinguisher = go.GetComponent<FireExtinguisher>();

        if (interactingFireExtinguisher)
        {
            ((VRFireExtinguisherButtonInteraction)vrInteraction).ShowButton();
            vrInteraction.interactingWith = this;
            interactedThisFrame = true;
            ActiveController = controller;
        }

        // delete this when merged with Time-Issue
        else
        {
            controllerManager.GetController(controller.trackedObj).TriggerHapticPulse(3000);
        }
    }

    public void ActivateFireExtinguisher(bool state)
    {
        interactingFireExtinguisher.SetActivated(state);
    }

    protected override void ActiveControllerUpdate(ControllerInformation controller)
    {
        if (vrInteraction.interactingWith && vrInteraction.interactingWith == this)
        {
            if (controllerManager.GetController(controller.trackedObj).GetPressUp(vrInteraction.menuButton))
            {
                if (!interactedThisFrame)
                {
                    vrInteraction.interactingWith = null;
                    ((VRFireExtinguisherButtonInteraction)vrInteraction).HideButton();
                    ActiveController = null;
                }
                else
                    interactedThisFrame = !interactedThisFrame;
            }
            if (controllerManager.GetController(controller.trackedObj).GetPressDown(vrInteraction.gripButton))
            {
                interactingFireExtinguisher.SetActivated(!interactingFireExtinguisher.GetActivated());
            }
        }
    }

    protected override void NonActiveControllerUpdate(ControllerInformation controller) { }

    protected override void AnyControllerUpdate(ControllerInformation controller) { }
}
