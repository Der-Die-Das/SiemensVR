using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    public bool lit;
    public GameObject raycastOrigin;

	// Use this for initialization
	void Start () {
        lit = false;
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
