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
	public bool alive = true;

	public Death death;

	private PowerColor powerColor;
	private PowerColor prevPowerColor;
	private Color groundColor;
	public Vector3 prevAlivePosition;
	private Vector3 velocity;
	private float groundDistance = 0.1f;
	private bool isGrounded;

	// Update is called once per frame
	void Update ()
	{
		// Update color the player is stading on (reads from ColorCheck.cs)
		powerColor = gameObject.GetComponent<ColorCheck>().powerColor;
		groundColor = gameObject.GetComponent<ColorCheck>().groundColor;

		// Ground Check
		Vector3 sphere_position = transform.position + Vector3.down * (controller.height * .5f - controller.radius);
		float sphere_radius = controller.radius + groundDistance;
		isGrounded = Physics.CheckSphere(sphere_position, sphere_radius, groundMask);



		// Check for black color
		if (isGrounded && (groundColor.grayscale < 0.1) && (Time.timeSinceLevelLoad > .5))
		{
			alive = false;
			death.TriggerDeath();
		}
		else
		{
			// All is well with the world
			alive = true;
		}


		// When the detected color is blue for the first time, bounce back.
		if ((powerColor == PowerColor.BLUE) && (prevPowerColor != PowerColor.BLUE))
		{
			// TODO detecter le gradient de bleu et appliquer la force en direction opposee
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
			if (isGrounded && (powerColor != PowerColor.CYAN))
			{
				float x = Input.GetAxis("Horizontal");
				float z = Input.GetAxis("Vertical");

				float actualMaxSpeed = (powerColor == PowerColor.GREEN) ? maxLateralSpeed * 3 : maxLateralSpeed;
				float actualAcceleration = lateralAcceleration;

				// Calculate velocity increase
				Vector3 lateralVelocity = new Vector3(velocity.x, 0f, velocity.z);
				Vector3 newSpeed = lateralVelocity + (actualAcceleration * Time.deltaTime * (transform.right * x + transform.forward * z));

				// Apply increase to x and z while making sure they don't go over the max speed
				if (newSpeed.magnitude <= actualMaxSpeed)
				{
					velocity.x = newSpeed.x;
					velocity.z = newSpeed.z;
				}

			}

			// Jump
			if (isGrounded)
			{
				velocity.y = 0;
				//if ((powerColor == PowerColor.YELLOW) && (velocity.y < -3f))
				//{
				//	velocity.y = -velocity.y;
				//}
				//else
				//{
				//	velocity.y = 0;
				//}
			}
			if (Input.GetButtonDown("Jump") && isGrounded)
			{
				velocity.y = (powerColor == PowerColor.YELLOW) ? jumpSpeed * 3 : jumpSpeed;
			}

		}

		// Lateral Friction
		if (isGrounded && (powerColor != PowerColor.CYAN))
		{
			// Friction en sqrt pour permettre aux grandes vitesses de rester plus longtemps
			// Tres important pour la dynamique entre la couleur high speed et la couleur jump
			velocity.x -= lateralFriction * Math.Sign(velocity.x) * (float) Math.Sqrt(Math.Abs(velocity.x));
			velocity.z -= lateralFriction * Math.Sign(velocity.z) * (float) Math.Sqrt(Math.Abs(velocity.z));
		}
		// Gravity
		velocity.y += verticalAcceleration * Time.deltaTime;
		
		// Update controller
		controller.Move(velocity * Time.deltaTime);

		// Remember power color for next frame
		prevPowerColor = powerColor;
	}
}
