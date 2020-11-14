using System;
using System.Linq;
using UnityEngine;

public class ColorCheck : MonoBehaviour
{
    public RenderTexture colorCheckTexture;
    public Color groundColor;
    public PowerColor powerColor;

    [HideInInspector]
    public  PowerColor prevPowerColor;
    [HideInInspector]
    public Color[] colorArray = {
        Color.white,
        Color.red,
        Color.green,
        Color.blue,
        Color.cyan,
        Color.magenta,
        Color.yellow,
        Color.black
    };

    private Texture2D tex;
    private FadingAudioSource greenAudio;

    // Start is called before the first frame update
    void Start()
    {
        groundColor = Color.white;
        prevPowerColor = PowerColor.WHITE;
        powerColor = PowerColor.WHITE;
        greenAudio = transform.Find("Sounds").Find("Green").gameObject.GetComponent<FadingAudioSource>();
    }


// Update is called once per frame
public void UpdatePowerColor(bool isShadowed)
    {
        // Remember for next frame
        prevPowerColor = powerColor;

        groundColor = MeasureGroundColor();
        
        if (isShadowed)
        {
            // No powers for you if you're in the dark
            powerColor = PowerColor.WHITE;
        }
        else
        {
            powerColor = CalculatePowerColor();
        }

        return;
    }


    private Color MeasureGroundColor()
    {
        // Remember currently active render texture
        RenderTexture currentActiveRT = RenderTexture.active;

        // Set the supplied RenderTexture as the active one
        RenderTexture.active = colorCheckTexture;

        // Create a new Texture2D and read the RenderTexture image into it
        tex = new Texture2D(colorCheckTexture.width, colorCheckTexture.height);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);

        // Restore previously active render texture
        RenderTexture.active = currentActiveRT;

        return tex.GetPixel(tex.width / 2, tex.height / 2);
    }


    private PowerColor CalculatePowerColor()
    {
        if ((groundColor.r == groundColor.b) && (groundColor.r == groundColor.g))
        {
            // If the color is a shade of grey, no powers 
            // (this is to avoid glitches in transitions between black and white)
            return PowerColor.WHITE; // No power.
        }
        else
        {
            double[] distanceArray = new double[colorArray.Length - 1];
            for (int i = 0; i < distanceArray.Length; i++)
            {
                distanceArray[i] = euclidianDistance(groundColor, colorArray[i]);
            }
            return (PowerColor)Array.IndexOf(distanceArray, distanceArray.Min());
        }
    }

    private void LateUpdate()
    {
        if (prevPowerColor != powerColor)
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
            greenAudio.FadeIn();
        }
        else
        {
            greenAudio.FadeOut();
        }
    }

    public Color PowerColorToColor(PowerColor pc)
    {
        return colorArray[(int)pc];
    }
}
