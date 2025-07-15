using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FightText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Color[] colors;
    public Color color;
    public Color color2;

    private int currentColorIndex = 0;
    private int targetColorIndex = 1;
    private float targetPoint;
    //public float time;
    public float speed = 1f;

    private int currentColorIndex2 = 1;
    private int targetColorIndex2 = 2;

    public int color2IndexAdvantage = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (this.color2IndexAdvantage > this.colors.Length - 1)
            this.currentColorIndex2 = 1;
        else
            this.currentColorIndex2 = this.color2IndexAdvantage;

        if (this.colors.Length < 3)
            this.targetColorIndex2 = 0;
        else
            this.targetColorIndex2 = this.currentColorIndex2 + 1;
    }

    private void OnDisable()
    {
        this.currentColorIndex = 0;
        this.targetColorIndex = 1;

        this.currentColorIndex2 = 1;
        if (this.colors.Length < 3)
            this.targetColorIndex2 = 0;
        else
            this.targetColorIndex2 = 2;

        this.targetPoint = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        this.ChangeColor();
    }

    public void ChangeColor()
    {
        //this.targetPoint += Time.unscaledDeltaTime / this.time;
        this.targetPoint += Time.unscaledDeltaTime * this.speed;
        this.color = Color.Lerp(this.colors[this.currentColorIndex], this.colors[this.targetColorIndex], this.targetPoint);
        this.color2 = Color.Lerp(this.colors[this.currentColorIndex2], this.colors[this.targetColorIndex2], this.targetPoint);
        /*if (this.text != null)
            this.text.color = this.color;*/
        //material.color = Color.Lerp(colors[currentColorIndex], colors[targetColorIndex], targetPoint);
        if (this.targetPoint >= 1f)
        {
            this.targetPoint = 0f;
            this.currentColorIndex = this.targetColorIndex;
            this.targetColorIndex++;
            if (this.targetColorIndex == this.colors.Length)
                this.targetColorIndex = 0;


            this.currentColorIndex2 = this.targetColorIndex2;
            this.targetColorIndex2++;
            if (this.targetColorIndex2 == this.colors.Length)
                this.targetColorIndex2 = 0;
        }

        if(this.text != null)
        {
            this.text.colorGradient = new VertexGradient(
            this.color,
            this.color,
            this.color2,
            this.color2
            );
        }
    }
}
