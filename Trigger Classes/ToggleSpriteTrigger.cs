using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSpriteTrigger : TriggerParent
{
    public Image UIImage;
    public Sprite onSprite, offSprite;
    public bool disabledWhenOff;

    public override void EnterTriggerAction()
    {
        UIImage.sprite = onSprite;
        if (!disabledWhenOff)
        {
            UIImage.enabled = true;
        }
    }

    public override void ExitTriggerAction()
    {
        UIImage.sprite = offSprite;
        if (!disabledWhenOff)
        {
            UIImage.enabled = false;
        }
        if (destroyOnLeave)
        {
            DestroySelf();
        }
    }

    public override void StayTriggerAction()
    {

    }
}
