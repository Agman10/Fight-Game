using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxAnimation : MonoBehaviour
{
    /*float xDir;
    float yDir;*/
    public Material skybox;
    public float unscaledTime;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(skybox.material.shaderKeywords[0]);
        
    }

    private void OnEnable()
    {
        this.unscaledTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        /*xDir += 0.05f * Time.deltaTime;
        yDir -= 0.05f * Time.deltaTime;
        if(this.skybox != null)
        {
            skybox.SetTextureOffset("_FrontTex", new Vector2(xDir, yDir));
            skybox.SetTextureOffset("_BackTex", new Vector2(xDir, yDir));
            skybox.SetTextureOffset("_LeftTex", new Vector2(xDir, yDir));
            skybox.SetTextureOffset("_RightTex", new Vector2(xDir, yDir));
            skybox.SetTextureOffset("_UpTex", new Vector2(xDir, yDir));
            skybox.SetTextureOffset("_DownTex", new Vector2(xDir, yDir));
        }*/

        //this.unscaledTime += Time.unscaledTime;
        this.unscaledTime += Time.unscaledDeltaTime;

        if (this.skybox != null)
        {
            //this.skybox.SetFloat("_UnscaledTime", Time.unscaledTime);
            this.skybox.SetFloat("_UnscaledTime", this.unscaledTime);
        }
    }
}
