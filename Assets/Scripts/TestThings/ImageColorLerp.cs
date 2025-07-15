using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColorLerp : MonoBehaviour
{
    public Image[] images;
    public Color[] colors;
    public Color color;

    private int currentColorIndex = 0;
    private int targetColorIndex = 1;
    private float targetPoint;

    //public float time;
    public float speed;

    void Update()
    {
        if (this.colors.Length >= 2)
            this.ChangeColor();
    }

    public void ChangeColor()
    {
        //this.targetPoint += Time.unscaledDeltaTime / this.time;
        this.targetPoint += Time.unscaledDeltaTime * this.speed;
        this.color = Color.Lerp(this.colors[this.currentColorIndex], this.colors[this.targetColorIndex], this.targetPoint);

        if (this.targetPoint >= 1f)
        {
            this.targetPoint = 0f;
            this.currentColorIndex = this.targetColorIndex;
            this.targetColorIndex++;
            if (this.targetColorIndex == this.colors.Length)
                this.targetColorIndex = 0; 
        }

        foreach (Image image in this.images)
        {
            if (image != null)
                image.color = this.color;
        }
    }
}
