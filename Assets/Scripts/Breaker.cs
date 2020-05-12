using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Environments;
using UnityEngine;

public class Breaker : MonoBehaviour
{

    public Light associatedLight;
    public float openIntensity = 10f;
    public float closeIntensity = 0f;
    public bool isOpened = false;

    public MinimumDistance minimumDistance;

    private Transform arm;
    private Vector3 closedAngle = new Vector3(-30, 0, 0);
    private Vector3 openedAngle = new Vector3(30, 0, 0);

    // Use this for initialization
    void Start ()
    {
        arm = transform.Find("ArmWrapper");
        if (isOpened)
        {
            Open();
        }
        else
        {
            Close();
        }
    }
	
    void OnMouseUp()
    {
        if (!minimumDistance.Check())
        {
            return;
        }

        if (isOpened)
        {
            Close();
        }
        else
        {
            Open();
        }
        gameObject.GetComponent<AudioSource>().Play();
        isOpened = !isOpened;
    }

    void Close()
    {
        arm.localEulerAngles = closedAngle;
        associatedLight.intensity = closeIntensity;
    }

    void Open()
    {
        arm.localEulerAngles = openedAngle;
        associatedLight.intensity = openIntensity;
    }
}
