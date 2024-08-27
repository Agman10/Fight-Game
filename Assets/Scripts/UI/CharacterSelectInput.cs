using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSelectInput : MonoBehaviour
{
    public Vector3 moveInput;
    public Action<Vector3> MoveInput;
    [HideInInspector] public Vector3 pastMoveInput;

    public bool selecting;
    public Action<bool> SelectInput;
    [HideInInspector] public bool pastSelectInput;
    public bool backing;
    public Action<bool> BackInput;
    [HideInInspector] public bool pastBackInput;

    public bool quitting;
    public Action<bool> QuitInput;
    [HideInInspector] public bool pastQuitInput;


    public bool nextMusicType;
    public Action<bool> NextMusicTypeInput;
    [HideInInspector] public bool pastNextMusicTypeInput;

    public bool previousMusicType;
    public Action<bool> PreviousMusicTypeInput;
    [HideInInspector] public bool pastPreviousMusicTypeInput;

    public void OnMoveInput(InputAction.CallbackContext ctx)
    {
        Vector2 inputValue = ctx.ReadValue<Vector2>();

        this.moveInput = new Vector3(inputValue.x, inputValue.y, 0);

        if(this.pastMoveInput != new Vector3(inputValue.x, inputValue.y, 0))
        {
            this.MoveInput?.Invoke(new Vector3(inputValue.x, inputValue.y, 0));
            this.pastMoveInput = new Vector3(inputValue.x, inputValue.y, 0);
        }
    }

    public void OnSelectInput(InputAction.CallbackContext ctx)
    {

        bool select = ctx.ReadValueAsButton();
        this.selecting = select;
        if (this.pastSelectInput != select)
        {
            this.SelectInput?.Invoke(select);
            this.pastSelectInput = select;
        }
    }

    public void OnBackInput(InputAction.CallbackContext ctx)
    {

        bool back = ctx.ReadValueAsButton();
        this.backing = back;
        if (this.pastBackInput != back)
        {
            this.BackInput?.Invoke(back);
            this.pastBackInput = back;
        }
    }

    public void OnQuitInput(InputAction.CallbackContext ctx)
    {

        bool quit = ctx.ReadValueAsButton();
        this.quitting = quit;
        if (this.pastQuitInput != quit)
        {
            this.QuitInput?.Invoke(quit);
            this.pastQuitInput = quit;
        }
    }


    public void OnNextMusicTypeInput(InputAction.CallbackContext ctx)
    {

        bool next = ctx.ReadValueAsButton();
        this.nextMusicType = next;
        if (this.pastNextMusicTypeInput != next)
        {
            this.NextMusicTypeInput?.Invoke(next);
            this.pastNextMusicTypeInput = next;
        }
    }

    public void OnPreviousMusicTypeInput(InputAction.CallbackContext ctx)
    {

        bool previous = ctx.ReadValueAsButton();
        this.previousMusicType = previous;
        if (this.pastPreviousMusicTypeInput != previous)
        {
            this.PreviousMusicTypeInput?.Invoke(previous);
            this.pastPreviousMusicTypeInput = previous;
        }
    }
}
