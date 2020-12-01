using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterDeactiveState : InterBaseState
{
    public override void EnterState(InteractableController interactable)
    {
        interactable.outlineObject.SetActive(false);
    }

    public override void PlayerInRangeUpdate(InteractableController interactable)
    {

    }

    public override void Update(InteractableController interactable)
    {
        //Intentionally blank as this script requires an Update() method but there is no behaviour to be implemented
    }
}
