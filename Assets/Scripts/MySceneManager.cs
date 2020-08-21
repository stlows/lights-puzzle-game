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
        Debug.Log("Start");
        audioExitTunnel = transform.Find("SoundExit").gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (exiting && !audioExitTunnel.isPlaying)
        {
            Debug.Log("exiting");
            Debug.Log(exiting);
            Debug.Log("audioExitTunnel.isPlaying");
            Debug.Log(audioExitTunnel.isPlaying);
            // Load next Scene
            int currentLevelIndex = Int32.Parse(SceneManager.GetActiveScene().name.Substring(5));
            SceneManager.LoadScene("Scenes/Level" + (currentLevelIndex + 1));
        }


    }

    public void Exit()
    {
        Debug.Log("Exit");
        audioExitTunnel.Play();
        exiting = true;
    }


}
