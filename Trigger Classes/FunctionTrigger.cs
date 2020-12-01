using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FunctionTrigger : TriggerParent
{
    public UnityEvent EnterEvent, ExitEvent, StayEvent;

    public override void EnterTriggerAction()
    {
        if(EnterEvent != null)
        {
            EnterEvent.Invoke();
        }
    }

    public override void ExitTriggerAction()
    {
        if (EnterEvent != null)
        {
            ExitEvent.Invoke();
        }
    }

    public override void StayTriggerAction()
    {
        if (EnterEvent != null)
        {
            StayEvent.Invoke();
        }
    }
}
