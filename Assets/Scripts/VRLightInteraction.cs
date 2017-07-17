using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(VRWatchInteraction))]
public class VRLightInteraction : VRInteractionType
{
    public bool isOnTime;
    public GameObject accordingTimePrefab;
    VRLight interactingLight;
    VRLightInteraction otherLightInteractor;

    GameObject OnOffTimeObject;

    private bool interactedThisFrame = false;


    protected override void Start()
    {
        base.Start();
        OnOffTimeObject = (GameObject)Instantiate(accordingTimePrefab, ((VRWatchInteraction)vrInteraction).watch.transform, false);
        OnOffTimeObject.SetActive(false);
        vrInteraction = GetComponent<VRWatchInteraction>();
        otherLightInteractor = vrInteraction.otherInteractor.GetComponent<VRLightInteraction>();
        ((VRWatchInteraction)vrInteraction).onInteract += OnInteract;
        ((VRWatchInteraction)vrInteraction).updateDisplay += UpdateDisplay;
    }


    protected void Update()
    {
        if (vrInteraction.interactingWith && (vrInteraction.interactingWith == this || vrInteraction.interactingWith == otherLightInteractor))
        {
            if (!interactedThisFrame)
            {
                if (vrInteraction.Controller.GetPressUp(vrInteraction.menuButton))
                {
                    vrInteraction.interactingWith = null;
                    vrInteraction.otherInteractor.interactingWith = null;
                    ((VRWatchInteraction)vrInteraction).HideWatch();
                    ((VRWatchInteraction)otherLightInteractor.vrInteraction).HideWatch();
                    OnOffTimeObject.SetActive(false);
                    otherLightInteractor.OnOffTimeObject.SetActive(false);
                }
            }
            else
            {
                interactedThisFrame = !interactedThisFrame;
            }
            int time = ((VRWatchInteraction)vrInteraction).getTimeByVector(vrInteraction.touchPadValue);
            if (time != 0)
            {
                if (!((VRWatchInteraction)vrInteraction).isWatchOnFront)
                {
                    time += 12;
                }
                if (isOnTime && Mathf.Floor(time) != Mathf.Floor(interactingLight.StartTime))
                {
                    interactingLight.StartTime = time;
                    vrInteraction.Controller.TriggerHapticPulse(3000);
                    UpdateDisplay();
                }
                else if (Mathf.Floor(time) != Mathf.Floor(interactingLight.EndTime))
                {
                    interactingLight.EndTime = time;
                    vrInteraction.Controller.TriggerHapticPulse(3000);
                    UpdateDisplay();
                }
            }
        }
    }


    protected override void OnInteract(GameObject go)
    {
        interactingLight = go.GetComponent<VRLight>();
        if (interactingLight)
        {
            otherLightInteractor.interactingLight = interactingLight;

            ((VRWatchInteraction)vrInteraction).ShowWatch();
            ((VRWatchInteraction)otherLightInteractor.vrInteraction).ShowWatch();
            vrInteraction.interactingWith = this;
            otherLightInteractor.vrInteraction.interactingWith = this;
            interactedThisFrame = true;
            OnOffTimeObject.SetActive(true);
            otherLightInteractor.OnOffTimeObject.SetActive(true);
        }
        else
        {
            vrInteraction.Controller.TriggerHapticPulse(3000);
        }
    }

    protected void UpdateDisplay()
    {
        if (((VRWatchInteraction)vrInteraction).watch.activeSelf && (vrInteraction.interactingWith == this || vrInteraction.interactingWith == otherLightInteractor))
        {
            GameObject correctPart = null;
            if (isOnTime)
            {
                if ((((VRWatchInteraction)vrInteraction).isWatchOnFront && interactingLight.StartTime < 13) || (!((VRWatchInteraction)vrInteraction).isWatchOnFront && interactingLight.StartTime > 12))
                {
                    correctPart = ((VRWatchInteraction)vrInteraction).getCorrectPart(interactingLight.StartTime);
                }
            }
            else
            {
                if ((((VRWatchInteraction)vrInteraction).isWatchOnFront && interactingLight.EndTime < 13) || (!((VRWatchInteraction)vrInteraction).isWatchOnFront && interactingLight.EndTime > 12))
                {
                    correctPart = ((VRWatchInteraction)vrInteraction).getCorrectPart(interactingLight.EndTime);
                }
            }
            foreach (var item in ((VRWatchInteraction)vrInteraction).allParts)
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
}
