using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloorSwitch : BooleanSwitchParent
{
    private Animator anim;

    public UnityEvent<Boolean> onSwitchchange;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void TriggerSwitch(bool state)
    {
        switchState = state;
        anim.SetBool("switchPressed?", state);
        //onSwitchchange.Invoke(state);
    }

    void OnCollisionEnter(Collision collision)
    {
        TriggerSwitch(true);
    }

    void OnCollisionExit(Collision collision)
    {
        TriggerSwitch(false);
    }
}
