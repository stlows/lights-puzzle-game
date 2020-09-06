using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public abstract class Breaker : MonoBehaviour
{
    public bool isOpened = false;
    public float delay;


    private MinimumDistance minimumDistance;
    private Transform arm;
    private AudioSource audioSource;
    private Vector3 closedAngle = new Vector3(-30, 0, 0);
    private Vector3 openedAngle = new Vector3(30, 0, 0);
    private float timeClicked;

    // Use this for initialization
    protected void BreakerStart()
    {
        // Reach for important components in this game object
        minimumDistance = gameObject.GetComponent<MinimumDistance>();
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

    void OnMouseUp()
    {
        // If the minimum distance requirement wasn't met, do nothing
        if (!minimumDistance.Check())
        {
            return;
        }

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
