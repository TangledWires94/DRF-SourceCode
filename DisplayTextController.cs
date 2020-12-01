using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DisplayTextController : MonoBehaviour
{
    private TextMeshProUGUI displayText;
    
    // Start is called before the first frame update
    void Start()
    {
        displayText = GameObject.Find("Display Text").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateDisplayText(string newText)
    {
        displayText.text = newText;
    }

    public void ToggleText(bool state)
    {
        displayText.enabled = state;
    }


}
