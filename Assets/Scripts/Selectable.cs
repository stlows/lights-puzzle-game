using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    public float minimumDistance;
    private const KeyCode triggerKey = KeyCode.Mouse0;
    private RaycastHit hitInfo;

    protected bool ButtonIsActivated()
    {
        if (Input.GetKeyUp(triggerKey))
        {
            if (CheckRaycastHit())
            {
                return true;
            }
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
