using UnityEngine;

public class HeldObject : ImageDisplayObject
{
    #region Variables Declaration

    public GameObject outlineObject;
    public bool highlighted;
    public bool playerInRange;
    public PlayerControl Player;
    public Rigidbody rb;
    public Transform objectParent;
    public bool onPlatform = false;
    public Transform platformTransform;

    #endregion

    #region State Machine Variables

    private HeldBaseState currentState;

    public HeldBaseState CurrentState
    {
        get { return currentState; }
    }

    //Concrete States
    public readonly HeldIdleState idleState = new HeldIdleState();
    public readonly HeldInRangeState inRangeState = new HeldInRangeState();
    public readonly HeldSelectedState selectedState = new HeldSelectedState();
    public readonly HeldPickedUpState pickedUpState = new HeldPickedUpState();

    #endregion

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        rb = gameObject.GetComponent<Rigidbody>();
        objectParent = gameObject.transform.parent;
        currentState = idleState;
        TransitionToState(currentState);
    }

    void Update()
    {
        currentState.Update(this);
    }

    public void TransitionToState(HeldBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "MovingPlatform":
                onPlatform = true;
                platformTransform = other.transform;
                if (currentState != pickedUpState)
                {
                    other.GetComponent<CarryObject>().RidePlatform(gameObject);
                }
                break;

            default:
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "MovingPlatform":
                onPlatform = false;
                platformTransform = null;
                other.GetComponent<CarryObject>().LeavePlatform(gameObject);
                break;

            default:
                break;
        }
    }

}



