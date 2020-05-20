using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Environments;
using UnityEngine;
 
public class BreakerColor : MonoBehaviour
{

    public Light[] associatedLights;
    public Color openColor = Color.white;
    public Color closeColor = Color.black;
    public bool isOpened = false;

    public MinimumDistance minimumDistance;

    private Transform arm;
    private Vector3 closedAngle = new Vector3(-30, 0, 0);
    private Vector3 openedAngle = new Vector3(30, 0, 0);

    // Use this for initialization
    void Start ()
    {
        arm = transform.Find("Body").Find("ArmWrapper");
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
        foreach (Light light in associatedLights)
        {
            light.color = closeColor;
        }
    }

    void Open()
    {
        arm.localEulerAngles = openedAngle;
        foreach (Light light in associatedLights)
        {
            light.color = openColor;
        }
    }
}
