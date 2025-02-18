using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLimit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        //Screen.SetResolution(1920, 1080, true);
        //Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        //Screen.SetResolution(Screen.width, Screen.height, true);

        //Screen.SetResolution(WIDTH, HEIGHT, IS_FULLSCREEN);
        //Application.targetFrameRate = -1;
    }
}
