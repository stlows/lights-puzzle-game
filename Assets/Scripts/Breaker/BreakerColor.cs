using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Environments;
using UnityEngine;
 
public class BreakerColor : Breaker
{
    public Color openColor = Color.white;
    public Color closeColor = Color.black;
    public override void CloseSpecific()
    {
        foreach (Light light in associatedLights)
        {
            light.color = closeColor;
        }
    }

    public override void OpenSpecific()
    {
        foreach (Light light in associatedLights)
        {
            light.color = openColor;
        }
    }
}
