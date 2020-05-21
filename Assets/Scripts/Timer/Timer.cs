using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Environments;
using UnityEngine;

public abstract class Timer: MonoBehaviour
{
      
    public Light[] associatedLights;
    public bool isOpened = false;
    public float seconds = 10f;
    public AudioSource audioStart;
    public AudioSource audioDuring;


    public MinimumDistance minimumDistance;

    protected Transform knob;
    protected float timeOpened = 0;
    
    // Use this for initialization
    void Start ()
    {
        knob = transform.Find("Body").Find("Rotating knob");
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
