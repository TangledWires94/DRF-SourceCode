using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplayObject : MonoBehaviour
{

    public string displayText;
    public DisplayTextController displayTextController;

    public void InitialiseTextDisplay()
    {
        displayTextController = GameObject.Find("Canvas").GetComponent<DisplayTextController>();
    }

    public void ToggleDisplayText(bool state)
    {
        displayTextController.ToggleText(state);
    }

    public void UpdateDisplayText()
    {
        displayTextController.UpdateDisplayText(displayText);
    }
}
