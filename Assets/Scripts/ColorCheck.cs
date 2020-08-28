using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using System.Linq;
using UnityEngine;

public class ColorCheck : MonoBehaviour
{
    public RenderTexture colorCheckTexture;
    public Color groundColor;
    public PowerColor powerColor;

    private Texture2D tex;
    private PowerColor previousPowerColor = PowerColor.WHITE;
    private AudioSource greenAudio;

    // Start is called before the first frame update
    void Start()
    {
        greenAudio = transform.Find("Sounds").Find("Green").gameObject.GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        // Remember current power color
        previousPowerColor = powerColor;

        // Remember currently active render texture
        RenderTexture currentActiveRT = RenderTexture.active;

        // Set the supplied RenderTexture as the active one
        RenderTexture.active = colorCheckTexture;

        // Create a new Texture2D and read the RenderTexture image into it
        tex = new Texture2D(colorCheckTexture.width, colorCheckTexture.height);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);

        // Restore previously active render texture
        RenderTexture.active = currentActiveRT;

        groundColor = tex.GetPixel(tex.width/2, tex.height/2);

        if ((groundColor.r == groundColor.b) && (groundColor.r == groundColor.g))
        {
            // If the color is a shade of grey, no powers 
            // (this is to avoid glitches in transitions between black and white)
            powerColor = PowerColor.WHITE; // No power.
        }
        else
        {
            Color[] colorArray = {
            Color.white,
            Color.red,
            Color.green,
            Color.blue,
            Color.cyan,
            Color.magenta,
            Color.yellow
            };
            double[] distanceArray = new double[colorArray.Length];
            for (int i = 0; i < colorArray.Length; i++)
            {
                distanceArray[i] = euclidianDistance(groundColor, colorArray[i]);
            }

            powerColor = (PowerColor)Array.IndexOf(distanceArray, distanceArray.Min());
        }

        if (previousPowerColor != powerColor)
        {
            PlayColorSound();
        }

    }
    private double euclidianDistance(Color color1, Color color2)
    {
        double rDiff = color1.r - color2.r;
        double gDiff = color1.g - color2.g;
        double bDiff = color1.b - color2.b;
        // https://stackoverflow.com/questions/1847092/given-an-rgb-value-what-would-be-the-best-way-to-find-the-closest-match-in-the-d
        // Future improvement: use weighted approach?
        return Math.Sqrt((rDiff * rDiff) + (gDiff * gDiff) + (bDiff * bDiff));
    }

    private void PlayColorSound()
    {
        if (powerColor == PowerColor.GREEN)
        {
            greenAudio.Play();
        }
        else
        {
            greenAudio.Stop();
        }
    }
}
