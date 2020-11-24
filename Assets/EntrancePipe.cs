using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrancePipe : MonoBehaviour
{
    void Start()
    {
        transform.position = GameObject.Find("FPS").transform.position;
        transform.localEulerAngles = GameObject.Find("FPS").transform.localEulerAngles;
    }
}
