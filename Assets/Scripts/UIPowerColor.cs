using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPowerColor : MonoBehaviour
{
    private ColorCheck cc;
    private GameObject movingCrosshairLeft;
    private GameObject movingCrosshairRight;
    private GameObject blackPieLeft;
    private GameObject blackPieRight;
    private GameObject halo;
    private DeathManager dm;

    // Start is called before the first frame update
    void Start()
    {
        movingCrosshairLeft = GameObject.Find("/Canvas/crosshair/crosshairMovingLeft");
        movingCrosshairRight = GameObject.Find("/Canvas/crosshair/crosshairMovingRight");
        blackPieLeft = GameObject.Find("/Canvas/blackPie/blackPieLeft");
        blackPieRight = GameObject.Find("/Canvas/blackPie/blackPieRight");
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

        float newAngleLeft = -90f + 180f * dm.health;
        movingCrosshairLeft.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, newAngleLeft);
        float newAngleRight = -90f - 180f * dm.health;
        movingCrosshairRight.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, newAngleRight);
        
        blackPieLeft.GetComponent<Image>().fillAmount = (1 - dm.health) / 2;
        blackPieRight.GetComponent<Image>().fillAmount = (1 - dm.health) / 2;
    }
}
