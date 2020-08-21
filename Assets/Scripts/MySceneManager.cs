using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MySceneManager : MonoBehaviour
{
    private AudioSource audioExitTunnel;
    private bool exiting;

    // Start is called before the first frame update
    void Start()
    {
        audioExitTunnel = transform.Find("SoundExit").gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (exiting && !audioExitTunnel.isPlaying)
        {
            // Load next Scene
            int currentLevelIndex = Int32.Parse(SceneManager.GetActiveScene().name.Substring(5));
            SceneManager.LoadScene("Scenes/Level" + (currentLevelIndex + 1));
        }


    }

    public void Exit()
    {
        audioExitTunnel.Play();
        exiting = true;
    }


}
