using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity;
    public Transform playerBody;

    private float xRotation = 0f;

	// Use this for initialization
	void Start ()
	{
		#if !UNITY_EDITOR && UNITY_WEBGL
			Debug.Log("WEBGL HEEE AHHHHHH");
			mouseSensitivity /= 4;
		#else
			Debug.Log("Not WEBGL");
		#endif
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.L))	
        {
			Debug.Log("ah ah!");
			Cursor.lockState = CursorLockMode.Locked;
        }

		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
	    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

	    xRotation -= mouseY;
	    xRotation = Mathf.Clamp(xRotation, -60f, 60f);

	    transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
