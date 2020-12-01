using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldInRangeState : HeldBaseState
{
    public override void EnterState(HeldObject heldObject)
    {
        heldObject.outlineObject.SetActive(false);
    }

    public override void Update(HeldObject heldObject)
    {
        if (!heldObject.playerInRange)
        {
            heldObject.TransitionToState(heldObject.idleState);
        }
        else
        {
            RaycastHit hit;
            int layerMask = 1 << 10;
            bool mouseOver = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, layerMask, QueryTriggerInteraction.Ignore);
            if (mouseOver && hit.transform.name == heldObject.name)
            {
                heldObject.TransitionToState(heldObject.selectedState);
            }
        }
    }

    public override void ObjectDropped(HeldObject droppedObject)
    {
        throw new System.NotImplementedException();
    }

    public override void ObjectPickedUp(HeldObject pickedUpObject)
    {
        throw new System.NotImplementedException();
    }
}
