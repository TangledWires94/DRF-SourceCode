using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldPickedUpState : HeldBaseState
{

    public override void EnterState(HeldObject heldObject)
    {

    }

    public override void Update(HeldObject heldObject)
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ObjectDropped(heldObject);
        }
    }
    
    public override void ObjectDropped(HeldObject droppedObject)
    {
        droppedObject.rb.constraints = RigidbodyConstraints.None;
        droppedObject.rb.useGravity = true;
        droppedObject.Player.DropObject(droppedObject.gameObject);
        droppedObject.TransitionToState(droppedObject.selectedState);
    }

    public override void ObjectPickedUp(HeldObject pickedUpObject)
    {
        throw new System.NotImplementedException();
    }
}
