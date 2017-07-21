using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSpawner : MonoBehaviour
{
    public GameObject screenPrefab;
    public Vector3 spawnOffset;
    private TutorialScreen screen;
    private bool spawned = false;


    [ContextMenu("Spawn")]
    private void Spawn()
    {
        spawned = true;
        screen = Instantiate(screenPrefab).GetComponent<TutorialScreen>();
        screen.transform.position = transform.position + spawnOffset;
    }

    private void OnCollisionEnter(Collision collision)
    {if (!spawned)
        {
            Spawn();
            Destroy(gameObject);
        }
    }
}
