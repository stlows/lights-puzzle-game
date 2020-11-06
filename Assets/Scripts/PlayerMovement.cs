using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class PlayerMovement : MonoBehaviour
{
	public float lateralAcceleration = 500f;
	public float verticalAcceleration = -100f; // gravity
	public float maxLateralSpeed = 30f;
	public float minVerticalSpeed;
	public float airBonusSpeed;
	public float jumpSpeed = 35f;
	public float lateralFriction = 0.1f;
	public LayerMask groundMask;
	public bool goToNextLevel = false;
	public float startSpeed;

	private CharacterController controller;
	private PowerColor powerColor;
	private PowerColor prevPowerColor;
	private Color groundColor;
	public Vector3 prevAlivePosition;
	private Vector3 velocity;
	private Vector3 velocityAirBonus;
	private float groundDistance = 0.1f;
	private bool isGrounded;
	[HideInInspector]
	public bool wasGrounded;
	private bool isShadowed;
	private Death death;

    private void Start()
	{
		controller = gameObject.GetComponent<CharacterController>();
		death = gameObject.GetComponent<Death>();
		velocity = new Vector3(0, startSpeed, 0);
	}

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

		// Black color check: too dark of a grayscale means the player is shadowed and starts falling
		if (isGrounded && (groundColor.grayscale < death.lethalGrayScale))
		{
			isShadowed = true;
		}
        else
        {
			isShadowed = false;
        }

		// If the player is in the dark or enters the exit tunnel, 
		// Start the "falling through the floor" effect
		if (isShadowed || goToNextLevel || death.deathFinal)
		{
			death.GoToDeath();
		}
		else
		{
			death.GoToAlive();
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
			float x = Input.GetAxis("Horizontal");
			float z = Input.GetAxis("Vertical");
			
			// Lateral mouvement
			if (isGrounded && powerColor != PowerColor.CYAN)
			{

				float actualMaxSpeed = (powerColor == PowerColor.GREEN) ? maxLateralSpeed * 3 : maxLateralSpeed;
				float actualAcceleration = lateralAcceleration;

				// Lateral Friction
				// Friction en sqrt pour permettre aux grandes vitesses de rester plus longtemps
				// Tres important pour la dynamique entre la couleur high speed et la couleur jump
				velocity.x -= lateralFriction * Math.Sign(velocity.x) * (float)Math.Sqrt(Math.Abs(velocity.x));
				velocity.z -= lateralFriction * Math.Sign(velocity.z) * (float)Math.Sqrt(Math.Abs(velocity.z));

				// Calculate velocity increase
				Vector3 lateralVelocity = new Vector3(velocity.x, 0f, velocity.z);
				Vector3 newSpeed = lateralVelocity + (actualAcceleration * Time.deltaTime * (transform.right * x + transform.forward * z));

				// Apply increase to x and z while making sure they don't go over the max speed
				velocity.x = newSpeed.x;
				velocity.z = newSpeed.z;

				// TODO add footstep sounds if speed != 0

				if (velocity.magnitude > actualMaxSpeed)
				{
					velocity.x *= (actualMaxSpeed / velocity.magnitude);
					velocity.z *= (actualMaxSpeed / velocity.magnitude);
				}
			}

			// Always apply this little bonus which serves to control air travel
			float airAcceleration = 300f;
			velocityAirBonus = (airAcceleration * Time.deltaTime * (transform.right * x + transform.forward * z));

			// Jump
			if (isGrounded)
			{
				velocity.y = 0;
			}
			if (Input.GetButtonDown("Jump") && isGrounded)
			{
				velocity.y = (powerColor == PowerColor.YELLOW) ? jumpSpeed * 3 : jumpSpeed;
			}

		}

		// Gravity
		velocity.y += verticalAcceleration * Time.deltaTime;
		velocity.y = Math.Max(velocity.y, minVerticalSpeed);
		
		// Update controller
		controller.Move((velocity + velocityAirBonus) * Time.deltaTime);

		// Remember for next frame
		prevPowerColor = powerColor;
		wasGrounded = isGrounded;
	}



	private void OnTriggerEnter(Collider hit)
	{
		if (hit.gameObject.CompareTag("Finish"))
		{
			Debug.Log("TrigNextLevel");
			goToNextLevel = true;
		}
	}

}

