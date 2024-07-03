using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBTest : MonoBehaviour
{
    public Light light;
    public Light[] lights;
    public MeshRenderer[] meshRenderers;
    public float hue;
    public float speed = 0.1f;

    [Space]
    public float baseColorSaturation = 0.1f;
    public float baseColorBrightness = 1f;

    [Space]
    public float emissionColorIntensity = 5f;
    public float emissionColorSaturation = 1f;

    [Space]
    public MeshRenderer meshRenderer;
    
    //public ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        this.hue = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        this.hue += this.speed * Time.deltaTime;
        if (this.hue >= 1f)
            this.hue = 0f;
        if (this.light != null)
            this.light.color = Color.HSVToRGB(this.hue, this.baseColorSaturation, 1f);

        foreach (Light lightt in this.lights)
        {
            if (lightt != null)
                lightt.color = Color.HSVToRGB(this.hue, this.baseColorSaturation, 1f);
        }

        //Debug.Log(this.hue * 360f);

        if (this.meshRenderer != null)
        {
            this.meshRenderer.material.SetColor("_BaseColor", Color.HSVToRGB(this.hue, this.baseColorSaturation, this.baseColorBrightness));
            this.meshRenderer.material.SetColor("_EmissionColor", Color.HSVToRGB(this.hue, this.emissionColorSaturation, this.emissionColorIntensity, true));
            //this.meshRenderer.material.SetColor("Color_7022ee03b5e44f1aaf226a0eb8093b59", Color.HSVToRGB(this.hue, 1f, 1f));
        }

        foreach (MeshRenderer renderer in this.meshRenderers)
        {
            renderer.material.SetColor("_BaseColor", Color.HSVToRGB(this.hue, this.baseColorSaturation, this.baseColorBrightness));
            renderer.material.SetColor("_EmissionColor", Color.HSVToRGB(this.hue, this.emissionColorSaturation, this.emissionColorIntensity, true));
        }

        /*if (this.particleSystem != null)
        {
            this.particleSystem.startColor = Color.HSVToRGB(this.hue, 1f, 1f);
        }*/
    }
}
