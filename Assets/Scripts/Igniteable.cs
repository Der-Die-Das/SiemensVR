using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Igniteable : MonoBehaviour
{
    public GameObject firePrefab;
    private ParticleSystem[] fire;
    public float BoxColliderResizeFactor;
    public int maxFireAmount = 1;
    private int igniteCounter = 0;

    // Use this for initialization
    void Start()
    {
        fire = new ParticleSystem[maxFireAmount];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Ignite(Vector3 position)
    {
        if (igniteCounter < fire.Length)
        {
            fire[igniteCounter] = Instantiate(firePrefab, transform).GetComponent<ParticleSystem>();
            fire[igniteCounter].transform.position = position;
            //fire.transform.localScale = new Vector3(1f / transform.position.x, 1f / transform.position.y, 1f / transform.position.z);
            BoxCollider boxCollider = fire[igniteCounter].GetComponent<BoxCollider>();
            boxCollider.size = new Vector3(
                boxCollider.size.x / (transform.localScale.x * BoxColliderResizeFactor),
                boxCollider.size.y / (transform.localScale.y * BoxColliderResizeFactor),
                boxCollider.size.z / (transform.localScale.z * BoxColliderResizeFactor));

            fire[igniteCounter].GetComponent<BoxCollider>().center = Vector3.zero;
            fire[igniteCounter].GetComponent<Fire>().lit = true;
            fire[igniteCounter].Play();
            igniteCounter++;
        }
    }
}
