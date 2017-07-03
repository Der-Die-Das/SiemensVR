using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour {

    public float yRot = 1f;
    public float zRot = 1f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(zRot, 0f, yRot), Space.Self);
        //transform.localRotation = Quaternion.Euler(new Vector3(zRot, yRot, 0f)+ transform.localRotation.eulerAngles);
        //Debug.Log(transform.localRotation.eulerAngles);
	}

}
