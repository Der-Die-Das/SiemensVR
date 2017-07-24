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

        Vector3 rot = GameObject.FindObjectOfType<SteamVR_Camera>().transform.rotation.eulerAngles;

        rot.x = 0;
        rot.z = 0;
        rot.y -= 90;

        screen.transform.rotation = Quaternion.Euler(rot);
    }

    private void OnCollisionEnter(Collision collision)
    {if (!spawned)
        {
            Spawn();
            Destroy(gameObject);
        }
    }
}
