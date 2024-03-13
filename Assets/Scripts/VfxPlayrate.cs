using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VfxPlayrate : MonoBehaviour
{
    private VisualEffect visualEffect;
    public float playrate = 1f;

    private void Awake()
    {
        this.visualEffect = GetComponent<VisualEffect>();
    }
    private void Update()
    {
        if (this.visualEffect != null)
            this.visualEffect.playRate = this.playrate;
    }
}