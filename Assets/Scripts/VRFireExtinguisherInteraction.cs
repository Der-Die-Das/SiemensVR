using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFireExtinguisherInteraction : VRInteractionType {
    
    [HideInInspector]
    public FireExtinguisher interactingFireExtinguisher;

    private bool interactedThisFrame = false;

    protected override void Start()
    {
        base.Start();
        vrInteraction = GetComponent<VRFireExtinguisherButtonInteraction>();
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
            if (vrInteraction.Controller.GetPressDown(vrInteraction.gripButton))
            {
                interactingFireExtinguisher.SetActivated(!interactingFireExtinguisher.GetActivated());
            }
        }

    }

    protected override void OnInteract(GameObject go)
    {
        interactingFireExtinguisher = go.GetComponent<FireExtinguisher>();

        if (interactingFireExtinguisher)
        {
            ((VRFireExtinguisherButtonInteraction)vrInteraction).ShowButton();
            vrInteraction.interactingWith = this;
            interactedThisFrame = true;
        }
        // use this once merged with Time-Issue
        //else if (!go.GetComponent<VRClock>())
        //{
        //    vrInteraction.Controller.TriggerHapticPulse(3000);
        //}

        // delete this when merged with Time-Issue
        else
        {
            vrInteraction.Controller.TriggerHapticPulse(3000);
        }
    }

    public void ActivateFireExtinguisher(bool state)
    {
        interactingFireExtinguisher.SetActivated(state);
    }
}
