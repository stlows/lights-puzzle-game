using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    public float minimumDistance;
    private const KeyCode triggerKey = KeyCode.Mouse0;
    private bool keyCodeWasDown;
    private RaycastHit hitInfo;

    void Start()
    {
        keyCodeWasDown = false;
    }

    protected bool ButtonIsActivated()
    {
        // On a keyDown event, check for hit.
        if (Input.GetKeyDown(triggerKey))
        {
            if (CheckRaycastHit())
            {
                keyCodeWasDown = true;
                return false;
            }
        }

        // If the keyDown was a successful hit and the key remained pressed, check for hit on keyUp.
        if (keyCodeWasDown && Input.GetKeyUp(triggerKey))
        {
            if (CheckRaycastHit())
            {
                keyCodeWasDown = false;
                return true;
            }
        }

        // If the keyDown was a succesful hit, maintain success as long as the key stays pressed
        if (keyCodeWasDown && Input.GetKey(triggerKey))
        {
            keyCodeWasDown = true;
        }
        else
        {
            keyCodeWasDown = false;
        }
        return false;
    }

    bool CheckRaycastHit()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * minimumDistance, Color.green, 1f);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, minimumDistance))
        {
            if (hitInfo.collider.name == gameObject.GetComponent<Collider>().name)
            {
                return true;
            }
        }
        return false;
    }
}
