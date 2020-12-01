using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryObject : MonoBehaviour
{

    Dictionary<string, Transform> objectParents = new Dictionary<string, Transform>();

    // Start is called before the first frame update
    void Start()
    {
        objectParents.Clear();
    }

    public void RidePlatform(GameObject rider)
    {
        if (!objectParents.ContainsKey(rider.name))
        {
            objectParents.Add(rider.name, rider.transform.parent);
            rider.transform.parent = transform;
        }
    }

    public void LeavePlatform(GameObject rider)
    {
        if (objectParents.ContainsKey(rider.name))
        {
            rider.transform.parent = objectParents[rider.name];
            objectParents.Remove(rider.name);
        }
    }
}
