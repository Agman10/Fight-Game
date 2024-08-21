using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSkins : MonoBehaviour
{
    public bool changeSkinOnUpdate = false;

    public Color[] colors;
    public Material[] materials;
    public Material mainBodyMaterial;
    public Renderer[] renderers;

    [Space]
    public bool useMultiColorMats;
    public MultiColorMaterials[] multiColoredMats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.changeSkinOnUpdate)
            this.ChangeSkin();
    }

    private void OnEnable()
    {
        this.ChangeSkin();
    }

    [ContextMenu("GetAllRenderers")]
    public void GetAllRenderers()
    {
        Renderer[] children;
        children = GetComponentsInChildren<Renderer>();
        this.renderers = children;
    }

    [ContextMenu("ChangeSkin")]
    public void ChangeSkin()
    {
        if (Application.isPlaying)
        {
            for (int i = 0; i <= this.materials.Length - 1; i++)
            {
                foreach (Renderer rend in this.renderers)
                {
                    if (i > this.colors.Length - 1)
                        continue;
                    foreach (Material material in rend.materials)
                    {
                        if (material.name == this.materials[i].name + " (Instance)")
                        {
                            material.SetColor("_BaseColor", this.colors[i]);
                        }
                    }
                }
            }
            if (this.mainBodyMaterial != null && this.colors.Length >= 2)
            {
                foreach (Renderer rend in this.renderers)
                {
                    foreach (Material material in rend.materials)
                    {
                        if (material.name == this.mainBodyMaterial.name + " (Instance)")
                        {
                            material.SetColor("Color_83cbc8d686554d0f9aced64306129077", this.colors[0]);
                            material.SetColor("Color_1", this.colors[1]);
                        }
                    }
                }
            }


            if (this.useMultiColorMats)
                this.CheckMultiColorMats();
        }
        else
        {
            Debug.Log("Not in play mode");
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
                        }
                    }
                }
            }
        }
    }
}
