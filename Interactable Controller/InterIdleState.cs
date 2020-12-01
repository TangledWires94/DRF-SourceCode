using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterIdleState : InterBaseState
{
    public override void EnterState(InteractableController interactable)
    {
        interactable.outlineObject.SetActive(false);
    }

    public override void PlayerInRangeUpdate(InteractableController interactable)
    {
        interactable.TransitionToState(interactable.playerCloseState);
    }

    public override void Update(InteractableController interactable)
    {

    }
}
