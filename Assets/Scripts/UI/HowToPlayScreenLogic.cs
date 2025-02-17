using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HowToPlayScreenLogic : MonoBehaviour
{
    public GameObject keyboardButtons;
    public GameObject controllerButtons;

    public GameObject[] keyboardGameObjects;
    public GameObject[] controllerGameObjects;

    public bool goingToMenu;

    /*public Slider p1ChargeSlider;
    public Image p1ChargeFill;

    public Color fullChargeColor;
    public Color lowChargeColor;
    public Color halfChargeColor;*/

    public GameObject backingObject;
    public Slider backingSlider;
    public CharacterSelectInput characterSelectInput;

    private float backingProgress;

    private float dissapearValue;

    // Start is called before the first frame update
    void Start()
    {
        this.dissapearValue = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        this.BackingUpdateLogic();
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("keyboard");
        }*/

        //isKeyboardAndMouse = lastDevice.description.deviceClass.Equals("Keyboard")


        /*if (this.p1ChargeFill != null && this.p1ChargeSlider)
        {
            //this.p1ChargeFill.color = Color.Lerp(this.lowChargeColor, this.fullChargeColor, this.player1.superCharge / this.player1.maxSuperCharge);
            if (this.p1ChargeSlider.value >= 100f)
                this.p1ChargeFill.color = this.fullChargeColor;
            else if (this.p1ChargeSlider.value >= 50f)
                this.p1ChargeFill.color = this.halfChargeColor;
            else
                this.p1ChargeFill.color = this.lowChargeColor;
        }*/
    }

    public void SwitchToKeyboard()
    {
        /*if(this.keyboardButtons != null && this.controllerButtons != null)
        {
            this.keyboardButtons.SetActive(true);
            this.controllerButtons.SetActive(false);
        }*/

        foreach (GameObject keyboardObject in this.keyboardGameObjects)
        {
            if (keyboardObject != null)
                keyboardObject.SetActive(true);
        }

        foreach (GameObject controllerObject in this.controllerGameObjects)
        {
            if (controllerObject != null)
                controllerObject.SetActive(false);
        }
    }

    public void SwitchToController()
    {
        /*if (this.keyboardButtons != null && this.controllerButtons != null)
        {
            this.keyboardButtons.SetActive(false);
            this.controllerButtons.SetActive(true);
        }*/

        foreach (GameObject keyboardObject in this.keyboardGameObjects)
        {
            if (keyboardObject != null)
                keyboardObject.SetActive(false);
        }

        foreach (GameObject controllerObject in this.controllerGameObjects)
        {
            if (controllerObject != null)
                controllerObject.SetActive(true);
        }
    }

    public void GoBackToMenu()
    {
        
        if (!this.goingToMenu)
        {
            this.goingToMenu = true;
            Debug.Log("test");
            SceneManager.LoadSceneAsync("Menu");
        }
        
    }


    public void BackingUpdateLogic()
    {
        if (/*this.backingObject != null && this.backingSlider != null && */!this.goingToMenu)
        {

            if (this.characterSelectInput.backing)
            {
                if (this.backingProgress < 1f)
                    this.backingProgress += 3f * Time.deltaTime;
            }
            else
            {
                if (this.backingProgress > 0f)
                    this.backingProgress -= 4f * Time.deltaTime;
                else
                    this.backingProgress = 0f;
            }
        }


        if (this.backingProgress <= 0f)
        {
            if (this.dissapearValue < 1f)
                this.dissapearValue += 5f * Time.deltaTime;
            else
                this.dissapearValue = 1f;
        }
        else
        {
            this.dissapearValue = 0f;
        }

        if (this.backingObject != null && this.backingSlider != null)
        {
            

            if (this.dissapearValue >= 1f)
            {
                this.backingObject.SetActive(false);
            }
            else
            {
                this.backingObject.SetActive(true);
            }

            //this.backingObject.SetActive(this.backingProgress > 0f);

            this.backingSlider.value = this.backingProgress;
        }

        if (this.backingProgress >= 1f)
        {
            this.GoBackToMenu();
        }
    }
}
