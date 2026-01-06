using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBars : MonoBehaviour
{
    public TestPlayer player1;
    public TestPlayer player2;

    public Slider p1HealthSlider;
    public Slider p2HealthSlider;

    public Image p1HealthFill;
    public Image p2HealthFill;

    public Color fullHealthColor;
    public Color noHealthColor;
    public Color highHealthColor;

    [Space]

    public Slider p1ChargeSlider;
    public Slider p2ChargeSlider;

    public Image p1ChargeFill;
    public Image p2ChargeFill;

    public Color fullChargeColor;
    public Color lowChargeColor;
    public Color halfChargeColor;

    [Space]
    public Text player1Name;
    public Text player2Name;

    public TextMeshProUGUI p1Name;
    public TextMeshProUGUI p2Name;

    [Space]
    public Text p1HealthNumbers;
    public Text p2HealthNumbers;

    public TextMeshProUGUI p1HealthNumber;
    public TextMeshProUGUI p2HealthNumber;
    public TextMeshProUGUI p1SuperNumber;
    public TextMeshProUGUI p2SuperNumber;

    [Space]
    public GameObject p1HalfSuperReady;
    public GameObject p1FullSuperReady;

    public GameObject p2HalfSuperReady;
    public GameObject p2FullSuperReady;


    // Start is called before the first frame update
    void Start()
    {
        /*if (this.player1 != null && this.p1HealthSlider != null)
        {
            this.p1HealthSlider.maxValue = this.player1.maxHealth;
            this.p1HealthSlider.value = this.player1.health;
        }

        if (this.player2 != null && this.p2HealthSlider != null)
        {
            this.p2HealthSlider.maxValue = this.player2.maxHealth;
            this.p2HealthSlider.value = this.player2.health;
        }*/

        this.SetHealth();
        this.SetCharge();

        this.SetNames();
    }

    // Update is called once per frame
    void Update()
    {
        this.SetHealth();
        this.SetCharge();
    }

    private void SetHealth()
    {
        if(this.player1 != null && this.p1HealthSlider != null)
        {
            this.p1HealthSlider.maxValue = this.player1.maxHealth;
            this.p1HealthSlider.value = this.player1.health;

            if(this.p1HealthFill != null)
            {
                //this.p1HealthFill.color = Color.Lerp(this.noHealthColor, this.fullHealthColor, this.player1.health / this.player1.maxHealth);
                if (this.player1.health >= this.player1.maxHealth)
                    this.p1HealthFill.color = this.fullHealthColor;
                else
                    this.p1HealthFill.color = Color.Lerp(this.noHealthColor, this.highHealthColor, this.player1.health / this.player1.maxHealth);
            }

            /*if (this.p1HealthNumbers != null)
                this.p1HealthNumbers.text = this.player1.health.ToString();*/

            if (this.p1HealthNumber != null)
                this.p1HealthNumber.text = this.player1.health.ToString();
        }

        if (this.player2 != null && this.p2HealthSlider != null)
        {
            this.p2HealthSlider.maxValue = this.player2.maxHealth;
            this.p2HealthSlider.value = this.player2.health;

            if (this.p2HealthFill != null)
            {
                //this.p2HealthFill.color = Color.Lerp(this.noHealthColor, this.fullHealthColor, this.player2.health / this.player2.maxHealth);

                if (this.player2.health >= this.player2.maxHealth)
                    this.p2HealthFill.color = this.fullHealthColor;
                else
                    this.p2HealthFill.color = Color.Lerp(this.noHealthColor, this.highHealthColor, this.player2.health / this.player2.maxHealth);
            }

            /*if (this.p2HealthNumbers != null)
                this.p2HealthNumbers.text = this.player2.health.ToString();*/

            if (this.p2HealthNumber != null)
                this.p2HealthNumber.text = this.player2.health.ToString();
        }
    }

    private void SetCharge()
    {
        if (this.player1 != null && this.p1ChargeSlider != null)
        {
            this.p1ChargeSlider.maxValue = this.player1.maxSuperCharge;
            this.p1ChargeSlider.value = this.player1.superCharge;

            if (this.p1ChargeFill != null)
            {
                //this.p1ChargeFill.color = Color.Lerp(this.lowChargeColor, this.fullChargeColor, this.player1.superCharge / this.player1.maxSuperCharge);
                if (this.player1.superCharge >= this.player1.maxSuperCharge)
                    this.p1ChargeFill.color = this.fullChargeColor;
                else if (this.player1.superCharge >= this.player1.maxSuperCharge / 2)
                    this.p1ChargeFill.color = this.halfChargeColor;
                else
                    this.p1ChargeFill.color = this.lowChargeColor;


                if (this.p1HalfSuperReady != null)
                    this.p1HalfSuperReady.SetActive(this.player1.superCharge >= this.player1.maxSuperCharge / 2 && this.player1.superCharge < this.player1.maxSuperCharge);

                if (this.p1FullSuperReady != null)
                    this.p1FullSuperReady.SetActive(this.player1.superCharge >= this.player1.maxSuperCharge);
            }

            if (this.p1SuperNumber != null)
                this.p1SuperNumber.text = this.player1.superCharge.ToString();
        }

        if (this.player2 != null && this.p2ChargeSlider != null)
        {
            this.p2ChargeSlider.maxValue = this.player2.maxSuperCharge;
            this.p2ChargeSlider.value = this.player2.superCharge;

            if (this.p2ChargeFill != null)
            {
                //this.p2ChargeFill.color = Color.Lerp(this.lowChargeColor, this.fullChargeColor, this.player2.superCharge / this.player2.maxSuperCharge);
                if (this.player2.superCharge >= this.player2.maxSuperCharge)
                    this.p2ChargeFill.color = this.fullChargeColor;
                else if (this.player2.superCharge >= this.player2.maxSuperCharge / 2)
                    this.p2ChargeFill.color = this.halfChargeColor;
                else
                    this.p2ChargeFill.color = this.lowChargeColor;


                if (this.p2HalfSuperReady != null)
                    this.p2HalfSuperReady.SetActive(this.player2.superCharge >= this.player2.maxSuperCharge / 2 && this.player2.superCharge < this.player2.maxSuperCharge);

                if (this.p2FullSuperReady != null)
                    this.p2FullSuperReady.SetActive(this.player2.superCharge >= this.player2.maxSuperCharge);
            }

            if (this.p2SuperNumber != null)
                this.p2SuperNumber.text = this.player2.superCharge.ToString();
        }
    }

    public void SetNames()
    {
        if (this.player1 && this.player1Name != null)
            this.player1Name.text = this.player1.characterName.ToString();

        if (this.player2 && this.player2Name != null)
            this.player2Name.text = this.player2.characterName.ToString();



        if (this.player1 && this.p1Name != null)
            this.p1Name.text = this.player1.characterName.ToString();

        if (this.player2 && this.p2Name != null)
            this.p2Name.text = this.player2.characterName.ToString();
    }
}
