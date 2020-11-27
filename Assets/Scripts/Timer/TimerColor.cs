using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerColor: Timer
{
      
    public Color openColor = Color.white;
    public Color closeColor = Color.black;

    public void Awake()
    {
        ledUpMat= transform.Find("SphereUp").GetComponent<Renderer>().material;
        ledDownMat= transform.Find("SphereDown").GetComponent<Renderer>().material;
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
