﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithHelpScreen : MonoBehaviour
{
    public static TutorialScreen interactingHelpScreen;

    public LayerMask layer;
    public float toleranceForOutsideRecognition = 0.1f;


    public GameObject laserPrefab; // The laser prefab
    private GameObject laser; // A reference to the spawned laser
    private Transform laserTransform; // The transform component of the laser for ease of use

    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Use this for initialization
    void Start()
    {
        laser = Instantiate(laserPrefab, transform);
        laserTransform = laser.transform;
        laser.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        HideLaser();
        if (interactingHelpScreen != null)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, layer))
            {
                ShowLaser(hit);
                if (hit.collider.gameObject.name == "UpperTopicBorder") //not the nicest way to solve..
                {
                    hit.collider.GetComponentInParent<TutorialScreen>().Scroll(-1);
                }
                else if (hit.collider.gameObject.name == "LowerTopicBorder") //not the nicest way to solve..
                {
                    hit.collider.GetComponentInParent<TutorialScreen>().Scroll(1);
                }
            }

            if (Controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
            {
                Click();
            }
        }
    }
    [ContextMenu("Click")]
    public void Click()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * 100f);
        RaycastHit[] hits = Physics.RaycastAll(ray, 100f, layer);
        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                UITracked tracker = hit.collider.GetComponent<UITracked>();

                if (tracker != null)
                {
                    VRTopic topic = tracker.trackedUIObject.GetComponent<VRTopic>();
                    TutorialScreen screen = topic.GetComponentInParent<TutorialScreen>();
                    if (tracker.transform.position.y > screen.getLowerBorder().position.y - toleranceForOutsideRecognition && tracker.transform.position.y < screen.getUpperBorder().position.y + toleranceForOutsideRecognition)
                    {
                        screen.selectedTopic = topic;
                    }
                    break;
                }
            }
        }
    }
    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true); //Show the laser
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hit.point, .5f); // Move laser to the middle between the controller and the position the raycast hit
        laserTransform.LookAt(hit.point); // Rotate laser facing the hit point
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
            hit.distance * 1.333f); // Scale laser so it fits exactly between the controller & the hit point
    }
    private void HideLaser()
    {
        laser.SetActive(false);
    }
}

