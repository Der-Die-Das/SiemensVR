using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VRInteractionType : MonoBehaviour
{
    protected VRInteraction vrInteraction;

    protected virtual void Start()
    {
        vrInteraction = GetComponent<VRInteraction>();
        vrInteraction.onInteract += OnInteract;
    }

    protected abstract void OnInteract(GameObject go);

}
