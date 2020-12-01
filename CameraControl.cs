using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Rigidbody rb;
    public float rotateSpeed;
    float xMove, zMove, xRotation, yRotation;
    bool toggleRotate = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            toggleRotate = !toggleRotate;
        }
        if (toggleRotate)
        {
            //Rotate camera
            yRotation = Input.GetAxis("Mouse Y") * rotateSpeed;
            gameObject.transform.Rotate(-yRotation, 0, 0, Space.Self);
            gameObject.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
        }
        

    }
}
