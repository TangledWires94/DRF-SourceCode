using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneOffFunctions : MonoBehaviour
{
    public void SwapDimensions()
    {
        Debug.Log("Trigger");
        StartCoroutine(SwapDimensionsCoroutine());
    }

    public IEnumerator SwapDimensionsCoroutine()
    {
        Debug.Log("Trigger coroutine");

        DimensionSwap dimensionSwapHandler = GameObject.Find("Dimension Swap Handler").GetComponent<DimensionSwap>();

        dimensionSwapHandler.DimensionSwapAction();
        yield return new WaitForSeconds(3.0f);
        dimensionSwapHandler.DimensionSwapAction();
    }
}
