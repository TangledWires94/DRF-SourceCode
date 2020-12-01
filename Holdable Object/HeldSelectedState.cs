using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldSelectedState : HeldBaseState
{
    public override void EnterState(HeldObject heldObject)
    {
        heldObject.outlineObject.SetActive(true);
        if (heldObject.displayImage != null)
        {
            heldObject.ChangeSprite();
            heldObject.ToggleImage(true);
        }
    }

    public override void Update(HeldObject heldObject)
    {
        if (!heldObject.playerInRange)
        {
            if(heldObject.displayImage != null)
            {
                heldObject.ToggleImage(false);
            }
            heldObject.TransitionToState(heldObject.idleState);
        }
        else
        {
            RaycastHit hit;
            int layerMask = 1 << 10;
            bool mouseOver = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, layerMask, QueryTriggerInteraction.Ignore);
            if (mouseOver && hit.transform.name == heldObject.name)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    ObjectPickedUp(heldObject);
                }
            } else
            {
                if (heldObject.displayImage != null)
                {
                    heldObject.ToggleImage(false);
                }
                heldObject.TransitionToState(heldObject.inRangeState);
            }
        }
    }

    public override void ObjectDropped(HeldObject droppedObject)
    {
        throw new System.NotImplementedException();
    }

    public override void ObjectPickedUp(HeldObject pickedUpObject)
    {
        pickedUpObject.outlineObject.SetActive(false);
        pickedUpObject.rb.constraints = RigidbodyConstraints.FreezeRotation;
        pickedUpObject.rb.useGravity = false;
        pickedUpObject.Player.PickupObject(pickedUpObject.gameObject);
        pickedUpObject.TransitionToState(pickedUpObject.pickedUpState);
    }
}
