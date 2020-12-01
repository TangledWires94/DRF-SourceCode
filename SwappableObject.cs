using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwappableObject : MonoBehaviour
{
    private DimensionSwap dimensionSwaphandler;

    // Start is called before the first frame update
    void Start()
    {
        dimensionSwaphandler = GameObject.Find("Dimension Swap Handler").GetComponent<DimensionSwap>();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "SwapZone")
        {
            dimensionSwaphandler.RemoveObjectFromList(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SwapZone" && !dimensionSwaphandler.swapObjects.Contains(gameObject))
        {
            dimensionSwaphandler.AddObjectToList(gameObject);
        }
    }


}
