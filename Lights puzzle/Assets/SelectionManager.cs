using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour {

    private GameObject _selection;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform.gameObject;
            var selectionHoverable = selection.GetComponent<Hoverable>();
            if (selectionHoverable != null)
            {
                if (selectionHoverable.isHoverable)
                {
                    if (selection != _selection)
                    {
                        ResetSelectionRender();
                        _selection = selection;
                        SetSelectionRender();
                    }
                    return;
                }
            }
            

        }

        ResetSelectionRender();
        _selection = null;

    }

    private void ResetSelectionRender()
    {
        if (_selection != null)
        {
            _selection.GetComponent<Renderer>().material = _selection.GetComponent<Hoverable>().defaultMaterial;
        }
    }

    private void SetSelectionRender()
    {
        if (_selection != null)
        {
            _selection.GetComponent<Renderer>().material = _selection.GetComponent<Hoverable>().hoveredMaterial;
        }
    }
}
