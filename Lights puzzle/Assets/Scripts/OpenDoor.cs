using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    [SerializeField]
    private Animator openDoorController;

    public Transform player;

    public float minimalDistanceToSwitch;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        if (Vector3.Distance(player.position, transform.position) > minimalDistanceToSwitch)
        {
            return;
        }
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
