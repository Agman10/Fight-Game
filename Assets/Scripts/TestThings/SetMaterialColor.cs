using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterialColor : MonoBehaviour
{
    private Renderer materialRenderer;
    [ColorUsage(true, false)] public Color color = new Color(1f, 1f, 1f, 1f);
    [ColorUsage(false, true)] public Color emission;

    // Start is called before the first frame update
    /*void Start()
    {
        this.materialRenderer = this.GetComponent<Renderer>();
    }*/

    private void Awake()
    {
        this.materialRenderer = this.GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        //this.SetColor();
    }

    // Update is called once per frame
    void Update()
    {
        this.SetColor();
    }

    public void SetColor()
    {
        if (this.materialRenderer != null && this.materialRenderer.material != null)
        {
            this.materialRenderer.material.SetColor("_BaseColor", this.color);
            this.materialRenderer.material.SetColor("_EmissionColor", this.emission);
        }
    }
}
