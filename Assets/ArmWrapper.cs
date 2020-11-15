using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmWrapper : MonoBehaviour
{
    private Material sphereUpMaterial;
    private Material sphereDownMaterial;

    void Start()
    {
        sphereDownMaterial = transform.Find("../SphereDown").GetComponent<Renderer>().sharedMaterial;
        sphereUpMaterial = transform.Find("../SphereUp").GetComponent<Renderer>().sharedMaterial;
    }

    public void UpdateColor(bool isOpened)
    {
        if (isOpened)
        {
            transform.Find("Arm").GetComponent<MeshRenderer>().material = sphereUpMaterial;
        }
        else
        {
            transform.Find("Arm").GetComponent<MeshRenderer>().material = sphereDownMaterial;
        }
    }
}
