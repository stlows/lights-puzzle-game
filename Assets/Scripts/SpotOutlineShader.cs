using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotOutlineShader : MonoBehaviour
{

    private Material cylinderMat;
    private Material cubeMat;
    private Material boxMat;

    private Light light;
    private Color currentColor;
    private float currentIntensity;

    void Start()
    {
        light = transform.Find("Rotating/Light").GetComponent<Light>();
        currentColor = light.color;
        currentIntensity = light.intensity;

        cylinderMat = transform.Find("Base/Cylinder").GetComponent<Renderer>().material;
        cubeMat = transform.Find("Base/Cube").GetComponent<Renderer>().material;
        boxMat = transform.Find("Rotating/Box").GetComponent<Renderer>().material;

        UpdateOutlineColor(currentColor);
        UpdateOutlineWidth(currentIntensity);
    }

    void Update()
    {
        Color newColor = light.color; 
        if (newColor != currentColor)
        {
            UpdateOutlineColor(newColor);
        }
        currentColor = newColor;

        float newIntensity = light.intensity;
        if (newIntensity != currentIntensity)
        {
            UpdateOutlineWidth(newIntensity);
        }
        currentIntensity = newIntensity;
    }

    private void UpdateOutlineWidth(float newIntensity)
    {
        boxMat.SetFloat("_FirstOutlineWidth", (newIntensity > 0) ? 0.05f : 0f);
        cubeMat.SetFloat("_FirstOutlineWidth", (newIntensity > 0) ? 0.05f : 0f);
        cylinderMat.SetFloat("_FirstOutlineWidth", (newIntensity > 0) ? 0.15f : 0f);
    }

    private void UpdateOutlineColor(Color newColor)
    {
        boxMat.SetVector("_FirstOutlineColor", newColor);
        cubeMat.SetVector("_FirstOutlineColor", newColor);
        cylinderMat.SetVector("_FirstOutlineColor", newColor);
    }
}