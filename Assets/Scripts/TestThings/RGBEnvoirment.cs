using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBEnvoirment : MonoBehaviour
{
    public Light directionalLight;
    public float hue;
    public float speed = 0.5f;

    public float directionalLightSaturation = 0.16f;
    public float directionalLightBrightness = 1f;


    public float ambientLightSaturation = 0.1f;
    public float ambientLightBrightness = 0.51f;
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
        else if (this.hue < 0f)
            this.hue = 1f;


        if (this.directionalLight != null)
            this.directionalLight.color = Color.HSVToRGB(this.hue, this.directionalLightSaturation, this.directionalLightBrightness);

        RenderSettings.ambientLight = Color.HSVToRGB(this.hue, ambientLightSaturation, this.ambientLightBrightness);
    }
}
