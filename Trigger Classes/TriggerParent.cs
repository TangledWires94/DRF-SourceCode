using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerParent : MonoBehaviour
{
    public bool destroyOnLeave = false;

    virtual public void EnterTriggerAction()
    {
        Debug.Log("Trigger parent called - Enter");
    }

    virtual public void ExitTriggerAction()
    {
        Debug.Log("Trigger parent called - Exit");
        if (destroyOnLeave)
        {
            DestroySelf();
        }
    }

    virtual public void StayTriggerAction()
    {
        Debug.Log("Trigger parent called - Stay");
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
