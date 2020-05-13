using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 30f;
    public float gravity = -100f;
    public float jumpSpeed = 35f;
    public LayerMask groundMask;    
	
	
	private PowerColor groundColorLabel;
	private Vector3 velocity;
	private float groundDistance = 0.1f;
	private bool isGrounded;


	// Update is called once per frame
	void Update ()
	{
		// Update color the player is stading on (reads from ColorCheck.cs)
		groundColorLabel = gameObject.GetComponent<ColorCheck>().powerColor;

		// Lateral mouvement
		float x = Input.GetAxis("Horizontal");
	    float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
		move = (groundColorLabel == PowerColor.GREEN) ? move * speed * 3 : move * speed;
		//move = (groundColorLabel == PowerColor.BLACK) ? -move : move;

		// Jump
		Vector3 sphere_position = transform.position + Vector3.down * (controller.height * .5f - controller.radius);
		float sphere_radius = controller.radius + groundDistance;
		isGrounded = Physics.CheckSphere(sphere_position, sphere_radius, groundMask);
		if (isGrounded)
		{
			velocity.y = 0;
		}
		if (Input.GetButtonDown("Jump") && isGrounded)
	    {
			velocity.y = (groundColorLabel == PowerColor.RED) ? jumpSpeed * 3 : jumpSpeed;
		}
	    velocity.y += gravity * Time.deltaTime;

		// Update controller
		controller.Move(move * Time.deltaTime);
		controller.Move(velocity * Time.deltaTime);
	}
}
