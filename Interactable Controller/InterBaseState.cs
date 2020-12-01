using UnityEngine;

public abstract class InterBaseState
{
    public abstract void EnterState(InteractableController interactable);
    public abstract void Update(InteractableController interactable);
    public abstract void PlayerInRangeUpdate(InteractableController interactable);
}
