﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Timer: MonoBehaviour
{

    public Transform[] lightTransforms;
    public bool isOpened = false;
    public float seconds = 10f;

    protected List<Light> associatedLights = new List<Light>();

    private MinimumDistance minimumDistance;
    private AudioSource audioStart;
    private AudioSource audioDuring;
    private Transform knob;
    private float timeOpened = 0;
    
    // Use this for initialization
    void Start ()
    {
        // Extract Light components from provided Light transforms
        foreach (Transform lightTransform in lightTransforms)
        {
            Light light = lightTransform.Find("Rotating").Find("Light").gameObject.GetComponent<Light>();
            associatedLights.Add(light);
        }
        // Reach for important components in this game object
        minimumDistance = gameObject.GetComponent<MinimumDistance>();
        audioStart = transform.Find("Body").Find("SoundStart").gameObject.GetComponent<AudioSource>();
        audioDuring = transform.Find("Body").Find("SoundDuring").gameObject.GetComponent<AudioSource>();
        knob = transform.Find("Body").Find("Rotating knob");
        // Start the timer or not
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
            // Time elapsed since the timer has been turned on
            float timeElapsed = Time.time - timeOpened;
            if (timeElapsed > seconds)
            {
                // If the timer has been on for its entire duration, close.
                Close();
            }
            else
            {
                // Adjust angle of timer dial
                knob.localEulerAngles = new Vector3(90f - (timeElapsed / seconds * 180f), 0, 0);
            }
        }
    }

    void OnMouseUp()
    {
        if (!minimumDistance.Check())
        {
            // If the minimum distance requirement wasn't met, do nothing
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
            CloseSpecific();
        }
        audioDuring.Stop();
    }

    void Open()
    {
        isOpened = true;
        timeOpened = Time.time;
        foreach (Light light in associatedLights)
        {
            OpenSpecific();
        }
        audioStart.Play();
        audioDuring.Play();
    }



    public abstract void CloseSpecific();
    public abstract void OpenSpecific();
}
