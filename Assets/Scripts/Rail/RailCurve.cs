using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailCurve : Rail
{
    public float angularSpeed;
    public float angleBegin;
    public float angleEnd;

    private float currentAngle; // we have to track current angle because localEulerAngles get converted to %360
    private float totalAngle;

    // Start is called before the first frame update
    void Start()
    {
        if (isOpened)
        {
            currentAngle = angleEnd;
            percentComplete = 1f;
        }
        else
        {
            currentAngle = angleBegin;
            percentComplete = 0f;
        }
        transform.localEulerAngles = new Vector3(0, currentAngle, 0);
        totalAngle = angleEnd - angleBegin;
    }

    public override void GoToBegin()
    {
        // Calculate direction towards beginning
        float sign = Math.Sign(angleBegin - currentAngle);
        // Calculate new angle according to current speed
        float newAngle = currentAngle + sign * angularSpeed * Time.deltaTime;
        // This is the angle between the new angle and the beginning angle
        float newAngleFromBegin = (newAngle - angleBegin);
        // We know we went to far if this product is negative
        if (Math.Sign(totalAngle * newAngleFromBegin) > 0)
        {
            // Still going the right way, apply newPosition
            currentAngle = newAngle;
            transform.localEulerAngles = new Vector3(0, newAngle, 0);
            percentComplete = newAngleFromBegin / totalAngle;
        }
        else
        {
            // Since we went beyond beginning angle, reset to beginning position
            currentAngle = angleBegin;
            transform.localEulerAngles = new Vector3 (0, angleBegin, 0);
            percentComplete = 0f;
        }
    }

    public override void GoToEnd()
    {
        // Calculate direction towards end angle
        float sign = Math.Sign(angleEnd - currentAngle);
        // Calculate new angle according to current speed
        float newAngle = currentAngle + sign * angularSpeed * Time.deltaTime;
        // This is the angle between the new angle and the beginning angle
        float newAngleFromEnd = (newAngle - angleEnd);
        // We know we went to far if this product is negative
        if (Math.Sign(totalAngle * newAngleFromEnd) < 0)
        {
            // Still going the right way, apply newPosition
            currentAngle = newAngle;
            transform.localEulerAngles = new Vector3(0, newAngle, 0);
            percentComplete = 1 + newAngleFromEnd / totalAngle;
        }
        else
        {
            // Since we went beyond beginning angle, reset to end position
            currentAngle = angleEnd;
            transform.localEulerAngles = new Vector3(0, angleEnd, 0);
            percentComplete = 1f;
        }
    }

}
