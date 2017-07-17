using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRWatchInteraction : VRInteraction
{
    public VRWatchInteractionProperties properties;
    [HideInInspector]
    public Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    public Action updateDisplay;


    [HideInInspector]
    public GameObject watch;
    [HideInInspector]
    public GameObject[] allParts;
    [HideInInspector]
    public bool isWatchOnFront = true;

    private bool isSwitchingSide = false;
    private Transform head;

    protected override void Start()
    {
        base.Start();
        head = GameObject.FindObjectOfType<SteamVR_Camera>().transform;
        watch = Instantiate(properties.watchPrefab, transform);
        watch.SetActive(false);
        allParts = GetOrderedParts();
    }

    protected override void Update()
    {
        base.Update();
        if (interactingWith)
        {
            if (Controller.GetPressDown(gripButton))
            {
                StartCoroutine(SwitchSide());
            }
        }
        watch.transform.LookAt(head);
    }

    public void ShowWatch()
    {
        watch.SetActive(true);
        updateDisplay();
    }
    public void HideWatch()
    {
        watch.SetActive(false);
    }

    public int getTimeByVector(Vector2 vec)
    {
        if (vec == Vector2.zero)
        {
            return 0;
        }

        Vector3 forward = new Vector3(vec.x, 0, vec.y);
        Vector3 upward = new Vector3(0, 1, 0);
        Vector3 rotation = Quaternion.LookRotation(forward, upward).eulerAngles;

        int returnValue = Mathf.FloorToInt(rotation.y / 30f);
        if (returnValue == 0)
        {
            return 12;
        }
        return returnValue;
    }
    public IEnumerator SetPartToNormal(GameObject part)
    {
        MeshRenderer rend = part.GetComponent<MeshRenderer>();
        rend.material = properties.normalMaterial;
        while (part.transform.localScale.x > properties.normalSize)
        {
            part.transform.localScale = part.transform.localScale - Vector3.one * properties.FadeSpeed;
            yield return 0;
        }
    }
    public IEnumerator SetPartToSelected(GameObject part)
    {
        MeshRenderer rend = part.GetComponent<MeshRenderer>();
        rend.material = properties.highlightedMaterial;
        while (part.transform.localScale.x < properties.highlightedSize)
        {
            part.transform.localScale = part.transform.localScale + Vector3.one * properties.FadeSpeed;
            yield return 0;
        }
    }
    private GameObject[] GetOrderedParts()
    {
        GameObject[] parts = new GameObject[12];
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i] = watch.transform.GetChild(0).Find("Part" + (i + 1)).gameObject;
        }
        return parts;
    }

    public IEnumerator SwitchSide()
    {
        if (isSwitchingSide)
        {
            yield break;
        }
        updateDisplay();
        isWatchOnFront = !isWatchOnFront;
        isSwitchingSide = true;
        Transform child = watch.transform.GetChild(0);
        Vector3 oldRot = child.localRotation.eulerAngles;
        Vector3 targetRot = oldRot;
        targetRot.z += 180;
        while (child.transform.localRotation != Quaternion.Euler(targetRot))
        {
            child.transform.localRotation = Quaternion.RotateTowards(child.transform.localRotation, Quaternion.Euler(targetRot), properties.TurnSpeed);
            yield return 0;
        }
        isSwitchingSide = false;
    }
    public GameObject getCorrectPart(int time)
    {
        int newTime = time;
        if (newTime > 12)
        {
            newTime -= 12;
        }
        if (isWatchOnFront)
        {
            return allParts[newTime - 1];
        }
        else
        {
            return allParts[12 - newTime];
        }
    }


}
