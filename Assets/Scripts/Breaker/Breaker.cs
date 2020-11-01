using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public abstract class Breaker : Selectable
{
    public bool isOpened = false;
    public float delay;


    private Transform arm;
    private AudioSource audioSource;
    private Vector3 closedAngle = new Vector3(-30, 0, 0);
    private Vector3 openedAngle = new Vector3(30, 0, 0);
    private float timeClicked;

    // Use this for initialization
    protected void BreakerStart()
    {
        // Reach for important components in this game object
        audioSource = transform.Find("Body").Find("Sound").gameObject.GetComponent<AudioSource>();
        arm = transform.Find("Body").Find("ArmWrapper");
        // Starting position for the breaker
        if (isOpened)
        {
            OpenBreaker();
            OpenSpecific();
        }
        else
        {
            CloseBreaker();
            CloseSpecific();
        }
        timeClicked = Mathf.Infinity;
    }

    private void Update()
    {
        if (base.ButtonIsActivated())
        {
            ActivateButton();
        }

        if ( Time.time > (timeClicked + delay) )
        {
            if (isOpened)
            {
                OpenSpecific();
            }
            else
            {
                CloseSpecific();
            }
            AfterDelaySpecific();
            timeClicked = Mathf.Infinity;
        }
    }

    void ActivateButton()
    {
        timeClicked = Time.time;
        OnClickSpecific();

        if (isOpened)
        {
            CloseBreaker();
        }
        else
        {
            OpenBreaker();
        }

        audioSource.Play();
        isOpened = !isOpened;
    }

    void CloseBreaker()
    {
        arm.localEulerAngles = closedAngle;
    }

    void OpenBreaker()
    {
        arm.localEulerAngles = openedAngle;
    }


    public abstract void CloseSpecific();
    public  abstract void OpenSpecific();
    public abstract void OnClickSpecific();
    public abstract void AfterDelaySpecific();

}
