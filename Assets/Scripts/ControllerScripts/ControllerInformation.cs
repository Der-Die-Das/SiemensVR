using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ControllerInformation
{
    public SteamVR_TrackedObject trackedObj { get; private set; }
    public VRSensor sensor { get; private set; }
    private List<ControllerFunctionalityInformation> functionalityInformations;


    public ControllerInformation(SteamVR_TrackedObject _trackedObj, VRSensor controllerSensor)
    {
        trackedObj = _trackedObj;
        sensor = controllerSensor;
        functionalityInformations = new List<ControllerFunctionalityInformation>();
    }

    public void AddFunctionalityInfo(ControllerFunctionalityInformation info)
    {
        functionalityInformations.Add(info);
    }
    public ControllerFunctionalityInformation GetFunctionalityInfoByType(Type type)
    {
        foreach (var item in functionalityInformations)
        {
            if (item.GetType() == type)
            {
                return item;
                Debug.Log("objName: " + trackedObj + " -- " + ((GrabObjectInformation)item).objectInHand);
            }
        }
        return null;
    }

}
