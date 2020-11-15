using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotOutlineShader : MonoBehaviour
{

    private Material cylinder;
    private Light spot;
    private Material cube;
    private Material box;
    void Start()
    {
        cylinder = transform.Find("Base/Cylinder").GetComponent<Renderer>().material;
        cube = transform.Find("Base/Cube").GetComponent<Renderer>().material;
        box = transform.Find("Rotating/Box").GetComponent<Renderer>().material;
        spot = transform.Find("Rotating/Light").GetComponent<Light>();
    }

    void Update()
    {
        if ((spot.color == Color.black) || (spot.intensity == 0))
        {
            cylinder.SetFloat("_FirstOutlineWidth", 0f);
            box.SetFloat("_FirstOutlineWidth", 0f);
            cube.SetFloat("_FirstOutlineWidth", 0f);
        }
        else
        {
            cylinder.SetFloat("_FirstOutlineWidth", 0.05f);
            box.SetFloat("_FirstOutlineWidth", 0.05f);
            cube.SetFloat("_FirstOutlineWidth", 0.05f);
        }
        cylinder.SetVector("_FirstOutlineColor", (Vector4) spot.color);
        box.SetVector("_FirstOutlineColor", (Vector4) spot.color);
        cube.SetVector("_FirstOutlineColor", (Vector4) spot.color);
    }
}
