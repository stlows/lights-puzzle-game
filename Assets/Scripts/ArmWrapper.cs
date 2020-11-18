using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmWrapper : MonoBehaviour
{
    public MeshRenderer[] affectedRenderers;

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
            foreach (MeshRenderer rend in affectedRenderers) 
            {
                rend.material = sphereUpMaterial;
            }
        }
        else
        {
            foreach (MeshRenderer rend in affectedRenderers)
            {
                rend.material = sphereDownMaterial;
            }
        }
    }
}
