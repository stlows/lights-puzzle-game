using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Environments;
using UnityEngine;
 
public class BreakerRail : Breaker
{
    public Transform railTransform;

    private RailCurve railCurve;

    void Start()
    {
        railCurve = railTransform.gameObject.GetComponent<RailCurve>();
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
}
