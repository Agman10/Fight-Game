using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilingOffsetAnimation : MonoBehaviour
{
    public MeshRenderer[] meshRenderers;

    public Vector2 tilingDirection;

    public Vector2 tiling;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.tiling += new Vector2(this.tilingDirection.x * Time.deltaTime, this.tilingDirection.y * Time.deltaTime);

        foreach (MeshRenderer renderer in this.meshRenderers)
        {
            renderer.material.SetTextureOffset("_BaseMap", this.tiling);
        }
    }
}
