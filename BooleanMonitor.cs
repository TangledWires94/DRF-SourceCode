using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BooleanMonitorEvent : UnityEvent<bool>
{
}

public class BooleanMonitor : MonoBehaviour
{
    public List<BooleanSwitchParent> monitoredSwitches = new List<BooleanSwitchParent>();
    private bool allSwitchesOn = false;

    public BooleanMonitorEvent switchStateChange;

    // Update is called once per frame
    void Update()
    {
        if(monitoredSwitches.Count != 0)
        {
            if(allSwitchesOn != CheckSwitches())
            {
                allSwitchesOn = !allSwitchesOn;
                switchStateChange.Invoke(allSwitchesOn);
            }
        }
    }

    bool CheckSwitches()
    {
        int i, j = 0;
        for(i = 0; i < monitoredSwitches.Count; i++)
        {
            if (monitoredSwitches[i].switchState)
            {
                j++;
            }
        }

        return i == j;
    }
}
