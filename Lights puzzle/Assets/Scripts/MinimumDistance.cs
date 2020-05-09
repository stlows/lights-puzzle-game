using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimumDistance : MonoBehaviour
{
    public float minimumDistance;
    public Transform player;

    public bool Check()
    {
        return Vector3.Distance(player.position, transform.position) < minimumDistance;

    }
}
