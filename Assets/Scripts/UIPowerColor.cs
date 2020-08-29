using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPowerColor : MonoBehaviour
{
    public ColorCheck playerColorCheck;

    private Image indicator;

    // Start is called before the first frame update
    void Start()
    {
        indicator = transform.gameObject.GetComponent<Image>();
        indicator.canvasRenderer.SetAlpha(1f);
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerColorCheck.powerColor)
        {
            case PowerColor.GREEN:
                indicator.color = Color.green;
                break;
            case PowerColor.RED:
                indicator.color = Color.red;
                break;
            case PowerColor.BLUE:
                indicator.color = Color.blue;
                break;
            case PowerColor.YELLOW:
                indicator.color = Color.yellow;
                break;
            case PowerColor.MAGENTA:
                indicator.color = Color.magenta;
                break;
            case PowerColor.CYAN:
                indicator.color = Color.cyan;
                break;
            default:
                indicator.color = Color.white;
                break;
        }
    }
}
