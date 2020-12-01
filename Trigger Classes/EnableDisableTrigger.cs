using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableTrigger : TriggerParent
{
    public GameObject ObjectToToggle;
    public bool onTriggerEnable;

    public override void EnterTriggerAction()
    {
        ObjectToToggle.SetActive(onTriggerEnable);
    }

    public override void ExitTriggerAction()
    {

    }

    public override void StayTriggerAction()
    {

    }

}
