using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectSelection : MonoBehaviour
{
    public float minimumDistance;
    public string lastSelectedName;

    private const KeyCode triggerKey = KeyCode.Mouse0;
    private RaycastHit hitInfo;

    void Start()
    {
        Clear();
    }

    void Update()
    {
        if (Input.GetKeyUp(triggerKey))
        {
            CheckRaycastHit();
        }
    }

    void CheckRaycastHit()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * minimumDistance, Color.green, 1f);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, minimumDistance))
        {
            lastSelectedName = hitInfo.collider.name;
        }
    }

    public void Clear()
    {
        lastSelectedName = null;

    }
}
