using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLayoutController : MonoBehaviour
{
    public List<GameObject> Layouts;
    public List<GameObject> Switches;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject layout in Layouts)
        {
            layout.SetActive(false);
        }
        foreach (GameObject layoutSwitch in Switches)
        {
            layoutSwitch.GetComponentInChildren<LightControl>().SwitchStateNotify(false);
            layoutSwitch.GetComponentInChildren<InteractableController>().switchOn = false;
        }
    }

    private void ChangeLayout(int layoutNumber)
    {
        for(int i = 0; i < Layouts.Count; i++)
        {
            Layouts[i].SetActive(i == layoutNumber - 1);
            Switches[i].GetComponentInChildren<LightControl>().SwitchStateNotify(i == layoutNumber - 1);
            Switches[i].GetComponentInChildren<InteractableController>().switchOn = i == layoutNumber - 1;
        }
    }

    public void UpdateLayout(InteractableController controller)
    {
        int number;
        int.TryParse(controller.name.Substring(7, 1), out number);
        ChangeLayout(number);
    }
}
