using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilingOffsetAnimation : MonoBehaviour
{
    //public MeshRenderer[] meshRenderers;
    public MeshRenderer meshRenderer;

    public Vector2 tilingDirection;

    private Vector2 tiling;
    private Vector2 initialTiling;

    //make it so it resets the tiling to where it was
    private void OnEnable()
    {
        if (this.meshRenderer != null)
        {
            this.initialTiling = this.meshRenderer.material.GetTextureOffset("_BaseMap");
            this.tiling = this.initialTiling;
            //this.tiling = this.meshRenderer.material.GetTextureOffset("_BaseMap");
        }
    }
    private void OnDisable()
    {
        //this.tiling = this.initialTiling;
        if (this.meshRenderer != null)
        {
            this.meshRenderer.material.SetTextureOffset("_BaseMap", this.initialTiling);
        }
        this.tiling = new Vector2(0f, 0f);
        this.initialTiling = new Vector2(0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        this.tiling += new Vector2(this.tilingDirection.x * Time.deltaTime, this.tilingDirection.y * Time.deltaTime);

        if (this.meshRenderer != null)
        {
            this.meshRenderer.material.SetTextureOffset("_BaseMap", this.tiling);
        }

        /*foreach (MeshRenderer renderer in this.meshRenderers)
        {
            renderer.material.SetTextureOffset("_BaseMap", this.tiling);
        }*/
    }
}
