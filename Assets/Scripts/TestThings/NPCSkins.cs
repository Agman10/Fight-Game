using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSkins : MonoBehaviour
{
    public Color[] colors;
    public Material[] materials;
    public Material mainBodyMaterial;
    public Renderer[] renderers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
        else
        {
            Debug.Log("Not in play mode");
        }
    }
}
