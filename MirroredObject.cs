using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MirroredObject : MonoBehaviour
{
    public bool textObject;
    
    Rigidbody rb;
    public string objectToMirror;
    public Transform originalObject, mainCamera;
    public GameObject Mirror;
    Vector3 reflectPointX, reflectPointZ, distanceBetween, newPos, originalRotation, mirroredRotation;
    float angle, grad, offset, newX, newZ;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        if(this.GetComponent<Text>() == null)
        {
            textObject = false;
        } else
        {
            textObject = true;
        }
        originalObject = GameObject.Find(objectToMirror).GetComponent<Rigidbody>().transform;
        mainCamera = GameObject.Find("Player Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //EQUATION OF LINE
        //Get current angle of mirror
        angle = 360 - Mirror.GetComponent<Rigidbody>().rotation.eulerAngles.y;
        
        //Find gradient of mirror
        grad = Mathf.Tan(Mathf.Deg2Rad * angle);

        //Find y-intercept of mirror i.e. c = y - mx
        offset = Mirror.GetComponent<Rigidbody>().position.z - (grad * Mirror.GetComponent<Rigidbody>().position.x);

        //POSITION
        //Calculate reflected X and Z values of originalObject in mirror described by y = mx + c
        newX = CalculateNewX(originalObject.position.x, originalObject.position.z, grad, offset);
        newZ = CalculateNewZ(originalObject.position.x, originalObject.position.z, grad, offset);

        //Calculate new position
        newPos = new Vector3(newX, originalObject.position.y, newZ);

        //Set new position
        rb.position = newPos;

        //ROTATION
        //Get originalObject rotation in x,y,z angles
        originalRotation = originalObject.rotation.eulerAngles;

        //Adjust angle based on orientation of mirror
        if(objectToMirror == "Player")
        {
            mirroredRotation = new Vector3(Mirror.transform.rotation.eulerAngles.x - mainCamera.rotation.eulerAngles.x, Mirror.transform.rotation.eulerAngles.y - originalObject.rotation.eulerAngles.y, Mirror.transform.rotation.eulerAngles.z - originalObject.rotation.eulerAngles.z);
        } else
        {
            mirroredRotation = new Vector3(Mirror.transform.rotation.eulerAngles.x - originalObject.rotation.eulerAngles.x, Mirror.transform.rotation.eulerAngles.y - originalObject.rotation.eulerAngles.y, Mirror.transform.rotation.eulerAngles.z - originalObject.rotation.eulerAngles.z);
        }
        
        //Set new rotation
        rb.rotation = Quaternion.Euler(mirroredRotation.x, mirroredRotation.y, mirroredRotation.z);

    }

    float CalculateNewX(float x, float z, float m, float c)
    {
        float numerator = ((1 - (m * m)) * x) + (2 * m * z) - (2 * m * c);
        float denominator = (m * m) + 1;
        float u = numerator / denominator;
        return u;
    }

    float CalculateNewZ(float x, float z, float m, float c)
    {
        float numerator = (((m * m) - 1) * z) + (2 * m * x) + (2 * c);
        float denominator = (m * m) + 1;
        float v = numerator / denominator;
        return v;
    }


}
