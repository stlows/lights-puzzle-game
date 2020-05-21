using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimumDistance : MonoBehaviour
{
    public float minimumDistance = 8f;
    private Transform player;

    void Start()
    {
        player = GameObject.Find("FPS").transform;
    }

    public bool Check()
    {
        return Vector3.Distance(player.position, transform.position) < minimumDistance;
    }
}
