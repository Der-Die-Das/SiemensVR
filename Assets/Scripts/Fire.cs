using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public bool lit = false;
    public GameObject raycastOrigin;
    public Light flickeringLight;
    public float minLightIntensity;
    public float maxLightIntensity;
    public float flickeringSpeed;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Flickering());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Flickering()
    {
        while (true)
        {
            if (lit)
            {
                flickeringLight.intensity = Random.Range(minLightIntensity, maxLightIntensity);
            }
            yield return new WaitForSeconds(1f / flickeringSpeed);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Igniteable igniteableObject = other.GetComponent<Igniteable>();
        Igniteable thisIgniteable = GetComponentInChildren<Igniteable>();
        if (igniteableObject && lit && igniteableObject != thisIgniteable)
        {
            RaycastHit hit;
            if (Physics.Raycast(raycastOrigin.transform.position, other.transform.position - raycastOrigin.transform.position, out hit))
            {
                igniteableObject.Ignite(hit.point);
            }

        }
    }
}
