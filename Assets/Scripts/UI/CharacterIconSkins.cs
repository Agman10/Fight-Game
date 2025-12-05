using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CharacterIconSkins : MonoBehaviour
{
    public SO_Skin skin;
    public IconLayer[] iconLayers;

    //public Material eyeEmssionMaterial;
    //public Image emissionEye;
    public Image[] emissionEyes;

    public IconLayer[] iconColorLayers;

    private void OnEnable()
    {
        if (this.skin != null)
        {
            this.SetSkin(this.skin);
        }
    }

    public void SetSkin(SO_Skin skinData)
    {
        if(skinData != null)
        {
            this.skin = skinData;
            foreach (IconLayer iconLayer in this.iconLayers)
            {
                foreach (Image layer in iconLayer.layers)
                {
                    if (layer != null && this.skin.colors.Length >= iconLayer.colorId + 1)
                    {
                        //layer.color = this.skin.colors[iconLayer.colorId];
                        layer.color = new Color(this.skin.colors[iconLayer.colorId].r, this.skin.colors[iconLayer.colorId].g, this.skin.colors[iconLayer.colorId].b, layer.color.a);

                        //REMOVE THIS WHEN UNKNOWN ENTITY NO LONGER HAS TRANSPARENT BASE COLOR BECAUSE OF THEIR GHOST!!
                        /*Color c = layer.color;
                        c.a = 1f;
                        layer.color = c;*/
                    }
                }
            }

            /*if(this.emissionEye != null && this.eyeEmssionMaterial != null)
            {
                this.emissionEye.material = this.eyeEmssionMaterial.in;
            }*/

            foreach (Image emissionEye in this.emissionEyes)
            {

                if (emissionEye.material != null)
                {
                    Material material = new Material(emissionEye.material);
                    material.name = material.name + " (Instance)";
                    //Material material = new Material;
                    material.SetColor("_EmissionColor", this.skin.eyeEmission);

                    emissionEye.material = material;


                    //emissionEye.material.SetColor("_EmissionColor", this.skin.eyeEmission);
                }



                // In your script, get the HDR color from your material or shader
                //Color hdrColor = material.GetColor("_EmissionColor");
                /*Color hdrColor = this.skin.eyeEmission;

                // Convert to SDR
                Color sdrColor = ConvertHDRToSDR(hdrColor);

                // Optionally, you can adjust the exposure before converting
                //float exposure = 3.0f; // Example exposure value
                float exposure = this.CalculateHDRIntensity(hdrColor);

                //float exposure = hdrColor.g;
                Debug.Log(exposure);


                Color exposedHdrColor = hdrColor * Mathf.Pow(2.0f, exposure);
                //Color exposedHdrColor = hdrColor * exposure;
                sdrColor = ConvertHDRToSDR(exposedHdrColor);

                // Apply the SDR color back to your material
                //material.SetColor("_BaseColor", sdrColor);

                emissionEye.color = sdrColor;*/







                /*// In your script, get the HDR color from your material or shader
                Color hdrColor = material.GetColor("_EmissionColor");

                // Convert to SDR
                Color sdrColor = ConvertHDRToSDR(hdrColor);

                // Optionally, you can adjust the exposure before converting
                float exposure = 1.0f; // Example exposure value
                Color exposedHdrColor = hdrColor * Mathf.Pow(2.0f, exposure);
                Color sdrColor = ConvertHDRToSDR(exposedHdrColor);

                // Apply the SDR color back to your material
                material.SetColor("_BaseColor", sdrColor);*/

            }

            foreach (IconLayer iconColorLayer in this.iconColorLayers)
            {
                foreach (Image layer in iconColorLayer.layers)
                {
                    if (layer != null && this.skin.iconColors.Length >= iconColorLayer.colorId + 1)
                    {
                        layer.color = this.skin.iconColors[iconColorLayer.colorId];
                        //layer.color = new Color(this.skin.iconColors[iconColorLayer.colorId].r, this.skin.iconColors[iconColorLayer.colorId].g, this.skin.iconColors[iconColorLayer.colorId].b, layer.color.a);
                    }
                }
            }
        }

        
    }

    public static Color ConvertHDRToSDR(Color hdrColor, float maxBrightness = 10f)
    {
        float maxComponent = Mathf.Max(hdrColor.r, hdrColor.g, hdrColor.b);
        if (maxComponent <= maxBrightness)
        {
            hdrColor.a = 1;
            return hdrColor;
        }
        float scale = maxBrightness / maxComponent;
        //return new Color(hdrColor.r * scale, hdrColor.g * scale, hdrColor.b * scale, hdrColor.a);
        return new Color(hdrColor.r * scale, hdrColor.g * scale, hdrColor.b * scale, 1);
    }

    float CalculateHDRIntensity(Color color)
    {
        float maxComponent = Mathf.Max(color.r, Mathf.Max(color.g, color.b));
        float scaleFactor = 255f / maxComponent;
        float intensity = Mathf.Log(255f / scaleFactor) / Mathf.Log(2f);
        return intensity;
    }

    /*public static Color Tonemap(Color hdrColor)
    {
        float exposure = 1.0f; // Adjust exposure as needed
        return hdrColor / (hdrColor + Vector4.one); //Simplified Reinhard
    }*/

}

[Serializable]
public class IconLayer
{
    public Image[] layers;
    public int colorId;
}
