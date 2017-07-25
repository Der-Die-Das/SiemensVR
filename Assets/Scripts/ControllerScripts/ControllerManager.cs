using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public SteamVR_TrackedObject[] trackedObjects;

    void Awake()
    {
        //SteamVR_TrackedObject[] temp = transform.parent.GetComponentsInChildren<SteamVR_TrackedObject>();
        //List<SteamVR_TrackedObject> tempList = new List<SteamVR_TrackedObject>();
        //for (int i = 0; i < temp.Length; i++)
        //{
        //    if (temp[i].name.Contains("Controller"))
        //    {
        //        tempList.Add(temp[i]);
        //    }
        //}
        //trackedObjects = tempList.ToArray();
    }

    public SteamVR_Controller.Device GetController(int index)
    {
        int deviceIndex = (int)trackedObjects[index].index;
        if (deviceIndex >= 0)
        {
            return SteamVR_Controller.Input(deviceIndex);
        }
        else
        {
            return null;
        }
    }

    public bool ButtonHeldDownBothController(Valve.VR.EVRButtonId button)
    {
        if (trackedObjects.Length == 0)
        {
            return false;
        }
        for (int i = 0; i < trackedObjects.Length; i++)
        {
            if (GetController(i) == null)
                return false;

            if (!GetController(i).GetPress(button))
            {
                return false;
            }
        }
        return true;
    }
}
