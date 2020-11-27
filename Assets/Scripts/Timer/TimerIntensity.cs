using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerIntensity : Timer
{
    public float openIntensity = 2f; 
    public float closeIntensity = 0f;

    public void Awake()
    {
        ledUpMat = transform.Find("SphereUp").GetComponent<Renderer>().material;
        ledDownMat = transform.Find("SphereDown").GetComponent<Renderer>().material;
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
