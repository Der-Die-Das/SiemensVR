using System;
using System.Collections;
using UnityEngine;

public class VRWatchInteraction : VRInteraction
{
    public GameObject watchOnController;
    public float FadeSpeed = 1;

    private VRTime timeScript;
    private GameObject[] allParts;
    [HideInInspector]
    public VRWatch interactingWatch;

    public Material normalMaterial;
    public Material highlightedMaterial;
    public int normalSize;
    public int highlightedSize;

    protected override void Start()
    {
        base.Start();
        timeScript = GameObject.FindObjectOfType<VRTime>();
        watchOnController.SetActive(false);
        allParts = GetOrderedParts();
        timeScript.timeChanged += OnTimeChanged;
    }

    protected override void Update()
    {
        base.Update();

        if (!shouldInteract)
        {
            if (Controller.GetPressUp(menuButton))
            {
                shouldInteract = true;
                HideMenu();
            }
            int time = getTimeByVector(touchPadValue);
            if (time != 0)
            {
                SetTime(time);
            }
        }
    }

    protected override void OnInteract(GameObject go)
    {
        interactingWatch = go.GetComponent<VRWatch>();
        if (interactingWatch)
        {
            shouldInteract = false;
            ShowMenu();
        }
        else
        {
            Controller.TriggerHapticPulse(500);
        }
    }

    private void ShowMenu()
    {
        watchOnController.SetActive(true);
        UpdateDisplay();
    }

    private void OnTimeChanged(float newTime)
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        foreach (var item in allParts)
        {
            StartCoroutine(SetPartToNormal(item));
        }
        if (Mathf.FloorToInt(timeScript.Time) - 1 >= 0 && Mathf.FloorToInt(timeScript.Time) - 1 < allParts.Length)
        {
            StartCoroutine(SetPartToSelected(allParts[Mathf.FloorToInt(timeScript.Time) - 1]));
        }
    }
    private void HideMenu()
    {
        watchOnController.SetActive(false);
    }
    public void SetTime(int newTime)
    {
        if (timeScript.Time != newTime)
        {
            timeScript.Time = newTime;
            UpdateDisplay();
        }
    }
    public void EditorWatchTriggered(VRWatch watch)
    {
        if (interactingWatch != null)
        {
            interactingWatch = null;
            HideMenu();
        }
        else
        {
            interactingWatch = watch;
            ShowMenu();
        }
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


        return Mathf.FloorToInt(rotation.y / 30f);
    }
    private IEnumerator SetPartToNormal(GameObject part)
    {
        MeshRenderer rend = part.GetComponent<MeshRenderer>();
        rend.material = normalMaterial;
        while (part.transform.localScale.x > normalSize)
        {
            part.transform.localScale = part.transform.localScale - Vector3.one * FadeSpeed;
            yield return 0;
        }
    }
    private IEnumerator SetPartToSelected(GameObject part)
    {
        MeshRenderer rend = part.GetComponent<MeshRenderer>();
        rend.material = highlightedMaterial;
        while (part.transform.localScale.x < highlightedSize)
        {
            part.transform.localScale = part.transform.localScale + Vector3.one * FadeSpeed;
            yield return 0;
        }
    }
    private GameObject[] GetOrderedParts()
    {
        GameObject[] parts = new GameObject[12];
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i] = watchOnController.transform.FindChild("Part" + (i + 1)).gameObject;
        }
        return parts;
    }
}
