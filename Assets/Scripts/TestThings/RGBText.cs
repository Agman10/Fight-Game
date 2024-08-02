using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RGBText : MonoBehaviour
{
    public TextMeshProUGUI text;


    public float speed = 0.1f;

    
    public float saturation = 0.5f;
    public float brightness = 1f;

    [Space]
    public float hue;
    public float hue2;

    public float startHue;
    public float startHue2;
    
    // Start is called before the first frame update
    private void OnEnable()
    {
        this.hue = this.startHue;
        this.hue2 = this.startHue2;
    }

    // Update is called once per frame
    void Update()
    {
        //this.hue += this.speed * Time.deltaTime;
        this.hue += this.speed * Time.unscaledDeltaTime;

        /*if (this.hue >= 1f)
            this.hue = 0f;
        else if (this.hue < 0f)
            this.hue = 1f;*/

        if (this.hue >= 1f)
            this.hue -= 1f;
        else if (this.hue < 0f)
            this.hue += 1f;

        this.hue2 += this.speed * Time.unscaledDeltaTime;

        /*if (this.hue2 >= 1f)
            this.hue2 = 0f;
        else if (this.hue2 < 0f)
            this.hue2 = 1f;*/

        if (this.hue2 >= 1f)
            this.hue2 -= 1f;
        else if (this.hue2 < 0f)
            this.hue2 += 1f;


        //this.text.color = Color.HSVToRGB(this.hue, this.saturation, this.brightness);

        Color color1 = Color.HSVToRGB(this.hue, this.saturation, this.brightness);
        Color color2 = Color.HSVToRGB(this.hue2, this.saturation, this.brightness);

        this.text.colorGradient = new VertexGradient(
            color1,
            color1,
            color2,
            color2
            );
    }
}
