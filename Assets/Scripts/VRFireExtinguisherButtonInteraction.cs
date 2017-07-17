using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFireExtinguisherButtonInteraction : VRInteraction {

    [HideInInspector]
    public GameObject button;
    public GameObject buttonPrefab;
    public float turnSpeed;

    private bool isSwitchingSide = false;
    private Transform head;
    private bool isOn = false;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        head = FindObjectOfType<SteamVR_Camera>().transform;
        button = Instantiate(buttonPrefab, transform);
        button.SetActive(false);
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();

        if (interactingWith)
        {
            if (Controller.GetPressDown(gripButton))
            {
                StartCoroutine(SwitchSide());
            }
        }

        button.transform.LookAt(head);
	}
    public void ShowButton()
    {
        button.SetActive(true);
    }

    public void HideButton()
    {
        button.SetActive(false);
    }

    public IEnumerator SwitchSide()
    {
        if (isSwitchingSide)
            yield break;

        isOn = !isOn;
        isSwitchingSide = true;
        Vector3 oldRot = button.transform.GetChild(0).localRotation.eulerAngles;
        Vector3 targetRot = new Vector3(oldRot.x, oldRot.y, oldRot.z + 180);
        
        while (button.transform.GetChild(0).localRotation != Quaternion.Euler(targetRot))
        {
            button.transform.GetChild(0).localRotation = Quaternion.RotateTowards(button.transform.GetChild(0).localRotation, Quaternion.Euler(targetRot), turnSpeed);
            yield return 0;
        }

        isSwitchingSide = false;
    }
}
