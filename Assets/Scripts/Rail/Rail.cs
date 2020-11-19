using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rail : MonoBehaviour
{
    public float percentComplete;
    public bool isOpened; // true = GoToEnd, false = GoToBegin
    public bool isPaused;

    private void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            if (isOpened && percentComplete < 1f)
            {
                GoToEnd();
            }
            else if (!isOpened && percentComplete > 0f)
            {
                GoToBegin();
            }
        }
    }

    public abstract void GoToEnd();
    public abstract void GoToBegin();
}
