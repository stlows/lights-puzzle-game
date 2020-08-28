using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    public bool cheatMode = false;
    public float fallSpeed;
    
    private Image blackFade;
    private MySceneManager mySceneManager;
    private GameObject mainCamera;
    private PlayerMovement player;
    private CharacterController controller;
    private bool deathFinal = false;
    private Vector2 cameraHeightRange;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = transform.Find("Main Camera").gameObject;
        player = transform.gameObject.GetComponent<PlayerMovement>();
        controller = transform.gameObject.GetComponent<CharacterController>(); ;
        cameraHeightRange = new Vector2(-controller.height/2f, 4f*controller.height/10.25f);
        blackFade = GameObject.Find("/Canvas/blackFade").GetComponent<Image>();
        mySceneManager = GameObject.Find("/SceneManager").GetComponent<MySceneManager>();

        blackFade.color = Color.black;
        blackFade.canvasRenderer.SetAlpha(0f);
    }


    public void GoToDeath(Color groundColor)
    {
        // "Sinking" effect when touching black. 
        // Camera creeps downwards, until it reaches 0, where death is final
        if (mainCamera.transform.localPosition.y < cameraHeightRange.x+0.01f)
        {
            if (!cheatMode)
            {
                deathFinal = true;
                FinalizeDeath();
            }
        }
        else 
        {
            float new_y = mainCamera.transform.localPosition.y - fallSpeed * Time.deltaTime;
            mainCamera.transform.localPosition = new Vector3(0, Math.Max(new_y, cameraHeightRange.x), 0);
        }
    }

    public void GoToAlive(Color groundColor)
    {
        if ((mainCamera.transform.localPosition.y < cameraHeightRange.y) && !deathFinal)
        {
            float new_y = mainCamera.transform.localPosition.y + fallSpeed * 3 * Time.deltaTime;
            mainCamera.transform.localPosition = new Vector3(0, Math.Min(new_y, cameraHeightRange.y), 0);
        }
    }

    private void FinalizeDeath()
    {
        //blackFade.CrossFadeAlpha(1f, 0.1f, true);
        blackFade.canvasRenderer.SetAlpha(1f);
        mySceneManager.Exit(true);
    }
}
