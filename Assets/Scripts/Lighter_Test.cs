using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter_Test : MonoBehaviour {
    public ParticleSystem flame;

	// Use this for initialization
	void Start () {
        flame.Stop();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartParticleSystem()
    {
        flame.GetComponent<Fire_Test>().lit = true;
        flame.Play();
    }

    public void StopParticleSystem()
    {
        flame.GetComponent<Fire>().lit = false;
        flame.Stop();
    }
}
