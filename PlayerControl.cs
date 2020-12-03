using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody rb;
    private float xRotation, horizontalMovement, verticalMovement;
    public float moveSpeed, rotateSpeed, jumpForce;

    private Transform playerCamera;
    private bool toggleRotate = true;

    public bool onFloor;
    private DimensionSwap dimensionSwapHandler;

    public GameObject objectHolder;
    private Transform objectParent;

    private bool cursorLocked;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        rb = gameObject.GetComponent<Rigidbody>();
        playerCamera = GameObject.Find("Player Camera").GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
        cursorLocked = false;
        dimensionSwapHandler = GameObject.Find("Dimension Swap Handler").GetComponent<DimensionSwap>();
    }

    // Update is called once per frame
    void Update()
    {            
        //Move player
        PlayerMove();

        //Jumping
        if (Input.GetButtonDown("Jump") && onFloor)
        {
            PlayerJump();
        }

        if (toggleRotate)
        {
            RotatePlayer();
        }

        //Unlocking cursor so you can quit the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            cursorLocked = true;
        }

        //Debug option to allow for camera rotation to be paused whilst editing values in inspector
        if (Input.GetButtonDown("Pause"))
        {
           ToggleRotation();
        }
    }

    #region Private Methods

    private void PlayerMove()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontalMovement) > Mathf.Epsilon)
        {
            horizontalMovement = horizontalMovement * Time.deltaTime * moveSpeed;
        }
        else
        {
            horizontalMovement = 0f;
        }

        if (Mathf.Abs(verticalMovement) > Mathf.Epsilon)
        {
            verticalMovement = verticalMovement * Time.deltaTime * moveSpeed;
        }
        else
        {
            verticalMovement = 0f;
        }

        transform.Translate(-verticalMovement, 0, horizontalMovement, Space.Self);
    }

    private void PlayerJump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void RotatePlayer()
    {
        xRotation = Input.GetAxis("Mouse X") * rotateSpeed;
        rb.transform.Rotate(0, xRotation, 0, Space.Self);
        rb.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
    }

    private void ToggleRotation()
    {
        toggleRotate = !toggleRotate;
        if (toggleRotate)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    #endregion

    #region Public Methods

    public void PickupObject(GameObject heldObject)
    {
        if(heldObject.transform.parent != null)
        {
            if (heldObject.transform.parent.tag == "MovingPlatform")
            {
                heldObject.GetComponentInParent<CarryObject>().LeavePlatform(heldObject);
            }
        }
        heldObject.transform.parent = objectHolder.transform;
        objectHolder.GetComponent<SpringJoint>().connectedBody = heldObject.GetComponent<Rigidbody>();
    }

    public void DropObject(GameObject heldObject)
    {
        objectHolder.GetComponent<SpringJoint>().connectedBody = null;
        if(heldObject.GetComponent<HeldObject>().onPlatform)
        {
            heldObject.transform.parent = heldObject.GetComponent<HeldObject>().platformTransform;
        } else
        {
            heldObject.transform.parent = heldObject.GetComponent<HeldObject>().objectParent;
        }
        objectParent = null;
    }

    #endregion


    #region Trigger Handlers

    void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "Floor":
                onFloor = false;
                break;

            case "Door":
                other.GetComponent<DoorController>().OpenCloseDoor(false);
                break;

            case "Interactable":
                other.GetComponent<InteractableController>().SetInRange(false);
                break;

            case "Holdable":
                other.GetComponentInParent<HeldObject>().playerInRange = false;
                break;

            case "Trigger":
                other.GetComponent<TriggerParent>().ExitTriggerAction();
                break;

            case "DimensionSwap":
                dimensionSwapHandler.inSwapZone = false;
                break;

            case "MovingPlatform":
                onFloor = false;
                other.GetComponent<CarryObject>().LeavePlatform(gameObject);
                break;

            default:
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Floor":
                onFloor = true;
                break;

            case "Door":
                other.GetComponent<DoorController>().OpenCloseDoor(true);
                break;

            case "Interactable":
                other.GetComponent<InteractableController>().SetInRange(true);
                break;

            case "Holdable":
                other.GetComponentInParent<HeldObject>().playerInRange = true;
                break;

            case "Trigger":
                other.GetComponent<TriggerParent>().EnterTriggerAction();
                break;

            case "DimensionSwap":
                break;

            case "MovingPlatform":
                onFloor = true;
                other.GetComponent<CarryObject>().RidePlatform(gameObject);
                break;

            default:
                break;
        }
    }

    void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case "Floor":
                onFloor = true;
                break;

            case "Door":
                other.GetComponent<DoorController>().OpenCloseDoor(true);
                break;

            case "Trigger":
                other.GetComponent<TriggerParent>().StayTriggerAction();
                break;

            case "DimensionSwap":
                dimensionSwapHandler.inSwapZone = true;
                break;

            case "MovingPlatform":
                onFloor = true;
                break;

            default:
                break;
        }

    }

    #endregion

}
