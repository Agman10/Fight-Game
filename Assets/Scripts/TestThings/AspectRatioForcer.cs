using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioForcer : MonoBehaviour
{
    // Use this for initialization
    public bool forceOnEnable;

    //private float cameraBaseFov;

    /*private void Awake()
    {
        Camera camera = GetComponent<Camera>();
        this.cameraBaseFov = camera.fieldOfView;

        Debug.Log(this.cameraBaseFov);
    }*/
    void Start()
    {
        //this.FixAspect();
        //this.FovFixer();
    }

    private void OnEnable()
    {
        if (this.forceOnEnable)
            this.FovFixer();
    }


    public void FovFixer()
    {
        float targetaspect = 16.0f / 9.0f;

        float windowaspect = (float)Screen.width / (float)Screen.height;

        float scaleheight = windowaspect / targetaspect;
        //float scaleheight = targetaspect / windowaspect;

        Camera camera = GetComponent<Camera>();
        float baseFov = camera.fieldOfView;
        //float baseFov = this.cameraBaseFov;

        //Debug.Log(scaleheight);

        if(camera != null && scaleheight < 1f)
        {
            camera.fieldOfView = baseFov * ((scaleheight * 0.1f) + 1f);
            //camera.fieldOfView = baseFov * ((1 - scaleheight) + 1f);

            //Debug.Log((scaleheight * 0.1f) + 1f);
            //Debug.Log((1 - scaleheight) + 1f);
        }
    }









    public void FixAspect()
    {
        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        float targetaspect = 16.0f / 9.0f;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera camera = GetComponent<Camera>();

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}
