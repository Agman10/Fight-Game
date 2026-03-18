using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCSkinsNew : MonoBehaviour
{
    public Color[] colors;

    [ColorUsage(false, true)] public Color[] emissionColors;

    public SkinRenderers[] skinRenderers;
    public MultiColorMaterials[] multiColoredMats;
    public SkinRenderers[] emissionRenderers;

    public IconLayer[] iconLayers;

    private void OnEnable()
    {
        this.ChangeSkin();
    }

    void Update()
    {
        this.ChangeSkin();
    }

    [ContextMenu("ChangeSkin")]
    public void ChangeSkin()
    {
        if (Application.isPlaying)
        {
            /*foreach(SkinRenderers renderers in this.skinRenderers)
            {

            }*/
            for (int i = 0; i <= this.skinRenderers.Length - 1; i++)
            {
                foreach (Renderer rend in this.skinRenderers[i].renderers)
                {
                    if (i > this.colors.Length - 1 || rend == null)
                        continue;
                    foreach (Material material in rend.materials)
                    {
                        material.SetColor("_BaseColor", this.colors[i]);
                        //material.SetColor("_BaseColor", new Color(this.colors[i].r, this.colors[i].g, this.colors[i].b, material.color.a));
                    }
                }
            }

            for (int i = 0; i <= this.emissionRenderers.Length - 1; i++)
            {
                foreach (Renderer rend in this.emissionRenderers[i].renderers)
                {
                    if (i > this.emissionColors.Length - 1 || rend == null)
                        continue;
                    foreach (Material material in rend.materials)
                    {
                        material.SetColor("_EmissionColor", this.emissionColors[i]);
                    }
                }
            }

            this.CheckMultiColorMats();

            this.ChangeIconColors();
        }
    }

    public void CheckMultiColorMats()
    {
        if (this.multiColoredMats.Length > 0)
        {
            foreach (MultiColorMaterials multiColor in this.multiColoredMats)
            {
                /*if(multiColor.material != null)
                {

                }*/

                if (multiColor.renderers.Length > 0)
                {
                    foreach (Renderer rend in multiColor.renderers)
                    {
                        foreach (Material mat in rend.materials)
                        {
                            mat.SetColor("Color_83cbc8d686554d0f9aced64306129077", colors[multiColor.colorIndex1]);
                            mat.SetColor("Color_1", colors[multiColor.colorIndex2]);
                            mat.SetColor("Color_42f013fb876e4189a051c4bba88d09bc", colors[multiColor.colorIndex3]);
                            mat.SetColor("Color_D", colors[multiColor.colorIndex4]);
                        }
                    }
                }
            }
        }
    }

    public void ChangeIconColors()
    {
        foreach (IconLayer iconLayer in this.iconLayers)
        {
            foreach (Image layer in iconLayer.layers)
            {
                if (layer != null && this.colors.Length >= iconLayer.colorId + 1)
                {
                    //layer.color = this.skin.colors[iconLayer.colorId];
                    //layer.color = new Color(this.colors[iconLayer.colorId].r, this.colors[iconLayer.colorId].g, this.colors[iconLayer.colorId].b, layer.color.a);
                    layer.color = this.colors[iconLayer.colorId];

                    //REMOVE THIS WHEN UNKNOWN ENTITY NO LONGER HAS TRANSPARENT BASE COLOR BECAUSE OF THEIR GHOST!!
                    /*Color c = layer.color;
                    c.a = 1f;
                    layer.color = c;*/
                }
            }
        }
    }
}

[System.Serializable]
public class SkinRenderers
{
    //public Material material;
    public Renderer[] renderers;
}

/*[System.Serializable]
public class EmissionRenderers
{
    //public Material material;
    public Renderer[] renderers;
    //public int emissionIndex;
}*/
