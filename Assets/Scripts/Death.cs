using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Death : MonoBehaviour
{
    public bool cheatMode = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void TriggerDeath()
    {
        if (!cheatMode)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
