using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public abstract class VRInteraction : MonoBehaviour
{
    //controller stuff
    private SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }

    }
    [HideInInspector]
    public Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    [HideInInspector]
    public Valve.VR.EVRButtonId menuButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
    [HideInInspector]
    public Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    [HideInInspector]
    public Vector2 touchPadValue;
    [HideInInspector]
    public Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;

    public Action<GameObject> onInteract;

    public GameObject laserPrefab; // The laser prefab
    private GameObject laser; // A reference to the spawned laser
    private Transform laserTransform; // The transform component of the laser for ease of use

    public VRInteraction otherInteractor;


    [HideInInspector]
    //public bool shouldInteract = true;
    public VRInteractionType interactingWith;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    protected virtual void Start()
    {
        laser = Instantiate(laserPrefab, transform);
        laserTransform = laser.transform;
        laser.SetActive(false);

        //otherInteractor = .ToList<VRInteraction>().Where(x => x != this).First();
        var a = GameObject.FindObjectsOfType<VRInteraction>();
        foreach (var item in a)
        {
            if (item != this)
            {
                otherInteractor = item;
                break;
            }
        }
    }


    protected virtual void Update()
    {
        touchPadValue = Controller.GetAxis(touchpad);
        if (!otherInteractor.interactingWith)
        {
            if (Controller.GetPress(menuButton))
            {
                RaycastHit hit;

                // Send out a raycast from the controller
                if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 10000) && !hit.collider.isTrigger)
                {
                    laser.SetActive(true); //Show the laser
                    laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hit.point, .5f); // Move laser to the middle between the controller and the position the raycast hit
                    laserTransform.LookAt(hit.point); // Rotate laser facing the hit point
                    laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
                        hit.distance * 1.333f); // Scale laser so it fits exactly between the controller & the hit point
                }
                else
                {
                    laser.SetActive(false);
                }
            }
            if (Controller.GetPressUp(menuButton))
            {
                RaycastHit hit;

                // Send out a raycast from the controller
                if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 10000) && !hit.collider.isTrigger)
                {
                    onInteract(hit.collider.gameObject);
                }
                else
                {
                    Controller.TriggerHapticPulse(3000);
                }
                laser.SetActive(false);

            }
        }

    }


}
