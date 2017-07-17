using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour {
    public ParticleSystem water;
    private List<Fire> firesToExtinguish;
    public int firesNeededToActivate;
    public float durationInSeconds;
    private bool activated = true;


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
        checkForFireAndExtinguishThem();
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
        Fire fire = other.gameObject.GetComponent<Fire>();
        if (fire && !fire.GetComponentInParent<Lighter>())
        {
            firesToExtinguish.Add(fire);
            checkForFireAndExtinguishThem();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Fire fire = other.gameObject.GetComponent<Fire>();
        if (firesToExtinguish.Contains(fire))
        {
            firesToExtinguish.Remove(fire);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Fire fire = other.gameObject.GetComponent<Fire>();
        if (fire && !fire.GetComponentInParent<Lighter>() && !firesToExtinguish.Contains(fire))
        {
            checkForFireAndExtinguishThem();
        }
    }

    private void checkForFireAndExtinguishThem()
    {
        if (firesToExtinguish.Count >= firesNeededToActivate && activated)
        {
            StartExtinguishFire();
            foreach (Fire item in firesToExtinguish)
            {
                if (item != null)
                    Destroy(item.gameObject);
            }
            StartCoroutine(WaitAndStopParticleSystem());
        }
    }

    private IEnumerator WaitAndStopParticleSystem()
    {
        //suspend execution for certain amount of time
        yield return new WaitForSeconds(durationInSeconds);
        StopExtinguishFire();
    }
}
