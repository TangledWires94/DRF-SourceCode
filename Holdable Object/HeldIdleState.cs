using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldIdleState : HeldBaseState
{
    public override void EnterState(HeldObject heldObject)
    {
        heldObject.outlineObject.SetActive(false);
    }

    public override void Update(HeldObject heldObject)
    {
        if (heldObject.playerInRange)
        {
            heldObject.TransitionToState(heldObject.inRangeState);
        }
    }

    public override void ObjectDropped(HeldObject droppedObject)
    {
        //throw new System.NotImplementedException();
    }

    public override void ObjectPickedUp(HeldObject pickedUpObject)
    {
        //throw new System.NotImplementedException();
    }
}
