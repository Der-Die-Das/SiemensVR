using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    public bool lit = false;
    public GameObject raycastOrigin;
    private Quaternion rotation;

    private void Awake()
    {
        rotation = transform.rotation;
    }

    private void LateUpdate()
    {
        transform.rotation = rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        Igniteable igniteableObject = other.GetComponent<Igniteable>();
        if (igniteableObject && lit)
        {
            RaycastHit hit;
            if(Physics.Raycast(raycastOrigin.transform.position, other.transform.position - raycastOrigin.transform.position, out hit))
            {
                igniteableObject.Ignite(hit.point);
            }
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Igniteable igniteableObject = other.GetComponent<Igniteable>();
        if (igniteableObject && lit)
        {
            RaycastHit hit;
            if (Physics.Raycast(raycastOrigin.transform.position, other.transform.position - raycastOrigin.transform.position, out hit))
            {
                igniteableObject.Ignite(hit.point);
            }

        }
    }
}
