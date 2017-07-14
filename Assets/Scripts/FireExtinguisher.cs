using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour {
    public ParticleSystem water;
    private List<Fire> firesToExtinguish;
    public int firesNeededToActivate;
    public float durationInSeconds;
    private bool activated = true;
    private int fireCounter = 0;


	// Use this for initialization
	void Start () {
        firesToExtinguish = new List<Fire>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetActivated(bool pactivated)
    {
        activated = pactivated; 
    }

    public bool GetActivated()
    {
        return activated;
    }

    private void StartExtinguishFire()
    {
        water.Play();
    }

    private void StopExtinguishFire()
    {
        water.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(activated)
        //if (other.gameObject.GetComponent<Fire>())
        //{
        //    fireCounter++;

        //    if(fireCounter == firesNeededToActivate)
        //    {
        //        StartExtinguishFire();
        //    }
        //}
        Fire fire = other.gameObject.GetComponent<Fire>();
        if (fire && !fire.GetComponentInParent<Lighter>())
        {
            firesToExtinguish.Add(fire);
            fireCounter++;

            if(fireCounter >= firesNeededToActivate)
            {
                StartExtinguishFire();
                foreach (Fire item in firesToExtinguish)
                {
                    if(item != null)
                        Destroy(item.gameObject);
                }
                fireCounter = 0;
                StartCoroutine(WaitAndStopParticleSystem());
            }
        }
    }

    private IEnumerator WaitAndStopParticleSystem()
    {
        //suspend execution for certain amount of time
        yield return new WaitForSeconds(durationInSeconds);
        StopExtinguishFire();
    }
}
