using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectSelection : MonoBehaviour
{
    public float minimumDistance;
    public string lastSelected;
    public string currentlyHovering;

    private const KeyCode triggerKey = KeyCode.Mouse0;
    private RaycastHit hitInfo;

    void Start()
    {
        lastSelected = null;
        currentlyHovering = null;
    }

    void Update()
    {
        string st = CheckRaycastHit();

        currentlyHovering = st;

        if (Input.GetKeyUp(triggerKey))
        {
            lastSelected = st;
        }
    }

    private string CheckRaycastHit()
    {
        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * minimumDistance, Color.green, 1f);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, minimumDistance))
        {
            return hitInfo.collider.name;
        }

        return null;
    }

    public void Clear()
    {
        lastSelected = null;
    }
}
