using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Igniteable : MonoBehaviour
{
    public GameObject firePrefab;
    private List<ParticleSystem> fires;
    public float BoxColliderResizeFactor;
    public int maxFires = 1;

    // Use this for initialization
    void Start()
    {
        fires = new List<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    [ContextMenu("Ignite")]
    public void Ignite(Vector3 position)
    {
        if (fires.Count < maxFires)
        {
            ParticleSystem fire = Instantiate(firePrefab, transform).GetComponent<ParticleSystem>();
            fire.transform.position = position;
            //fire.transform.localScale = new Vector3(1f / transform.position.x, 1f / transform.position.y, 1f / transform.position.z);
            fire.GetComponent<BoxCollider>().size = new Vector3(fire.GetComponent<BoxCollider>().size.x / (transform.localScale.x * BoxColliderResizeFactor), fire.GetComponent<BoxCollider>().size.y / (transform.localScale.y * BoxColliderResizeFactor), fire.GetComponent<BoxCollider>().size.z / (transform.localScale.z * BoxColliderResizeFactor));
            fire.GetComponent<BoxCollider>().center = Vector3.zero;
            fire.GetComponent<Fire>().lit = true;
            fire.transform.Find("Light").localPosition = Vector3.zero;
            fire.Play();
            fires.Add(fire);
        }
    }
}
