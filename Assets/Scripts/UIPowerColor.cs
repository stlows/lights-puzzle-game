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
        indicator.color = playerColorCheck.groundColor;
    }
}
