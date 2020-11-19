using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailLine : Rail
{
    public float speed = 1f;
    public Vector3 positionBegin;
    public Vector3 positionEnd;
    private Vector3 beginToEnd; 

    void Start()
    {
        percentComplete = 0f;
        transform.localPosition = positionBegin;
        // Vector from beginning position to end position
        beginToEnd = (positionEnd - positionBegin);
    }

    public override void GoToBegin()
    {
        // Calculate direction towards beginning
        Vector3 direction = (positionBegin - transform.localPosition).normalized;
        // Calculate new position according to current speed
        Vector3 newPosition = transform.localPosition + direction * speed * Time.deltaTime;
        // This is the vector between the new position and the beginning position
        Vector3 newPositionToBegin = (positionBegin - newPosition);
        // We know we went to far if the dot product is positive
        if (Vector3.Dot(beginToEnd, newPositionToBegin) < 0)
        {
            // Still going the right way, apply newPosition
            transform.localPosition = newPosition;
            percentComplete = newPositionToBegin.magnitude / beginToEnd.magnitude;
        }
        else
        {
            // Since we went beyond beginning position, reset to beginning position
            transform.localPosition = positionBegin;
            percentComplete = 0f;
        }
    }

    public override void GoToEnd()
    {
        // Calculate direction towards end
        Vector3 direction = (positionEnd - transform.localPosition).normalized;
        // Calculate new position according to current speed
        Vector3 newPosition = transform.localPosition + direction * speed * Time.deltaTime;
        // This is the vector between the new position and the end position
        Vector3 newPositionToEnd = (positionEnd - newPosition);
        // We know we went to far if the dot product is negative
        if (Vector3.Dot(beginToEnd, newPositionToEnd) > 0)
        {
            // Still going the right way, apply newPosition
            transform.localPosition = newPosition;
            percentComplete = 1f - newPositionToEnd.magnitude / beginToEnd.magnitude;
        }
        else
        {
            // Since we went beyond end position, reset to end position
            transform.localPosition = positionEnd;
            percentComplete = 1f;
        }
    }
}
