using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;
using UnityEngine.UI;

//public delegate void DimensionSwapHandler(Vector3 positionOffset);

public class DimensionSwap : MonoBehaviour
{
    public Action DimensionSwapEvent;

    public List<Vector3> dimensionOffset = new List<Vector3>(2);
    public int numberOfDimensions = 2;
    public int currentDimension = 0;
    private int newDimension;
    
    public bool inSwapZone = false;
    public bool playerIsBlocked, playerWasBlocked = true;
    
    private GameObject player, objectHolder;
    private Vector3 newPosition;

    [SerializeField]
    public List<GameObject> swapObjects = new List<GameObject>();
    public List<Transform> rePickUpObjects = new List<Transform>();

    public Sprite swapFilled, swapEmpty;
    private Image swapImage;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        objectHolder = GameObject.Find("Object Holder");
        swapImage = GameObject.Find("Cursor").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Object Swapping

        if (inSwapZone)
        {
            //Check if player is blocked by an object from swapping dimensions
            playerIsBlocked = CheckPlayerBlocked();

            if (!playerIsBlocked && Input.GetKeyDown(KeyCode.Mouse1))
            {
                DimensionSwapAction();         
            }
        }
        else
        {
            playerIsBlocked = true;
        }

        #endregion

        #region UI Change

        //Continuously changing sprite slows game down, only change if the value of PlayerIsBlocked changed between frames
        if (playerIsBlocked != playerWasBlocked)
        {
            if (playerIsBlocked)
            {
                swapImage.sprite = swapEmpty;
            } else
            {
                swapImage.sprite = swapFilled;
            }
        }

        //Need to know if blocked state has changed so we don't need to continuously poll to change cursor sprite;
        playerWasBlocked = playerIsBlocked;

        #endregion
    }

    public void DimensionSwapAction()
    {
        //Detach any objects that are held by the player so they can be swapped/not swapped correctly
        DropHeldObjects();

        //Swap the player between dimensions
        DimensionSwapObject(player);

        //Swap any objects within the swap zones between dimensions
        foreach (GameObject swapObject in swapObjects)
        {
            DimensionSwapObject(swapObject);
        }

        //Pickup any objects that were dropped by the player and dimension swapped with them
        PickupObjects();

        //Finished swapping, current dimension is now the new dimension
        currentDimension = newDimension;
    }

    bool CheckPlayerBlocked()
    {
        //Currently only 2 dimensions so can determine which dimension to change to by seeing where we currently are
        if (currentDimension == 0)
        {
            newDimension = 1;
        }
        else
        {
            newDimension = 0;
        }

        //Calculate positional offset between current dimension and target dimension
        Vector3 positionChange = dimensionOffset[newDimension] - dimensionOffset[currentDimension];
        newPosition = player.transform.position + positionChange;

        //Check that the location the player wishes to swap to is clear of any objects, if the path is not clear set swapBlocked to true
        int layerMask = 1 << 9;
        layerMask = ~layerMask;
        Vector3 halfExtents = new Vector3(player.transform.localScale.x / 2, player.transform.localScale.y / 2, player.transform.localScale.z / 2);
        bool swapBlocked = Physics.CheckBox(newPosition, halfExtents, Quaternion.identity, layerMask, QueryTriggerInteraction.Ignore);
        return swapBlocked;
    }

    void DropHeldObjects()
    {
        //Clear list of objects to pickup after the swap has ocurred so that a new list can be built of only currently held objects
        rePickUpObjects.Clear();

        //Only run if player is holding an object, will likely only ever hold one but want to future proof in case it gets increased later
        if (objectHolder.transform.childCount > 0)
        {
            for (int i = 0; i < objectHolder.transform.childCount; i++)
            {
                //Get reference to each child object in turn and if they are in a swap zone add them to list of objects to be picked up after the swap happens
                Transform heldObject = objectHolder.transform.GetChild(i);
                if (swapObjects.Contains(heldObject.gameObject))
                {
                    rePickUpObjects.Add(heldObject);
                }

                //Held objects picked up state already has code to handle how objects should be dropped so use this
                HeldObject heldScript = heldObject.GetComponent<HeldObject>();
                heldScript.CurrentState.ObjectDropped(heldScript);
            }
        }
    }

    void PickupObjects()
    {
        //For each object dropped that is to be pickeup again after the swap use the function from the held object selected state to have player pickup object again
        foreach(Transform heldObject in rePickUpObjects)
        {
            //heldObject.parent = objectHolder.transform;
            HeldObject heldScript = heldObject.GetComponent<HeldObject>();
            heldScript.CurrentState.ObjectPickedUp(heldScript);
        }
    }

    void DimensionSwapObject(GameObject swappingObject)
    {
        //Translate the Game Object by the difference between the current and target dimensions
        Vector3 positionChange = dimensionOffset[newDimension] - dimensionOffset[currentDimension];
        swappingObject.transform.position = swappingObject.transform.position + positionChange; ;
    }

    public void AddObjectToList(GameObject addingObject)
    {
        //If object to be added isn't on the list add it to the list
        if (!swapObjects.Contains(addingObject))
        {
            swapObjects.Add(addingObject);
        }
    }

    public void RemoveObjectFromList(GameObject removingObject)
    {
        //If the object to be removed is on the list remove it
        if (swapObjects.Contains(removingObject))
        {
            swapObjects.Remove(removingObject);
        }
    }
}
