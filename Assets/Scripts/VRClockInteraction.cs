using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(VRWatchInteraction))]
public class VRClockInteraction : VRInteractionType
{
    private VRTime timeScript;
    [HideInInspector]
    public VRClock interactingClock;
    private bool interactedThisFrame = false;

    protected override void Start()
    {
        base.Start();
        timeScript = GameObject.FindObjectOfType<VRTime>();
        timeScript.timeChanged += OnTimeChanged;
        ((VRWatchInteraction)vrInteraction).onInteract += OnInteract;
        ((VRWatchInteraction)vrInteraction).updateDisplay += UpdateDisplay;

    }
    protected void Update()
    {
        if (vrInteraction.interactingWith && vrInteraction.interactingWith == this)
        {
            if (vrInteraction.Controller.GetPressUp(vrInteraction.menuButton))
            {
                if (!interactedThisFrame)
                {
                    vrInteraction.interactingWith = null;
                    ((VRWatchInteraction)vrInteraction).HideWatch();
                }
                else
                {
                    interactedThisFrame = !interactedThisFrame;
                    //notification for tutorial
                    if (interactingClock.taskCompleted[0] != null)
                    {
                        interactingClock.taskCompleted[0].Invoke();
                    }
                }

            }
            if (vrInteraction.Controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip))
            {
                //notification for tutorial
                if (interactingClock.taskCompleted[1] != null)
                {
                    interactingClock.taskCompleted[1].Invoke();
                }
            }

            int time = ((VRWatchInteraction)vrInteraction).getTimeByVector(vrInteraction.touchPadValue);
            if (time != 0 && Mathf.Floor(time) != Mathf.Floor(timeScript.Time))
            {
                SetTime(time);
                vrInteraction.Controller.TriggerHapticPulse(3000);
            }
        }
    }
    protected override void OnInteract(GameObject go)
    {
        interactingClock = go.GetComponent<VRClock>();
        if (interactingClock)
        {
            ((VRWatchInteraction)vrInteraction).ShowWatch();
            vrInteraction.interactingWith = this;
            interactedThisFrame = true;
        }
        else
        {
            vrInteraction.Controller.TriggerHapticPulse(3000);
        }
    }
    private void OnTimeChanged(float newTime)
    {
        UpdateDisplay();
    }
    protected void UpdateDisplay()
    {
        if (((VRWatchInteraction)vrInteraction).watch.activeSelf && vrInteraction.interactingWith == this)
        {
            foreach (var item in ((VRWatchInteraction)vrInteraction).allParts)
            {
                StartCoroutine(((VRWatchInteraction)vrInteraction).SetPartToNormal(item));
            }
            if (((VRWatchInteraction)vrInteraction).isWatchOnFront != timeScript.isNight)
            {   
                if (Mathf.FloorToInt(timeScript.Time) - 1 >= 0 && Mathf.FloorToInt(timeScript.Time) - 1 < ((VRWatchInteraction)vrInteraction).allParts.Length)
                {
                    StartCoroutine(((VRWatchInteraction)vrInteraction).SetPartToSelected(((VRWatchInteraction)vrInteraction).getCorrectPart(Mathf.FloorToInt(timeScript.Time))));
                }
            }
        }
    }
    public void SetTime(int newTime)
    {
        if (timeScript.Time != newTime)
        {
            float f = timeScript.Time - Mathf.Floor(timeScript.Time);
            timeScript.Time = newTime + f;
            timeScript.isNight = !((VRWatchInteraction)vrInteraction).isWatchOnFront;
            UpdateDisplay();

            //notification for tutorial
            if (interactingClock.taskCompleted[2] != null)
            {
                interactingClock.taskCompleted[2].Invoke();
            }
        }
    }
    
}
