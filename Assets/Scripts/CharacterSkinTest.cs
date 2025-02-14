using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkinTest : MonoBehaviour
{
    public SO_Skin skin;
    public Material[] materials;
    public Material mainBodyMaterial;
    public Renderer[] renderers;

    public int eyeIndex;
    public bool glowingEyes;

    [Space]
    public bool useExtraEmission;
    public Renderer[] extraEmissionRenderers;

    [Space]
    public bool useMultiColorMats;
    public MultiColorMaterials[] multiColoredMats;


    //public Renderer[] avoiding;
    // Start is called before the first frame update
    void Start()
    {
        if (this.skin != null)
            this.ChangeSkin(this.skin);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSkin(SO_Skin skin)
    {
        if (skin != null)
            this.ChangeSkin(skin);
    }
    public void ChangeSkin(SO_Skin skinData)
    {
        //int val = 0;
        /*Renderer[] children;
        children = GetComponentsInChildren<Renderer>();*/

        /*for (int i = 0; i <= children.Length - 1; i++)
        {
            *//*if (avoiding[j] == children[i])
            {
                Debug.Log("t");
                continue;
            }*//*
            for (int j = 0; j <= this.avoiding.Length - 1; j++)
            {
                if (avoiding[j] == children[i])
                {
                    Debug.Log("t");
                    return;
                }

            }
        }*/
        this.skin = skinData;
        //renderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i <= this.materials.Length - 1; i++)
        {
            foreach (Renderer rend in this.renderers)
            {
                //Debug.Log(rend.material);
                if (i > skinData.colors.Length - 1)
                    continue;
                foreach (Material material in rend.materials)
                {
                    if (material.name == this.materials[i].name + " (Instance)")
                    {
                        material.SetColor("_BaseColor", skinData.colors[i]);

                        /*material.EnableKeyword("_EMISSION");
                        material.SetColor("_EmissionColor", skinData.colors[i]);*/
                        if (this.glowingEyes && i == this.eyeIndex)
                            material.SetColor("_EmissionColor", skinData.eyeEmission);
                    }
                }
            }
        }
        if(this.mainBodyMaterial != null && skinData.colors.Length >= 2)
        {
            foreach (Renderer rend in this.renderers)
            {
                //Debug.Log(rend.material);
                foreach (Material material in rend.materials)
                {
                    if (material.name == this.mainBodyMaterial.name + " (Instance)")
                    {
                        material.SetColor("Color_83cbc8d686554d0f9aced64306129077", skinData.colors[0]);
                        material.SetColor("Color_1", skinData.colors[1]);
                    }
                }
            }
        }
        if(this.useExtraEmission && this.extraEmissionRenderers.Length > 0)
        {
            foreach (Renderer rend in this.extraEmissionRenderers)
            {
                foreach (Material material in rend.materials)
                {
                    if (material != null)
                    {
                        material.SetColor("_EmissionColor", skinData.extraEmission);
                        material.SetColor("_BaseColor", skinData.extraColor);
                        /*if (material.name == material.name + " (Instance)")
                        {
                            Debug.Log("test");
                            material.SetColor("_EmissionColor", skinData.extraEmission);
                        }*/
                    }
                }
            }
        }

        /*for (int i = 0; i <= this.extraEmissionRenderers.Length - 1; i++)
        {
            
        }*/


        /*if (this.glowingEyes)
        {

        }*/
        if (this.useMultiColorMats)
            this.CheckMultiColorMats(skinData);
    }

    public void CheckMultiColorMats(SO_Skin skinData)
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
                            mat.SetColor("Color_83cbc8d686554d0f9aced64306129077", skinData.colors[multiColor.colorIndex1]);
                            mat.SetColor("Color_1", skinData.colors[multiColor.colorIndex2]);
                            mat.SetColor("Color_42f013fb876e4189a051c4bba88d09bc", skinData.colors[multiColor.colorIndex3]);
                        }
                    }
                }
            }
        }
    }

}

[System.Serializable]
public class MultiColorMaterials
{
    //public Material material;
    public Renderer[] renderers;

    public int colorIndex1;
    public int colorIndex2;
    public int colorIndex3;
}