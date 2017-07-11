using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VRInteraction : MonoBehaviour
{
    //controller stuff
    private SteamVR_TrackedObject trackedObj;
    protected SteamVR_Controller.Device Controller;
    //{
    //    get { return SteamVR_Controller.Input((int)trackedObj.index); }
    //}
    protected Valve.VR.EVRButtonId menuButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
    protected Valve.VR.EVRButtonId touchpad  = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    protected Vector2 touchPadValue;

    
    public GameObject laserPrefab; // The laser prefab
    private GameObject laser; // A reference to the spawned laser
    private Transform laserTransform; // The transform component of the laser for ease of use

    protected bool shouldInteract = true;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    protected virtual void Start()
    {
        laser = Instantiate(laserPrefab,transform);
        laserTransform = laser.transform;
    }

    protected virtual void Update()
    {
        /*
        touchPadValue = Controller.GetAxis(touchpad);
        if (Controller.GetPressDown(menuButton))
        {
            RaycastHit hit;

            // Send out a raycast from the controller
            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100))
            {
                laser.SetActive(true); //Show the laser
                laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hit.point, .5f); // Move laser to the middle between the controller and the position the raycast hit
                laserTransform.LookAt(hit.point); // Rotate laser facing the hit point
                laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
                    hit.distance); // Scale laser so it fits exactly between the controller & the hit point
            }
        }
        if (Controller.GetPressUp(menuButton))
        {
            RaycastHit hit;

            // Send out a raycast from the controller
            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100))
            {
                OnInteract(hit.collider.gameObject);
            }
            else
            {
                Controller.TriggerHapticPulse(500);
            }
        }
        */
    }

    protected abstract void OnInteract(GameObject go);

}
