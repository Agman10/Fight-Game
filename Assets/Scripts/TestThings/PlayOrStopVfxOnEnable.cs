using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayOrStopVfxOnEnable : MonoBehaviour
{
    public VisualEffect visualEffect;
    public bool stopOnEnable = true;
    private void OnEnable()
    {
        if (this.visualEffect != null)
        {
            if (this.stopOnEnable)
                this.visualEffect.Stop();
            else
                this.visualEffect.Play();
        }
    }

    private void OnDisable()
    {
        
    }
}
