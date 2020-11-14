

using UnityEngine;

/// <summary>
///   Audio source that fades between clips instead of playing them immediately.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class FadingAudioSource : MonoBehaviour
{
    public float FadeSpeed = 2f;
    private AudioSource audioSource;
    private FadeState fadeState = FadeState.None;
    public float finalVolume = .5f;

    public enum FadeState
    {
        None,

        FadingOut,

        FadingIn
    }

    public void FadeIn()
    {
        fadeState = FadeState.FadingIn;
        audioSource.Play();
    }

    public void FadeOut()
    {
        fadeState = FadeState.FadingOut;
    }

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = 0f;
    }

    private void Update()
    {
        if (fadeState == FadeState.FadingOut)
        {
            if (audioSource.volume > 0f)
            {
                // Fade out current clip.
                audioSource.volume -= FadeSpeed * Time.deltaTime;
            }
            else
            {
                // Stop fading out.
                fadeState = FadeState.None;
                // Stop audio
                audioSource.Stop();
            }
        }
        else if (fadeState == FadeState.FadingIn)
        {
            if (audioSource.volume < finalVolume)
            {
                // Fade in next clip.
                audioSource.volume += FadeSpeed * Time.deltaTime;
            }
            else
            {
                audioSource.volume = finalVolume;
                // Stop fading in.
                fadeState = FadeState.None;
            }
        }
    }

}