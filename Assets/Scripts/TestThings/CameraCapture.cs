using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CameraCapture : MonoBehaviour
{
    /*public RenderTexture rt;
    // Use this for initialization

    [ContextMenu("SaveTexture")]
    public void SaveTexture()
    {
        byte[] bytes = toTexture2D(rt).EncodeToPNG();
        //System.IO.File.WriteAllBytes("C:/Users/August/Documents/Unity/clonk/Assets/SkinIcons", bytes);
        File.WriteAllBytes(Application.dataPath + "/SkinIcons/" + "test" + ".png", bytes);
    }
    
    
    Texture2D toTexture2D(RenderTexture rTex)
    {
        //Texture2D tex = new Texture2D(1024, 1024, TextureFormat.RGB24, false);
        Texture2D tex = new Texture2D(1024, 1024);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        Destroy(tex);//prevents memory leak
        return tex;
    }*/

    public int fileCounter;
    public string fileName = "skin";
    //public KeyCode screenshotKey;
    public Camera camera;
    /*private Camera Camera
    {
        get
        {
            if (!_camera)
            {
                _camera = Camera.main;
            }
            return _camera;
        }
    }
    private Camera _camera;*/

    /*private void LateUpdate()
    {
        if (Input.GetKeyDown(screenshotKey))
        {
            Capture();
        }
    }*/
    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    [ContextMenu("Capture")]
    public void Capture()
    {
        RenderTexture activeRenderTexture = RenderTexture.active;
        RenderTexture.active = camera.targetTexture;
        camera.backgroundColor = new Color(0, 0, 0, 0);
        camera.Render();

        Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height, TextureFormat.ARGB32, false);
        image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        image.Apply();
        RenderTexture.active = activeRenderTexture;

        byte[] bytes = image.EncodeToPNG();
        Destroy(image);

        //File.WriteAllBytes(Application.dataPath + "/Icons/Test/" + fileName + ".png", bytes);

        File.WriteAllBytes(Application.dataPath + "/Icons/Test/" + "Capture_" + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss") + ".png", bytes);
        fileCounter++;
    }


}
