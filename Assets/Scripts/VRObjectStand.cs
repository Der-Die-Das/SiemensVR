using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Collider))]
public class VRObjectStand : MonoBehaviour
{
    public Color targetColor = Color.red;
    public System.Action<Keycard> keycardInserted;
    public System.Action<Keycard> keycardEjected;
    public Transform targetTransform;

    private Color origColor;
    private MeshRenderer meshRenderer;
    private Keycard objectInRange;
    private Keycard keycardIn;

    // Use this for initialization
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        origColor = meshRenderer.material.color;

        foreach (var item in GameObject.FindObjectsOfType<ControllerGrabObject>())
        {
            item.ObjectReleased += OnControllerDropObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

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
