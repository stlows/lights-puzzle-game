using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectSelection : MonoBehaviour
{
    public float minimumDistance;
    public string lastSelectedName;
    public string lastHoveredName;

    private const KeyCode triggerKey = KeyCode.Mouse0;
    private RaycastHit hitInfo;

    void Start()
    {
        Clear();
    }

    void Update()
    {
        lastHoveredName = CheckRaycastHit();
        if (Input.GetKeyUp(triggerKey))
        {
            lastSelectedName = lastHoveredName;
        }
    }

    private string CheckRaycastHit()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * minimumDistance, Color.green, 1f);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, minimumDistance))
        {
            return hitInfo.collider.name;
        }

        return null;
    }

    public void Clear()
    {
        lastSelectedName = null;

    }
}
