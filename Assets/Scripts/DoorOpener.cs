using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{

    [SerializeField] private Animator openDoorController;

    public MinimumDistance minimumDistance;

    void OnMouseUp()
    {
        if (!minimumDistance.Check())
        {
            return;
        }

        Open();
    }
        

    void Open()
    {
        
        if (openDoorController.GetBool("opened"))
        {
            return;
        }
        else
        {
            GameObject.FindGameObjectWithTag("SuccessSound").GetComponent<AudioSource>().Play();
            openDoorController.SetBool("opened", true);
        }
    }
}
