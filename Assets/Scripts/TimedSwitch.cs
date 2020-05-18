using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Environments;
using UnityEngine;

public class TimedSwitch: MonoBehaviour
{

    public Light[] associatedLights;
    public float openIntensity = 10f; 
    public float closeIntensity = 0f;
    public bool isOpened = false;
    public float seconds = 10f;

    public MinimumDistance minimumDistance;

    private Transform knob;
    private float timeOpened = 0;

    // Use this for initialization
    void Start ()
    {
        knob = transform.Find("Rotating knob");
        if (isOpened)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    void Update()
    {
        if (isOpened)
        {
            float timeElapsed = Time.time - timeOpened;
            Debug.Log(timeElapsed);
            if (timeElapsed > seconds)
            {
                Close();
            }
            else
            {
                knob.localEulerAngles = new Vector3(90f - (timeElapsed / seconds * 180f), 0, 0);
            }
        }
    }

    void OnMouseUp()
    {
        if (!minimumDistance.Check())
        {
            return;
        }
        Open();
    }

    void Close()
    {
        isOpened = false;
        knob.localEulerAngles = new Vector3(-90f, 0, 0);
        foreach (Light light in associatedLights)
        {
            light.intensity = closeIntensity;
        }
        gameObject.GetComponent<AudioSource>().Stop();
    }

    void Open()
    {
        isOpened = true;
        timeOpened = Time.time;
        foreach (Light light in associatedLights)
        {
            light.intensity = openIntensity;
        }
        gameObject.GetComponent<AudioSource>().Play();
    }
}
