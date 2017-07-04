﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Collider))]
public class VRObjectStand : MonoBehaviour
{
    public Color targetColor = Color.red;
    public System.Action<Keycard> keycardInserted;
    public System.Action<Keycard> keycardEjected;
    public Transform targetTransform;
    public VRButton ejectButton;

    private Color origColor;
    private MeshRenderer meshRenderer;
    private Keycard objectInRange;
    private Keycard keycardIn;


    public ControllerGrabObject[] grabs;

    // Use this for initialization
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        origColor = meshRenderer.material.color;


        foreach (var item in grabs)
        {
            item.ObjectReleased += OnControllerDropObject;
        }
        ejectButton.buttonPressed += OnEject;
     
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEject(int no)
    {
        Destroy(gameObject.GetComponent<Joint>());
        keycardIn.GetComponent<Rigidbody>().AddForce(transform.forward*1000f);
    }

    private void OnControllerDropObject(GameObject obj)
    {
        Keycard card = obj.GetComponent<Keycard>();
        if (card == objectInRange)
        {
            keycardIn = card;
            if (keycardInserted != null)
            {
                keycardInserted.Invoke(card);
                card.transform.position = targetTransform.position;
                card.transform.rotation = targetTransform.rotation;

                FixedJoint fx = gameObject.AddComponent<FixedJoint>();
                fx.breakForce = 5000;
                fx.breakTorque = 5000;
                fx.connectedBody = card.GetComponent<Rigidbody>();
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Keycard card = other.GetComponent<Keycard>();
        if (card != null && objectInRange == null)
        {
            objectInRange = card;
            meshRenderer.material.color = Color.Lerp(origColor, targetColor, 0.5f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Keycard card = other.GetComponent<Keycard>();
        if (card != null && objectInRange == null)
        {
            objectInRange = card;
            meshRenderer.material.color = Color.Lerp(origColor, targetColor, 0.5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Keycard card = other.GetComponent<Keycard>();
        if (card != null && objectInRange == card)
        {
            if (keycardIn != null)
            {
                if (keycardEjected != null)
                {
                    keycardEjected.Invoke(keycardIn);
                }
                keycardIn = null;
            }
            objectInRange = null;
            meshRenderer.material.color = origColor;
        }
    }
}
