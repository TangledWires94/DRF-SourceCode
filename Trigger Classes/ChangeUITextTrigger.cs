using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeUITextTrigger : TriggerParent
{
    public string displayText;
    public DisplayTextController displayTextController;


    // Start is called before the first frame update
    public void Start()
    {
        displayTextController = GameObject.Find("Canvas").GetComponent<DisplayTextController>();
    }

    public override void EnterTriggerAction()
    {
        displayTextController.UpdateDisplayText(displayText);
        displayTextController.ToggleText(true);
    }

    public override void ExitTriggerAction()
    {
        displayTextController.ToggleText(false);
        displayTextController.UpdateDisplayText(null);
        if (destroyOnLeave)
        {
            DestroySelf();
        }
    }

    public override void StayTriggerAction()
    {

    }
}
