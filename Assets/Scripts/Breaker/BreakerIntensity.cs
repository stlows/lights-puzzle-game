using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BreakerIntensity : Breaker
{
    public Transform[] lightTransforms;
    protected List<Light> associatedLights = new List<Light>();
    public float openIntensity = 2f;
    public float closeIntensity = 0f;


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
