using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceMultipleCameraFovs : MonoBehaviour
{
    public AspectRatioForcer[] aspectRatioForcers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        foreach(AspectRatioForcer fovForcer in this.aspectRatioForcers)
        {
            if (fovForcer != null)
                fovForcer.FovFixer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
