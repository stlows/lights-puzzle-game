using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BreakerRail : Breaker
{
    public Transform railTransform;

    private RailCurve railCurve;

    void Awake()
    {
        railCurve = railTransform.gameObject.GetComponent<RailCurve>();
        railCurve.isOpened = isOpened;
        ledUpMat = transform.Find("ArrowUp").GetComponent<Renderer>().material;
        ledDownMat = transform.Find("ArrowDown").GetComponent<Renderer>().material;
    }

    void Start()
    {
        BreakerStart();
    }

    public override void CloseSpecific()
    {
        railCurve.isOpened = false;
    }

    public override void OpenSpecific()
    {
        railCurve.isOpened = true;
    }

    public override void OnClickSpecific()
    {
        railCurve.isPaused = true;
    }

    public override void AfterDelaySpecific()
    {
        railCurve.isPaused = false;
    }
}
