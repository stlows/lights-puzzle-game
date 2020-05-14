using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
	public float lateralAcceleration = 500f;
	public float verticalAcceleration = -100f; // gravity
	public float maxLateralSpeed = 30f;
	public float jumpSpeed = 35f;
	public float lateralFriction = 0.1f;
	public LayerMask groundMask;

	private PowerColor groundColor;
	private PowerColor prevGroundColor;
	private Vector3 velocity;
	private float groundDistance = 0.1f;
	private bool isGrounded;

	// Update is called once per frame
	void Update ()
	{
		// Update color the player is stading on (reads from ColorCheck.cs)
		groundColor = gameObject.GetComponent<ColorCheck>().powerColor;

		// Ground Check
		Vector3 sphere_position = transform.position + Vector3.down * (controller.height * .5f - controller.radius);
		float sphere_radius = controller.radius + groundDistance;
		isGrounded = Physics.CheckSphere(sphere_position, sphere_radius, groundMask);

		// When the detected color is black for the first time, bounce back.
		if ((groundColor == PowerColor.BLACK) && (prevGroundColor != PowerColor.BLACK))
		{
			velocity = -velocity.normalized * Math.Max(30, velocity.magnitude);
			if (!isGrounded) {
				velocity.y = Math.Max(jumpSpeed, velocity.y);
			}
			else
			{
				velocity.y = jumpSpeed/2;
			}
		}
		else
		{
			// Lateral mouvement
			if (isGrounded)
			{
				float x = Input.GetAxis("Horizontal");
				float z = Input.GetAxis("Vertical");

				float actualMaxSpeed = (groundColor == PowerColor.GREEN) ? maxLateralSpeed * 3 : maxLateralSpeed;

				// Calculate velocity increase
				Vector3 lateralVelocity = new Vector3(velocity.x, 0f, velocity.z);
				Vector3 newSpeed = lateralVelocity + (lateralAcceleration * Time.deltaTime * (transform.right * x + transform.forward * z));

				// Apply increase to x and z while making sure they don't go over the max speed
				if (newSpeed.magnitude > actualMaxSpeed)
				{
					newSpeed = newSpeed.normalized * actualMaxSpeed;
				}
				velocity.x = newSpeed.x;
				velocity.z = newSpeed.z;
			}

			// Jump
			if (isGrounded)
			{
				velocity.y = 0;
			}
			if (Input.GetButtonDown("Jump") && isGrounded)
			{
				velocity.y = (groundColor == PowerColor.RED) ? jumpSpeed * 3 : jumpSpeed;
			}

		}

		// Lateral Friction
		if (isGrounded)
		{
			velocity.x -= lateralFriction * velocity.x;
			velocity.z -= lateralFriction * velocity.z;
		}
		// Gravity
		velocity.y += verticalAcceleration * Time.deltaTime;
		
		// Update controller
		controller.Move(velocity * Time.deltaTime);

		// Remember current color for next update
		prevGroundColor = groundColor;
	}
}
