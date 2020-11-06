using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public abstract class Breaker : MonoBehaviour
{
    public bool isOpened = false;
    public float delay;
    public bool automaticOpen;
    public float automaticOpenDelay;

    private Transform arm;
    private DetectSelection fpsSelection;
    private AudioSource audioSource;
    private Vector3 closedAngle = new Vector3(-30, 0, 0);
    private Vector3 openedAngle = new Vector3(30, 0, 0);
    private float timeClicked;

    protected void BreakerStart()
    {
        // Reach for important components in this game object
        fpsSelection = GameObject.Find("FPS").GetComponent<DetectSelection>(); 
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

    void Update()
    {
        if (automaticOpen && Time.timeSinceLevelLoad > automaticOpenDelay)
        {
            automaticOpen = false;
            ActivateButton();
        }
        else if (fpsSelection.lastSelected == gameObject.name)
        {
            fpsSelection.Clear();
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
