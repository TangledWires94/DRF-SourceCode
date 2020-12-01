using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class InteractableController : ImageDisplayObject
{
    #region Variable Declaration

    public BoolEvent OnSwitchChange;
    public InteractableEvent LayoutSwitchEvent;

    public bool switchOn = false;
    public bool reSwitch, startDeactive;
    public GameObject outlineObject;

    #endregion

    #region State Machine Variables

    private InterBaseState currentState;

    public InterBaseState CurrentState
    {
        get { return currentState; }
    }

    //Concrete States
    public readonly InterIdleState idleState = new InterIdleState();
    public readonly InterPlayerCloseState playerCloseState = new InterPlayerCloseState();
    public readonly InterActiveState activeState = new InterActiveState();
    public readonly InterDeactiveState deActiveState = new InterDeactiveState();

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (startDeactive)
        {
            currentState = deActiveState;
        } else
        {
            currentState = idleState;
        }
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Update(this);
    }

    #region Public Methods

    public void SetInRange(bool playerInRange)
    {
        currentState.PlayerInRangeUpdate(this);
    }

    public void TransitionToState(InterBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void ToggleSwitchState()
    {
        if (switchOn)
        {
            if (reSwitch)
            {
                switchOn = false;
                OnSwitchChange.Invoke(switchOn);
            }
        } else
        {
            switchOn = true;
            OnSwitchChange.Invoke(switchOn);
            LayoutSwitchEvent.Invoke(this);
        }
    }

    public void ActivateSwitch()
    {
        currentState = idleState;
        currentState.EnterState(this);
    }

    #endregion

}

[System.Serializable]
public class BoolEvent : UnityEvent<bool>
{
}

[System.Serializable]
public class InteractableEvent : UnityEvent<InteractableController>
{
}
