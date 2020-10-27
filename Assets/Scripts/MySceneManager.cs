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
        else if ((newSoundtrack != "") && (newSoundtrack != AudioManager.instance.currentSoundtrack.name))
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
            AudioManager.instance.currentSoundtrack.source.volume = 1f;
            //Load next Scene
            SceneManager.LoadScene("Scenes/Level" + (currentLevelIndex + loadIncrement));
        }


    }

    public void Exit(bool goToNextLevel)
    {
        // Exit has been triggered by player entering tunnel.
        if (!exiting)
        {
            loadIncrement = goToNextLevel ? 1 : 0;
            AudioManager.instance.currentSoundtrack.source.volume = 0f;
            nextLevelAudioSource.Play();
            exiting = true;
        }
    }


}
