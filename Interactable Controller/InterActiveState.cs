using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterActiveState : InterBaseState
{
    public override void EnterState(InteractableController interactable)
    {
        interactable.outlineObject.SetActive(true);
        if(interactable.displayImage != null)
        {
            interactable.ToggleImage(true);
            interactable.ChangeSprite();
        }
    }

    public override void PlayerInRangeUpdate(InteractableController interactable)
    {
        if (interactable.displayImage != null)
        {
            interactable.ToggleImage(false);
        }
        interactable.TransitionToState(interactable.idleState);
    }

    public override void Update(InteractableController interactable)
    {
        RaycastHit hit;
        int layerMask = 1 << 10;
        bool mouseOver = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, layerMask, QueryTriggerInteraction.Ignore);
        if (!mouseOver)
        {
            if (interactable.displayImage != null) 
            {
                interactable.ToggleImage(false);
                interactable.TransitionToState(interactable.playerCloseState);
            }

        } else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                interactable.ToggleSwitchState();
            }
        }
    }
}
