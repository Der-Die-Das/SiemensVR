using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Test : MonoBehaviour {
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
        Igniteable_Test igniteableObject = other.GetComponent<Igniteable_Test>();
        if (igniteableObject && lit)
        {
            RaycastHit hit;
            if (Physics.Raycast(raycastOrigin.transform.position, other.gameObject.transform.position - raycastOrigin.transform.position, out hit))
            {
                //Debug.DrawRay(raycastOrigin.transform.position, hit.point - raycastOrigin.transform.position, Color.red);
                //Debug.Log(hit.point.ToString());

                igniteableObject.IgniteFire(hit.point);
            }
        }
    }
}
