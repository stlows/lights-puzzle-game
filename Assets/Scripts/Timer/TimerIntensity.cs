using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Environments;
using UnityEngine;

public class TimerIntensity : Timer
{
    public float openIntensity = 2f; 
    public float closeIntensity = 0f;

    public override void CloseSpecific()
    {
        foreach (Light light in associatedLights)
        {
            light.intensity = closeIntensity;
        }
    }

    public override void OpenSpecific()
    {
        foreach (Light light in associatedLights)
        {
            light.intensity = openIntensity;
        }
    }
}
