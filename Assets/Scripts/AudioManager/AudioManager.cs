using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;
	public Sound[] sounds;

	[HideInInspector]
	public Sound currentSoundtrack;

	private void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
		}
	}

    private void Start()
	{
		currentSoundtrack = Find("gliding");
	}

    public void Play(string name)
	{
		if (currentSoundtrack.source.isPlaying)
		{
			currentSoundtrack.source.Stop();
		}
		currentSoundtrack = Find(name);
		currentSoundtrack.source.volume = currentSoundtrack.volume;
		currentSoundtrack.source.pitch = currentSoundtrack.pitch;
		currentSoundtrack.source.Play();
	}

    public Sound Find(string name)
    {
		Sound s = Array.Find(sounds, item => item.name == name);
		if (s == null)
			Debug.LogWarning("Sound: " + name + " not found!");
		return s;
	}


}