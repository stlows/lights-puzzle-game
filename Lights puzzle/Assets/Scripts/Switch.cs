using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Environments;
using UnityEngine;

public class Switch : MonoBehaviour
{

    public Light associatedLight;
    public Transform player;
    public float openIntensity = 10f;
    public float closeIntensity = 0f;
    public bool isOpened = false;
    public float minimalDistanceToSwitch = 20;

    public Vector3 closedAngle;
    public Vector3 openedAngle;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseUp()
    {
        if (Vector3.Distance(player.position, transform.position) > minimalDistanceToSwitch)
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

        GameObject.FindGameObjectWithTag("BreakerSwitchSound").GetComponent<AudioSource>().Play();
        isOpened = !isOpened;

    }

    void Close()
    {
        transform.eulerAngles = closedAngle;
        associatedLight.intensity = closeIntensity;
    }

    void Open()
    {
        transform.eulerAngles = openedAngle;
        associatedLight.intensity = openIntensity;
    }
}
