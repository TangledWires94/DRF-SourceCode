using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    private Animator anim;
    private bool doorOpen = false;
    public bool doorLocked;

    public List<LightControl> LightControls;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void OpenCloseDoor(bool state)
    {
        if (!doorLocked)
        {
            anim.SetBool("DoorOpen", state);
            doorOpen = state;
        }
    }

    public void UnlockDoor(bool locked)
    {
        doorLocked = !locked;
    }

    public void CheckUnlock()
    {
        int numberSwitched = 0;
        foreach (LightControl lightControl in LightControls)
        {
            if(lightControl.lightState == LightControl.LightState.On)
            {
                numberSwitched++;
            }
        }
        if (numberSwitched >= LightControls.Count)
        {
            doorLocked = true;
        }
        Debug.Log(numberSwitched);
    }
}
