using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour, ISwitchObserver
{
    public MeshRenderer lightMaterial;
    public Material onMaterial, offMaterial, deActiveMaterial;
    public enum LightState { Deactive, Off, On };
    public LightState lightState;

    void Start()
    {
        SwitchLightState(lightState);
    }

    private void SwitchLightState(LightState state)
    {
        switch (state)
        {
            case LightState.Deactive:
                lightMaterial.material = deActiveMaterial;
                break;
            case LightState.Off:
                lightMaterial.material = offMaterial;
                break;
            case LightState.On:
                lightMaterial.material = onMaterial;
                break;
            default:
                break;
        }
    }

    public void SwitchStateNotify(bool switchState)
    {
        if(switchState == true && lightState == LightState.Off)
        {
            lightState = LightState.On;
            SwitchLightState(lightState);
        } 
        else if (switchState == false && lightState == LightState.On)
        {
            lightState = LightState.Off;
            SwitchLightState(lightState);
        }
    }

    public void ActivateNotify()
    {
        if(lightState == LightState.Deactive)
        {
            lightState = LightState.Off;
            SwitchLightState(lightState);
        }
    }
}
