using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotlight : MonoBehaviour
{

    private EPOOutline.Outlinable bodyOutline;

    private new Light light;
    private Color currentColor;
    private float currentIntensity;
    private Material lensMat;

    void Start()
    {
        light = transform.Find("Light").GetComponent<Light>();
        currentColor = light.color;
        currentIntensity = light.intensity;

        bodyOutline = transform.Find("Body").GetComponent<EPOOutline.Outlinable>();
        lensMat = transform.Find("Lens").GetComponent<Renderer>().material;

        UpdateLensColor(currentColor);
        UpdateOutlineColor(currentColor);
        UpdateLensEmission(currentIntensity);
        UpdateOutlineWidth(currentIntensity);
    }

    void Update()
    {
        Color newColor = light.color; 
        if (newColor != currentColor)
        {
            UpdateLensColor(newColor);
            UpdateOutlineColor(newColor);
        }
        currentColor = newColor;

        float newIntensity = light.intensity;
        if (newIntensity != currentIntensity)
        {
            UpdateOutlineWidth(newIntensity);
            UpdateLensEmission(newIntensity);
        }
        currentIntensity = newIntensity;
    }

    private void UpdateOutlineWidth(float newIntensity)
    {
        bodyOutline.FrontParameters.Enabled = (newIntensity > 0) ? true : false;
    }

    private void UpdateLensEmission(float newIntensity)
    {
        if (newIntensity > 0)
        {
            lensMat.EnableKeyword("_EMISSION");
        }
        else
        {
            lensMat.DisableKeyword("_EMISSION");
        }
    }

    private void UpdateLensColor(Color newColor)
    {
        lensMat.SetVector("_EmissionColor", newColor);
        lensMat.SetVector("_Color", newColor);
    }

    private void UpdateOutlineColor(Color newColor)
    {
        bodyOutline.FrontParameters.Color = newColor;
    }
}