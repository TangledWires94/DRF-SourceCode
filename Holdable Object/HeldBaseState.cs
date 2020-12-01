using UnityEngine;

public abstract class HeldBaseState
{
    public abstract void EnterState(HeldObject heldObject);
    public abstract void Update(HeldObject heldObject);
    public abstract void ObjectDropped(HeldObject droppedObject);
    public abstract void ObjectPickedUp(HeldObject pickedUpObject);
}
