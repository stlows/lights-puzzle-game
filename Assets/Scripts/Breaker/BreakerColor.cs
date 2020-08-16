using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Environments;
using UnityEngine;
 
public class BreakerColor : Breaker
{
    public Transform[] lightTransforms;
    protected List<Light> associatedLights = new List<Light>();
    public Color openColor = Color.white;
    public Color closeColor = Color.black;

    private void Start()
    {
        // Extract Light components from provided Light transforms
        foreach (Transform lightTransform in lightTransforms)
        {
            Light light = lightTransform.Find("Rotating").Find("Light").gameObject.GetComponent<Light>();
            associatedLights.Add(light);
        }
        BreakerStart();
    }

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
