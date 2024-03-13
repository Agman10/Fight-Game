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
        //Application.targetFrameRate = -1;
    }
}
