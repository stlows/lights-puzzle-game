using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpotController : MonoBehaviour
{
    protected Material ledUpMat;
    protected Material ledDownMat;



    protected void ledsOpenState()
    {
        turnOn(ledUpMat);
        turnOff(ledDownMat);
    }
    protected void ledsClosedState()
    {
        turnOn(ledDownMat);
        turnOff(ledUpMat);
    }


    private void turnOn(Material ledMat)
    {
        //if (ledMat.color == Color.black)
        //{
        //    ledMat.EnableKeyword("_SPECULARHIGHLIGHTS_OFF");
        //}
        //else
        //{
            ledMat.EnableKeyword("_EMISSION");
        //}
    }
    private void turnOff(Material ledMat)
    {
        //if (ledMat.color == Color.black)
        //{
        //    ledMat.DisableKeyword("_SPECULARHIGHLIGHTS_OFF");
        //}
        //else
        //{
            ledMat.DisableKeyword("_EMISSION");
        //}
    }


}
