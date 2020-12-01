using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterPlayerCloseState : InterBaseState
{
    public override void EnterState(InteractableController interactable)
    {
        interactable.outlineObject.SetActive(false);
    }

    public override void PlayerInRangeUpdate(InteractableController interactable)
    {
        interactable.TransitionToState(interactable.idleState);
    }

    public override void Update(InteractableController interactable)
    {
        RaycastHit hit;
        int layerMask = 1 << 10;
        bool mouseOver = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, layerMask, QueryTriggerInteraction.Ignore);
        if (mouseOver && hit.transform.name == interactable.name)
        {
            interactable.TransitionToState(interactable.activeState);
        }
    }
}
