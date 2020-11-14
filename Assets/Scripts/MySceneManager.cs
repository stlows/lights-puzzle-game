using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MySceneManager : MonoBehaviour
{
    public int currentLevelIndex;
    public string newSoundtrack = "";

    private Image blackFade;
    private bool exiting;
    private int loadIncrement;
    private AudioSource currentlyPlayingAudio;

    // Start is called before the first frame update
    void Start()
    {
        currentLevelIndex = Int32.Parse(SceneManager.GetActiveScene().name.Substring(5));
        exiting = false;
        loadIncrement = 0;
        Debug.Log(newSoundtrack);
        if (newSoundtrack == "stop")
        {
            AudioManager.instance.currentSoundtrack.source.Stop();
        }
        else if ((newSoundtrack != "") && (newSoundtrack != AudioManager.instance.currentSoundtrack.name))
        {
            AudioManager.instance.Play(newSoundtrack);
        }

        blackFade = GameObject.Find("/Canvas/blackFade").GetComponent<Image>();
        blackFade.color = Color.black;
        blackFade.canvasRenderer.SetAlpha(0f);
    }

    // Update is called once per frame
    void Update()
    {

        //Load next scene if the exit tunnel sound is over
        if (exiting && !currentlyPlayingAudio.isPlaying)
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
            blackFade.canvasRenderer.SetAlpha(1f);
            if (goToNextLevel)
            {
                currentlyPlayingAudio = transform.Find("SoundExit").GetComponent<AudioSource>();
            } else {
                currentlyPlayingAudio = transform.Find("SoundDeath").GetComponent<AudioSource>();
                AudioManager.instance.currentSoundtrack.source.volume = 0f;
            }

            loadIncrement = goToNextLevel ? 1 : 0;
            currentlyPlayingAudio.Play();
            exiting = true;
        }
    }


}
