using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPowerColor : MonoBehaviour
{
    public ColorCheck cc;

    private GameObject movingCrosshairLeft;
    private GameObject movingCrosshairRight;
    private GameObject halo;
    private DeathManager dm;

    // Start is called before the first frame update
    void Start()
    {
        movingCrosshairLeft = GameObject.Find("/Canvas/crosshairMovingLeft");
        movingCrosshairRight = GameObject.Find("/Canvas/crosshairMovingRight");
        halo = GameObject.Find("/Canvas/halo");
        dm = GameObject.Find("FPS").GetComponent<DeathManager>();
        cc = GameObject.Find("FPS").GetComponent<ColorCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        Color color = cc.PowerColorToColor(cc.powerColor);

        if (color == Color.white)
        {
            halo.SetActive(false);
        }
        else
        {
            halo.GetComponent<RawImage>().color = color;
            halo.SetActive(true);
        }

        float newAngle = -90f + 180f * dm.health;
        movingCrosshairLeft.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, newAngle);
        newAngle = -90f - 180f * dm.health;
        movingCrosshairRight.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, newAngle);
    }
}
