using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Igniteable_Test : MonoBehaviour
{
    public ParticleSystem fire;
    public GameObject firePrefab;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IgniteFire(Vector3 position)
    {
        fire = Instantiate(firePrefab, transform).GetComponent<ParticleSystem>();
        fire.transform.position = position;
        fire.transform.localScale = new Vector3(1f / transform.localScale.x, 1f / transform.localScale.y, 1f / transform.localScale.z) * 4f;
        fire.transform.rotation = Quaternion.Euler(fire.transform.rotation.eulerAngles + (Vector3.right * -90f));
        fire.Play();
        fire.GetComponent<Fire_Test>().lit = true;
    }

    public void StopFire()
    {
        fire.Stop();
        fire.GetComponent<Fire_Test>().lit = false;
    }
}
