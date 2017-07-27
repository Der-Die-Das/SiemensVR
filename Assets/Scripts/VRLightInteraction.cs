using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(VRWatchInteraction))]
public class VRLightInteraction : VRInteractionType
{
    public GameObject[] accordingTimePrefab;
    private VRLight interactingLight;


    private bool interactedThisFrame = false;


    protected override void Start()
    {
        base.Start();

        var controllers = controllerManager.controllerInfos;

        for (int i = 0; i < controllers.Length; i++)
        {
            LightInteractionInformation info = (LightInteractionInformation)controllers[i].GetFunctionalityInfoByType(typeof(LightInteractionInformation));
            WatchInteractionInformation watchInfo = (WatchInteractionInformation)controllers[i].GetFunctionalityInfoByType(typeof(WatchInteractionInformation));

            info.isOnTime = (i == 0 ? true : false);

            info.OnOffTimeObject = (GameObject)Instantiate(accordingTimePrefab[i], watchInfo.watch.transform, false);
            info.OnOffTimeObject.SetActive(false);

        }

        vrInteraction = GetComponent<VRWatchInteraction>();
        ((VRWatchInteraction)vrInteraction).onInteract += OnInteract;
        ((VRWatchInteraction)vrInteraction).updateDisplay += UpdateDisplay;
    }


    protected void Update()
    {

    }


    protected void UpdateDisplay(ControllerInformation controller)
    {
        LightInteractionInformation info = (LightInteractionInformation)controller.GetFunctionalityInfoByType(typeof(LightInteractionInformation));
        WatchInteractionInformation watchInfo = (WatchInteractionInformation)controller.GetFunctionalityInfoByType(typeof(WatchInteractionInformation));

        if (watchInfo.watch.activeSelf && vrInteraction.interactingWith == this)
        {
            GameObject correctPart = null;
            if (info.isOnTime)
            {
                if ((watchInfo.isWatchOnFront && interactingLight.StartTime < 13) || (!watchInfo.isWatchOnFront && interactingLight.StartTime > 12))
                {
                    correctPart = ((VRWatchInteraction)vrInteraction).getCorrectPart(interactingLight.StartTime, controller);
                }
            }
            else
            {
                if ((watchInfo.isWatchOnFront && interactingLight.EndTime < 13) || (!watchInfo.isWatchOnFront && interactingLight.EndTime > 12))
                {
                    correctPart = ((VRWatchInteraction)vrInteraction).getCorrectPart(interactingLight.EndTime, controller);
                }
            }
            foreach (var item in watchInfo.allParts)
            {
                if (item != correctPart)
                    StartCoroutine(((VRWatchInteraction)vrInteraction).SetPartToNormal(item));
            }
            if (correctPart != null)
            {
                StartCoroutine(((VRWatchInteraction)vrInteraction).SetPartToSelected(correctPart));
            }

        }
    }

    protected override void OnInteract(GameObject go, ControllerInformation controller)
    {
        LightInteractionInformation info = (LightInteractionInformation)controller.GetFunctionalityInfoByType(typeof(LightInteractionInformation));

        interactingLight = go.GetComponent<VRLight>();
        if (interactingLight)
        {
            ((VRWatchInteraction)vrInteraction).ShowWatch(controller);
            vrInteraction.interactingWith = this;
            interactedThisFrame = true;
            info.OnOffTimeObject.SetActive(true);

            if (interactingLight.taskCompleted[0] != null)
            {
                interactingLight.taskCompleted[0].Invoke();
            }
        }
        else
        {
            controllerManager.GetController(controller.trackedObj).TriggerHapticPulse(3000);
        }
    }

    protected override void ActiveControllerUpdate(ControllerInformation controller)
    {
        if (vrInteraction.interactingWith && vrInteraction.interactingWith == this)
        {
            LightInteractionInformation info = (LightInteractionInformation)controller.GetFunctionalityInfoByType(typeof(LightInteractionInformation));
            if (!interactedThisFrame)
            {
                if (controllerManager.GetController(controller.trackedObj).GetPressUp(vrInteraction.menuButton))
                {
                    vrInteraction.interactingWith = null;
                    ((VRWatchInteraction)vrInteraction).HideWatch(controller);
                    info.OnOffTimeObject.SetActive(false);
                }
            }
            else
            {
                interactedThisFrame = !interactedThisFrame;
            }
            int time = ((VRWatchInteraction)vrInteraction).getTimeByVector(vrInteraction.touchPadValue);
            if (time != 0)
            {
                WatchInteractionInformation watchInfo = (WatchInteractionInformation)controller.GetFunctionalityInfoByType(typeof(WatchInteractionInformation));

                if (!watchInfo.isWatchOnFront)
                {
                    time += 12;
                }
                if (info.isOnTime && Mathf.Floor(time) != Mathf.Floor(interactingLight.StartTime))
                {
                    interactingLight.StartTime = time;
                    controllerManager.GetController(controller.trackedObj).TriggerHapticPulse(3000);
                    UpdateDisplay(controller);
                    if (interactingLight.taskCompleted[2] != null)
                    {
                        interactingLight.taskCompleted[2].Invoke();
                    }
                }
                else if (Mathf.Floor(time) != Mathf.Floor(interactingLight.EndTime))
                {
                    interactingLight.EndTime = time;
                    controllerManager.GetController(controller.trackedObj).TriggerHapticPulse(3000);
                    UpdateDisplay(controller);
                    if (interactingLight.taskCompleted[2] != null)
                    {
                        interactingLight.taskCompleted[2].Invoke();
                    }
                }
            }
            if (controllerManager.GetController(controller.trackedObj).GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip))
            {
                if (interactingLight.taskCompleted[1] != null)
                {
                    interactingLight.taskCompleted[1].Invoke();
                }
            }
        }
    }

    protected override void NonActiveControllerUpdate(ControllerInformation controller)
    {
        if (vrInteraction.interactingWith && vrInteraction.interactingWith == this)
        {
            if (!interactedThisFrame)
            {
                if (controllerManager.GetController(controllerManager.getOtherController(controller).trackedObj).GetPressUp(vrInteraction.menuButton))
                {
                    LightInteractionInformation info = (LightInteractionInformation)controller.GetFunctionalityInfoByType(typeof(LightInteractionInformation));

                    ((VRWatchInteraction)vrInteraction).HideWatch(controller);
                    info.OnOffTimeObject.SetActive(false);
                }
            }
        }
    }

    protected override void AnyControllerUpdate(ControllerInformation controller) { }
}
