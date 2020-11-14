using System;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public bool cheatMode = false;
    public float fallSpeed;
    public float lethalGrayScale = 0.1f;
    public float healthSpeed;
    public float health;

    [HideInInspector]
    public bool isDead;
    [HideInInspector]
    public bool isShadowed;
    [HideInInspector]
    public bool jumpEnabled;

    private bool goToNextLevel;
    private MySceneManager mySceneManager;
    private GameObject mainCamera;
    private CharacterController controller;
    private Vector2 cameraHeightRange;
    private ColorCheck cc;


    void Start()
    {
        goToNextLevel = false;
        mainCamera = transform.Find("Main Camera").gameObject;
        controller = transform.gameObject.GetComponent<CharacterController>();
        cameraHeightRange = new Vector2(-controller.height / 2f, 4f * controller.height / 10.25f);
        mySceneManager = GameObject.Find("/SceneManager").GetComponent<MySceneManager>();
        cc = gameObject.GetComponent<ColorCheck>();

        jumpEnabled = true;
        isDead = false;
        isShadowed = false;
        health = 1f;
    }

    public void SetupNextLevel()
    {
        goToNextLevel = true;
    }

    public void UpdateDeathManager(bool isGrounded, bool wasGrounded)
    {
        // If the player is in the dark or enters the exit tunnel, 
        // Start the "falling through the floor" effect
        isShadowed = CheckForLethalShadow();

        // If landing in shadow, deactivate jump until the light is found
        // Counters double jumps
        if (isShadowed && isGrounded && !wasGrounded)
            jumpEnabled = false;
        // Reactivate jump once player is back in the light 
        if (!isShadowed)
            jumpEnabled = true;

        if (isGrounded)
        {
            if (isShadowed)
                DecreaseHealth();
            else
                IncreaseHealth();
        
            if (health < 0.01f)
                KillPlayer();
        }

        if (isDead || goToNextLevel)
        {
            if (mainCamera.transform.localPosition.y < cameraHeightRange.x + 0.01f)
            {
                mySceneManager.Exit(goToNextLevel);
            }
            else
            {
                // "Sinking" effect when touching black. 
                float new_y = mainCamera.transform.localPosition.y - fallSpeed * Time.deltaTime;
                mainCamera.transform.localPosition = new Vector3(0, Math.Max(new_y, cameraHeightRange.x), 0);
            }
        }
    }

    public void KillPlayer()
    {
        if (!cheatMode)
        {
            isDead = true;
        }
    }

    private void IncreaseHealth()
    {
        health = Math.Min(1f, health + healthSpeed * Time.deltaTime);
    }

    private void DecreaseHealth()
    {
        health = Math.Max(0f, health - healthSpeed * Time.deltaTime);
    }

    private bool CheckForLethalShadow()
    {
        // Black color check: too dark of a grayscale means the player is shadowed and starts falling
        if (cc.groundColor.grayscale < lethalGrayScale)
            return true;
        else
            return false;
    }

}
