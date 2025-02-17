using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlaySuperMeter : MonoBehaviour
{

    public Slider chargeSlider;
    public Image chargeFill;

    public Color fullChargeColor;
    public Color lowChargeColor;
    public Color halfChargeColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.chargeFill != null && this.chargeSlider)
        {
            //this.p1ChargeFill.color = Color.Lerp(this.lowChargeColor, this.fullChargeColor, this.player1.superCharge / this.player1.maxSuperCharge);
            if (this.chargeSlider.value >= 100f)
                this.chargeFill.color = this.fullChargeColor;
            else if (this.chargeSlider.value >= 50f)
                this.chargeFill.color = this.halfChargeColor;
            else
                this.chargeFill.color = this.lowChargeColor;
        }
    }
}
