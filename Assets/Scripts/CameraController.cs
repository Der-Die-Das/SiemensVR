using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 10f;
    public float mouseSensivity = 5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 mouseMove = new Vector2(Input.GetAxis("Mouse Y") * -1f, Input.GetAxis("Mouse X"));
        Vector2 newRotation = mouseMove * mouseSensivity * Time.deltaTime;


        Vector3 localRotation = new Vector3(newRotation.x, newRotation.y, 0f);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + localRotation);

        Vector3 translate = Vector3.zero;
        translate += transform.forward * Input.GetAxis("Vertical");
        translate += transform.right * Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.Space))
        {
            translate += transform.up * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            translate += transform.up * -speed * Time.deltaTime;
        }

        transform.position += (translate * speed * Time.deltaTime);
        Debug.DrawRay(transform.position, translate * 100f, Color.red);


    }
}
