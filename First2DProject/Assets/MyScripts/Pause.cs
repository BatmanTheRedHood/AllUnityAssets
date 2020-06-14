using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // To pause
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 0; // To slow down = 0.25f;
        }

        // To resume
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
        }
    }
}
