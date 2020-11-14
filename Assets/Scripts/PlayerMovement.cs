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
	public float startSpeed;

	[HideInInspector]
	public bool isGrounded;
	[HideInInspector]
	public bool wasGrounded;
	
	private CharacterController controller;
	private ColorCheck cc;	
	private DeathManager dm;
	private Vector3 velocity;
	private Vector3 velocityAirBonus;
	private float groundDistance = 0.1f;

    private void Start()
	{
		controller = gameObject.GetComponent<CharacterController>();
		dm = gameObject.GetComponent<DeathManager>();
		cc = gameObject.GetComponent<ColorCheck>();
		velocity = new Vector3(0, startSpeed, 0);
		isGrounded = true;
		wasGrounded = true;
	}

    // Update is called once per frame
    void Update ()
	{
		// Ground Check
		wasGrounded = isGrounded;
		Vector3 sphere_position = transform.position + Vector3.down * (controller.height * .5f - controller.radius);
		float sphere_radius = controller.radius + groundDistance;
		isGrounded = Physics.CheckSphere(sphere_position, sphere_radius, groundMask);

		// Lethal shadow checks and more..
		dm.UpdateDeathManager(isGrounded, wasGrounded);

		// No mouvement if the player is dead
		if (dm.isDead)
			return;

		// Update color the player is stading on
		cc.UpdatePowerColor(dm.isShadowed);

		// When the detected color is blue for the first time, bounce back.
		if ((cc.powerColor == PowerColor.BLUE) && (cc.prevPowerColor != PowerColor.BLUE))
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
			if (isGrounded && cc.powerColor != PowerColor.CYAN)
			{

				float actualMaxSpeed = (cc.powerColor == PowerColor.GREEN) ? maxLateralSpeed * 3 : maxLateralSpeed;
				if (x==0 && z==0)
                {
					velocity.x = 0;
					velocity.z = 0;
                }
                else
                {
					// Lateral Friction
					// Friction en sqrt pour permettre aux grandes vitesses de rester plus longtemps
					// Tres important pour la dynamique entre la couleur high speed et la couleur jump
					velocity.x -= lateralFriction * Math.Sign(velocity.x) * (float)Math.Sqrt(Math.Abs(velocity.x));
					velocity.z -= lateralFriction * Math.Sign(velocity.z) * (float)Math.Sqrt(Math.Abs(velocity.z));

					// Calculate velocity increase
					Vector3 lateralVelocity = new Vector3(velocity.x, 0f, velocity.z);
					Vector3 newSpeed = lateralVelocity + (lateralAcceleration * Time.deltaTime * (transform.right * x + transform.forward * z));

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
			}

			// Always apply this little bonus which serves to control air travel
			float airAcceleration = 300f;
			velocityAirBonus = (airAcceleration * Time.deltaTime * (transform.right * x + transform.forward * z));

			// Jump
			if (isGrounded)
			{
				velocity.y = 0;
			}
			if (Input.GetButtonDown("Jump") && isGrounded && dm.jumpEnabled)
			{
				velocity.y = (cc.powerColor == PowerColor.YELLOW) ? jumpSpeed * 3 : jumpSpeed;
			}

		}

		// Gravity
		velocity.y += verticalAcceleration * Time.deltaTime;
		velocity.y = Math.Max(velocity.y, minVerticalSpeed);
		
		// Update controller
		controller.Move((velocity + velocityAirBonus) * Time.deltaTime);
	}



	private void OnTriggerEnter(Collider hit)
	{
		if (hit.gameObject.CompareTag("Finish"))
		{
			Debug.Log("TrigNextLevel");
			dm.SetupNextLevel();
		}
	}

}

