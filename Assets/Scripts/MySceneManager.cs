using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MySceneManager : MonoBehaviour
{
    public int currentLevelIndex;
    public string newSoundtrack = "";

    private bool exiting;
    private int loadIncrement;
    private AudioSource nextLevelAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        currentLevelIndex = Int32.Parse(SceneManager.GetActiveScene().name.Substring(5));
        exiting = false;
        loadIncrement = 0;
        nextLevelAudioSource = transform.Find("SoundExit").GetComponent<AudioSource>();
        Debug.Log(newSoundtrack);
        if (newSoundtrack == "stop")
        {
            AudioManager.instance.currentSoundtrack.source.Stop();
        }
        else if (newSoundtrack != "")
        {
            AudioManager.instance.Play(newSoundtrack);
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Load next scene if the exit tunnel sound is over, as well as exiting == true as a safeguard
        if (exiting && !nextLevelAudioSource.isPlaying)
        {
            //Load next Scene
            SceneManager.LoadScene("Scenes/Level" + (currentLevelIndex + loadIncrement));
        }


    }

    public void Exit(bool restart)
    {
        loadIncrement = restart ? 0 : 1;
        // Exit has been triggered by player entering tunnel.
        nextLevelAudioSource.Play();
        exiting = true;
    }


}
