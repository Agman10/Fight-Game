using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MunuSkinChange : MonoBehaviour
{
    public SO_Skin currentCapSkin;
    public SO_Skin currentDarkCapSkin;

    public SO_Skin[] capSkins;
    public SO_Skin[] darkCapSkins;

    public int currentCapSkinIndex = 0;
    public int currentDarkCapSkinIndex = 0;
    public CharacterSkinTest cap;
    public CharacterSkinTest darkCap;

    [Space]

    public bool switchingCapSkinInput;
    public Action<bool> SwitchCapSkinInput;
    [HideInInspector] public bool pastSwitchCapSkinInput;
    public bool switchingDarkCapSkinInput;
    public Action<bool> SwitchDarkCapSkinInput;
    [HideInInspector] public bool pastSwitchDarkCapSkinInput;
    // Start is called before the first frame update

    private void OnEnable()
    {
        this.SwitchCapSkinInput += this.SwitchCapSkin;
        this.SwitchDarkCapSkinInput += this.SwitchDarkCapSkin;
    }
    private void OnDisable()
    {
        this.SwitchCapSkinInput -= this.SwitchCapSkin;
        this.SwitchDarkCapSkinInput -= this.SwitchDarkCapSkin;
    }
    void Start()
    {
        if (this.capSkins.Length > 0 && this.capSkins[0] != null && this.currentCapSkin != null)
        {
            this.currentCapSkin.colors = this.capSkins[0].colors;
        }

        if (this.darkCapSkins.Length > 0 && this.darkCapSkins[0] != null && this.currentDarkCapSkin != null)
        {
            this.currentDarkCapSkin.colors = this.darkCapSkins[0].colors;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [ContextMenu("NextCapSkin")]
    public void NextCapSkin()
    {
        if(this.capSkins.Length > 0 && this.currentCapSkin != null)
        {
            if (this.currentCapSkinIndex >= this.capSkins.Length - 1 && this.capSkins[0] != null)
            {
                this.currentCapSkin.colors = this.capSkins[0].colors;
                if (this.cap != null)
                    this.cap.ChangeSkin(this.capSkins[0]);
                this.currentCapSkinIndex = 0;
            }
            else
            {
                if (this.capSkins[this.currentCapSkinIndex + 1] != null)
                {
                    this.currentCapSkin.colors = this.capSkins[this.currentCapSkinIndex + 1].colors;
                    if (this.cap != null)
                        this.cap.ChangeSkin(this.capSkins[this.currentCapSkinIndex + 1]);
                    this.currentCapSkinIndex = this.currentCapSkinIndex + 1;
                }
                    
            }
        }
    }

    [ContextMenu("NextDarkCapSkin")]
    public void NextDarkCapSkin()
    {
        if (this.darkCapSkins.Length > 0 && this.currentDarkCapSkin != null)
        {
            if (this.currentDarkCapSkinIndex >= this.darkCapSkins.Length - 1 && this.darkCapSkins[0] != null)
            {
                this.currentDarkCapSkin.colors = this.darkCapSkins[0].colors;
                if (this.darkCap != null)
                    this.darkCap.ChangeSkin(this.darkCapSkins[0]);
                this.currentDarkCapSkinIndex = 0;
            }
            else
            {
                if (this.darkCapSkins[this.currentDarkCapSkinIndex + 1] != null)
                {
                    this.currentDarkCapSkin.colors = this.darkCapSkins[this.currentDarkCapSkinIndex + 1].colors;
                    if (this.darkCap != null)
                        this.darkCap.ChangeSkin(this.darkCapSkins[this.currentDarkCapSkinIndex + 1]);
                    this.currentDarkCapSkinIndex = this.currentDarkCapSkinIndex + 1;
                }

            }
        }
    }

    public void OnNextCapSkin(InputAction.CallbackContext ctx)
    {

        bool boolean = ctx.ReadValueAsButton();
        //Debug.Log(boolean);
        this.switchingCapSkinInput = boolean;
        if (this.pastSwitchCapSkinInput != boolean)
        {
            this.SwitchCapSkinInput?.Invoke(boolean);
            /*if (this.randomSkybox != null)
                this.randomSkybox.LoadNextSkybox();*/
            this.pastSwitchCapSkinInput = boolean;
        }
    }

    public void OnNextDarkCapSkin(InputAction.CallbackContext ctx)
    {

        bool boolean = ctx.ReadValueAsButton();

        this.switchingDarkCapSkinInput = boolean;
        if (this.pastSwitchDarkCapSkinInput != boolean)
        {
            this.SwitchDarkCapSkinInput?.Invoke(boolean);
            /*Debug.Log(boolean);
            if (this.randomSkybox != null)
                this.randomSkybox.LoadNextStage();*/
            this.pastSwitchDarkCapSkinInput = boolean;
        }
        /*if (boolean && this.randomSkybox != null)
        {
            this.randomSkybox.LoadNextStage();
        }*/

    }

    public void SwitchCapSkin(bool boolean)
    {
        if (boolean)
            this.NextCapSkin();
    }
    public void SwitchDarkCapSkin(bool boolean)
    {
        //Debug.Log(boolean);
        if (boolean)
            this.NextDarkCapSkin();
    }
}
