using Febucci.UI.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PreFightDialogue : MonoBehaviour
{
    public SO_PreFightDialogue preFightDialogue;

    public bool inDialogue;

    public int currentDialogueIndex;

    public int p1Id;
    public int p2Id;
    public SO_Skin p1Skin;
    public SO_Skin p2Skin;
    public CharacterIconManager p1Icon;
    public CharacterIconManager p2Icon;

    public bool P1InProgress;
    public bool P2InProgress;

    public TextMeshProUGUI p1Text;
    public TypewriterCore p1Typewriter;
    //public TAnimCore p1TextAnimator;
    //private string currentP1Text = "";

    public TextMeshProUGUI p2Text;
    public TypewriterCore p2Typewriter;
    //private string currentP2Text = "";

    public Transform p1Transform;
    public Transform p2Transform;

    //these are temporary for testing will use another way to detect player input
    public PlayerInput playerInput;
    public bool skipButtonDown;

    public AudioSource explosionSfx;

    private void OnEnable()
    {
        this.StartDialogue();

        if (this.playerInput != null)
        {
            this.playerInput.PunchInput += this.Skip;
        }

        if (this.p1Icon != null)
        {
            this.p1Icon.InstantiateIconPrefab(this.p1Id, this.p1Skin);
        }

        if (this.p2Icon != null)
        {
            this.p2Icon.InstantiateIconPrefab(this.p2Id, this.p2Skin);
        }

        if (this.p1Typewriter != null)
            this.p1Typewriter.onMessage.AddListener(OnTypewriterMessage);

        if (this.p2Typewriter != null)
            this.p2Typewriter.onMessage.AddListener(OnTypewriterMessage);
    }
    private void OnDisable()
    {
        if (this.playerInput != null)
        {
            this.playerInput.PunchInput -= this.Skip;
        }

        if (this.p1Typewriter != null)
            this.p1Typewriter.onMessage.RemoveListener(this.OnTypewriterMessage);

        if (this.p2Typewriter != null)
            this.p2Typewriter.onMessage.RemoveListener(this.OnTypewriterMessage);
    }

    public void Skip(bool skipping)
    {
        if (skipping && this.inDialogue && Time.timeScale > 0)
        {
            this.skipButtonDown = true;
        }
    }

    public void StartDialogue()
    {
        if (this.preFightDialogue != null)
        {
            if (this.p1Text != null && this.p2Text != null)
            {
                if (this.preFightDialogue.dialogueLines.Length > 0)
                    this.StartCoroutine(this.DialogueCoroutine());
            }
        }
        else
        {
            if (this.p1Transform != null)
                this.p1Transform.gameObject.SetActive(false);

            if (this.p2Transform != null)
                this.p2Transform.gameObject.SetActive(false);
        }
    }

    private IEnumerator DialogueCoroutine()
    {
        if (this.p1Transform != null)
            this.p1Transform.gameObject.SetActive(true);

        if (this.p2Transform != null)
            this.p2Transform.gameObject.SetActive(true);

        this.ClearAllText();

        if (this.p1Transform != null && this.p2Transform != null)
        {
            this.p1Transform.localPosition = new Vector3(-700f, 0f, 0f);
            this.p2Transform.localPosition = new Vector3(700f, 0f, 0f);
        }

        yield return new WaitForSeconds(0.2f);

        if (this.p1Transform != null && this.p2Transform != null)
        {
            float currentTime = 0;
            float duration = 0.15f;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;

                this.p1Transform.localPosition = new Vector3(Mathf.Lerp(-700f, 0f, currentTime / duration), 0f, 0f);
                this.p2Transform.localPosition = new Vector3(Mathf.Lerp(700f, 0f, currentTime / duration), 0f, 0f);
                yield return null;
            }
            this.p1Transform.localPosition = Vector3.zero;
            this.p2Transform.localPosition = Vector3.zero;
        }



        yield return new WaitForSeconds(0.2f);

        this.inDialogue = true;

        yield return this.DisplayText();

        this.inDialogue = false;

        if (this.p1Transform != null && this.p2Transform != null)
        {
            float currentTime = 0;
            float duration = 0.1f;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;

                this.p1Transform.localPosition = new Vector3(0f, Mathf.Lerp(0f, 300f, currentTime / duration), 0f);
                this.p2Transform.localPosition = new Vector3(0f, Mathf.Lerp(0f, -300f, currentTime / duration), 0f);
                yield return null;
            }

            this.p1Transform.gameObject.SetActive(false);
            this.p2Transform.gameObject.SetActive(false);

            this.p1Transform.localPosition = Vector3.zero;
            this.p2Transform.localPosition = Vector3.zero;
        }
    }

    private IEnumerator DisplayText()
    {
        if (this.CharacterIdIsMatching())
        {
            int p1Index = 1;
            int p2Index = 2;
            if (this.p1Id != this.p2Id)
            {
                if (this.p1Id != this.preFightDialogue.characterId1)
                {
                    p1Index = 2;
                    p2Index = 1;
                }
            }
            for (int i = 0; i < this.preFightDialogue.dialogueLines.Length; i++)
            {
                /*TypewriterCore p1typer = p1Index == 1 ? this.p1Typewriter : this.p2Typewriter;
                TypewriterCore p2typer = p2Index == 2 ? this.p2Typewriter : this.p1Typewriter;*/

                //this.p1typer.ShowText(this.preFightDialogue.dialogueLines[i].charDialogue1);
                string p1Dialogue = p1Index == 1 ? this.preFightDialogue.dialogueLines[i].charDialogue1 : this.preFightDialogue.dialogueLines[i].charDialogue2;
                string p2Dialogue = p2Index == 2 ? this.preFightDialogue.dialogueLines[i].charDialogue2 : this.preFightDialogue.dialogueLines[i].charDialogue1;

                this.p1Typewriter.ShowText(p1Dialogue);
                this.p2Typewriter.ShowText(p2Dialogue);

                /*this.p1Typewriter.ShowText(this.preFightDialogue.dialogueLines[i].charDialogue1);
                this.p2Typewriter.ShowText(this.preFightDialogue.dialogueLines[i].charDialogue2);*/

                /*this.p1Typewriter.hideAppearancesOnSkip = true;
                this.p2Typewriter.hideAppearancesOnSkip = true;*/

                while (this.p1Typewriter.isShowingText && !this.skipButtonDown || this.p2Typewriter.isShowingText && !this.skipButtonDown)
                {
                    yield return null;
                }
                
                this.p1Typewriter.SkipTypewriter();
                this.p2Typewriter.SkipTypewriter();

                //add a bool that enables this on dialogue
                //this.p1Typewriter.TriggerRemainingEvents();
                //this.p2Typewriter.TriggerRemainingEvents();

                this.skipButtonDown = false;

                float currentTime = 0;
                //float duration = 1f;
                float duration = this.preFightDialogue.dialogueLines[i].dialogueStayTime;
                while (currentTime < duration && !this.skipButtonDown)
                {
                    currentTime += Time.deltaTime;
                    yield return null;
                }

                this.skipButtonDown = false;

                this.ClearAllText();
            }
        }

        //yield return new WaitForSeconds(0.2f);
    }

    public void ShowText()
    {

    }

    void OnTypewriterMessage(Febucci.UI.Core.Parsing.EventMarker eventMarker)
    {
        switch (eventMarker.name)
        {
            case "explosion":
                if (this.explosionSfx != null)
                    this.explosionSfx.Play();
                break;
            case "debugLog":
                    Debug.Log("test");
                break;
        }
    }

    public bool CharacterIdIsMatching()
    {
        if (this.p1Id == this.preFightDialogue.characterId1 || this.p1Id == this.preFightDialogue.characterId2 &&
            this.p2Id == this.preFightDialogue.characterId1 || this.p2Id == this.preFightDialogue.characterId2)
        {
            if (this.preFightDialogue.characterId1 == this.preFightDialogue.characterId2 && this.p1Id == this.p2Id)
            {
                if (this.p1Id == this.preFightDialogue.characterId1)
                    return true;
                else
                    return false;
            }
            else if (this.preFightDialogue.characterId1 != this.preFightDialogue.characterId2 && this.p1Id != this.p2Id)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            return false;
        }

    }

    public void ClearAllText()
    {
        if (this.p1Text != null)
            this.p1Text.text = "";

        if (this.p2Text != null)
            this.p2Text.text = "";

        /*this.currentP2Text = "";
        this.currentP1Text = "";*/
    }
}
