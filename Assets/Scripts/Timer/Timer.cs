﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Timer: SpotController
{

    public Transform[] lightTransforms;
    public bool isOpened;
    public bool isHighlighted;
    public float seconds = 10f;

    protected List<Light> associatedLights = new List<Light>();

    private Material roundedBoxMat;
    private string colliderName;
    private DetectSelection fpsSelection;
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
            Light light = lightTransform.Find("Light").gameObject.GetComponent<Light>();
            associatedLights.Add(light);
        }
        // Reach for important components in this game object
        audioStart = transform.Find("Body/SoundStart").gameObject.GetComponent<AudioSource>();
        audioDuring = transform.Find("Body/SoundDuring").gameObject.GetComponent<AudioSource>();
        fpsSelection = GameObject.Find("FPS").GetComponent<DetectSelection>();
        knob = transform.Find("Body/ArmWrapper");
        roundedBoxMat = transform.Find("Body/RoundedBoxWrapper/RoundedBox").GetComponent<Renderer>().material;
        isHighlighted = false;
        // Collider objects must be renamed for the DetectSelection script
        colliderName = gameObject.name + ": collider <generated by script>";
        transform.Find("Body/RoundedBoxWrapper/RoundedBox").gameObject.name = colliderName;
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
        if (fpsSelection.lastSelectedName == colliderName)
        {
            fpsSelection.Clear();
            ActivateButton();
        }
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

    void ActivateButton()
    {
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
        ledsClosedState();
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
        ledsOpenState();
    }


    private void LateUpdate()
    {
        if (!isHighlighted && (fpsSelection.lastHoveredName == colliderName))
        {
            isHighlighted = true;
            roundedBoxMat.SetFloat("_FirstOutlineWidth", 0.2f);
        }
        else if (isHighlighted && (fpsSelection.lastHoveredName != colliderName))
        {
            isHighlighted = false;
            roundedBoxMat.SetFloat("_FirstOutlineWidth", 0f);
        }
    }

    public abstract void CloseSpecific();
    public abstract void OpenSpecific();
}
