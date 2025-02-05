using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    public Vector3 moveInput;

    //public InputAction inputAction;
    public bool jumping;
    public Action<bool> JumpInput;
    [HideInInspector] public bool pastJumpInput;

    public bool punching;
    public Action<bool> PunchInput;
    [HideInInspector] public bool pastPunchInput;

    public bool kicking;
    public Action<bool> KickInput;
    [HideInInspector] public bool pastKickInput;

    public bool special;
    public Action<bool> SpecialInput;
    [HideInInspector] public bool pastSpecialInput;

    public bool special2;
    public Action<bool> Special2Input;
    [HideInInspector] public bool pastSpecial2Input;

    public bool super;
    public Action<bool> SuperInput;
    [HideInInspector] public bool pastSuperInput;

    public bool taunting;
    public Action<bool> TauntInput;
    [HideInInspector] public bool pastTauntInput;

    public bool suicide;
    public Action<bool> SuicideInput;
    [HideInInspector] public bool pastSuicideInput;

    public bool start;
    public Action<bool> StartInput;
    [HideInInspector] public bool pastStartInput;

    /*private void Awake()
    {
        this.JumpInput += this.Jump;
    }*/
    private void OnEnable()
    {
        this.JumpInput += this.Jump;

        this.StartInput += this.PauseIfAiVsAi;
    }
    private void OnDisable()
    {
        this.JumpInput -= this.Jump;

        this.StartInput -= this.PauseIfAiVsAi;
    }

    /*private void Update()
    {
        this.JumpInput += this.Jump;
    }*/

    public void OnMoveInput(InputAction.CallbackContext ctx)
    {
        Vector2 inputValue = ctx.ReadValue<Vector2>();

        this.moveInput = new Vector3(inputValue.x, inputValue.y, 0);
    }
    public void OnJumpInput(InputAction.CallbackContext ctx)
    {

        bool jump = ctx.ReadValueAsButton();
        this.jumping = jump;
        if(this.pastJumpInput != jump)
        {
            this.JumpInput?.Invoke(jump);
            this.pastJumpInput = jump;
        }
        
    }

    public void OnPunchInput(InputAction.CallbackContext ctx)
    {

        bool punch = ctx.ReadValueAsButton();
        this.punching = punch;
        if (this.pastPunchInput != punch)
        {
            this.PunchInput?.Invoke(punch);
            this.pastPunchInput = punch;
        }

    }

    public void OnKickInput(InputAction.CallbackContext ctx)
    {

        bool kick = ctx.ReadValueAsButton();
        this.kicking = kick;
        if (this.pastKickInput != kick)
        {
            this.KickInput?.Invoke(kick);
            this.pastKickInput = kick;
        }

    }

    public void OnSpecialInput(InputAction.CallbackContext ctx)
    {

        bool special = ctx.ReadValueAsButton();
        this.special = special;
        if (this.pastSpecialInput != special)
        {
            this.SpecialInput?.Invoke(special);
            this.pastSpecialInput = special;
        }

    }
    public void OnSpecial2Input(InputAction.CallbackContext ctx)
    {

        bool special2 = ctx.ReadValueAsButton();
        this.special2 = special2;
        if (this.pastSpecial2Input != special2)
        {
            this.Special2Input?.Invoke(special2);
            this.pastSpecial2Input = special2;
        }

    }

    public void OnSuperInput(InputAction.CallbackContext ctx)
    {

        bool super = ctx.ReadValueAsButton();
        this.super = super;
        if (this.pastSuperInput != super)
        {
            this.SuperInput?.Invoke(super);
            this.pastSuperInput = super;
        }

    }

    public void OnTauntInput(InputAction.CallbackContext ctx)
    {

        bool taunt = ctx.ReadValueAsButton();
        this.taunting = taunt;
        if (this.pastTauntInput != taunt)
        {
            this.TauntInput?.Invoke(taunt);
            this.pastTauntInput = taunt;
        }

    }


    public void OnSuicideInput(InputAction.CallbackContext ctx)
    {

        /*bool kick = ctx.ReadValueAsButton();
        this.SuicideInput?.Invoke(kick);*/

        /*this.kicking = kick;
        if (this.pastKickInput != kick)
        {
            this.KickInput?.Invoke(kick);
            this.pastKickInput = kick;
        }*/

        bool suiciding = ctx.ReadValueAsButton();
        this.suicide = suiciding;
        if (this.pastSuicideInput != suiciding)
        {
            this.SuicideInput?.Invoke(suiciding);
            this.pastSuicideInput = suiciding;
        }

    }

    public void OnStartInput(InputAction.CallbackContext ctx)
    {

        bool pressingStart = ctx.ReadValueAsButton();
        this.start = pressingStart;
        if (this.pastStartInput != pressingStart)
        {
            this.StartInput?.Invoke(pressingStart);
            this.pastStartInput = pressingStart;
        }

    }

    public void Jump(bool test)
    {
        //Debug.Log(test);
    }


    public void OnReloadSceneInput(InputAction.CallbackContext ctx)
    {

        bool reload = ctx.ReadValueAsButton();
        SceneManager.LoadScene("TestStage");

    }

    public void OnQuitInput(InputAction.CallbackContext ctx)
    {

        bool quit = ctx.ReadValueAsButton();
        SceneManager.LoadScene("Menu");
        //Application.Quit();

    }

    public void PauseIfAiVsAi(bool pause)
    {
        if (pause)
        {
            if (GameManager.Instance != null && CharacterManager.Instance != null && CharacterManager.Instance.vsAi && CharacterManager.Instance.vsAiId == 1)
            {
                //GameManager.Instance.TogglePauseGame(true);
                if (!GameManager.Instance.gameIsPaused)
                    GameManager.Instance.PauseGame();
                else
                    GameManager.Instance.UnPauseGame();

                //Debug.Log("paused in ai vs ai");
                //Debug.Log("pauseeplay");
                //this.playerInput.StartInput += this.PauseGame;
            }
        }
    }
}
