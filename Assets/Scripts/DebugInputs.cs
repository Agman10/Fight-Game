using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DebugInputs : MonoBehaviour
{
    public RandomSkybox randomSkybox;
    public bool switchingSkyboxInput;
    public TestPlayer player1;
    public TestPlayer player2;

    public Action<bool> SwitchSkyboxInput;
    [HideInInspector] public bool pastSwitchSkyboxInput;
    public bool switchingStageInput;
    public Action<bool> SwitchStageInput;
    [HideInInspector] public bool pastSwitchStageInput;

    public Action<bool> ReloadSceneInput;
    [HideInInspector] public bool pastReloadSceneInput;
    public bool reloadingSceneInput;

    public bool loadingPreviousStageInput;
    public Action<bool> LoadPreviousStageInput;
    [HideInInspector] public bool pastLoadPreviousStageInput;


    public int sceneIndex = 1;

    private void OnEnable()
    {
        this.SwitchSkyboxInput += this.SwitchSkybox;
        this.SwitchStageInput += this.SwitchStage;
        this.ReloadSceneInput += this.ReloadScene;

        this.LoadPreviousStageInput += this.LoadPreviousStage;
    }
    private void OnDisable()
    {
        this.SwitchSkyboxInput -= this.SwitchSkybox;
        this.SwitchStageInput -= this.SwitchStage;
        this.ReloadSceneInput -= this.ReloadScene;

        this.LoadPreviousStageInput -= this.LoadPreviousStage;
    }

    public void Start()
    {
        if (GameManager.Instance != null)
        {
            this.player1 = GameManager.Instance.player1;
            this.player2 = GameManager.Instance.player2;
        }
    }

    public void OnLoadNextSkyboxInput(InputAction.CallbackContext ctx)
    {

        bool boolean = ctx.ReadValueAsButton();
        //Debug.Log(boolean);
        this.switchingSkyboxInput = boolean;
        if (this.pastSwitchSkyboxInput != boolean)
        {
            this.SwitchSkyboxInput?.Invoke(boolean);
            /*if (this.randomSkybox != null)
                this.randomSkybox.LoadNextSkybox();*/
            this.pastSwitchSkyboxInput = boolean;
        }
    }

    public void OnLoadNextStageInput(InputAction.CallbackContext ctx)
    {

        bool boolean = ctx.ReadValueAsButton();
        
        this.switchingStageInput = boolean;
        if (this.pastSwitchStageInput != boolean)
        {
            this.SwitchStageInput?.Invoke(boolean);
            /*Debug.Log(boolean);
            if (this.randomSkybox != null)
                this.randomSkybox.LoadNextStage();*/
            this.pastSwitchStageInput = boolean;
        }
        /*if (boolean && this.randomSkybox != null)
        {
            this.randomSkybox.LoadNextStage();
        }*/

    }

    public void SwitchSkybox(bool boolean)
    {
        if (boolean && this.randomSkybox != null)
            this.randomSkybox.LoadNextSkybox();
    }
    public void SwitchStage(bool boolean)
    {
        //Debug.Log(boolean);
        if (boolean && this.randomSkybox != null)
            this.randomSkybox.LoadNextStage();
    }

    public void LoadPreviousStage(bool boolean)
    {
        //Debug.Log(boolean);
        if (boolean && this.randomSkybox != null)
            this.randomSkybox.LoadNextStage(true);
    }

    public void OnReloadSceneInput(InputAction.CallbackContext ctx)
    {

        bool reload = ctx.ReadValueAsButton();
        this.reloadingSceneInput = reload;
        if (this.pastReloadSceneInput != reload)
        {
            this.ReloadSceneInput?.Invoke(reload);
            this.pastReloadSceneInput = reload;
        }

        //SceneManager.LoadScene(this.sceneIndex);

    }
    public void ReloadScene(bool reload)
    {
        if (reload)
            SceneManager.LoadScene(this.sceneIndex);
    }

    public void OnQuitInput(InputAction.CallbackContext ctx)
    {

        bool quit = ctx.ReadValueAsButton();
        SceneManager.LoadScene("Menu");
        //Application.Quit();

    }
    public void OnGiveP1FullSuper(InputAction.CallbackContext ctx)
    {

        bool giveFullSuper = ctx.ReadValueAsButton();
        if (giveFullSuper && this.player1 != null)
            this.player1.GiveSuperCharge(this.player1.maxSuperCharge);
        //Application.Quit();

    }

    public void OnGiveP2FullSuper(InputAction.CallbackContext ctx)
    {

        bool giveFullSuper = ctx.ReadValueAsButton();
        if (giveFullSuper && this.player2 != null)
            this.player2.GiveSuperCharge(this.player2.maxSuperCharge);
        //Application.Quit();

    }


    public void OnLoadPreviousStageInput(InputAction.CallbackContext ctx)
    {

        bool boolean = ctx.ReadValueAsButton();

        this.loadingPreviousStageInput = boolean;
        if (this.pastLoadPreviousStageInput != boolean)
        {
            this.LoadPreviousStageInput?.Invoke(boolean);
            /*Debug.Log(boolean);
            if (this.randomSkybox != null)
                this.randomSkybox.LoadNextStage();*/
            this.pastLoadPreviousStageInput = boolean;
        }
        /*if (boolean && this.randomSkybox != null)
        {
            this.randomSkybox.LoadNextStage();
        }*/

    }

}
