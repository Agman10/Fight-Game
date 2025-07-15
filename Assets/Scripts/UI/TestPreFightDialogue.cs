using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class TestPreFightDialogue : MonoBehaviour
{
    public SO_PreFightDialogue preFightDialogue;

    public bool inDialogue;

    public int currentDialogueIndex;

    public int p1Id;
    public int p2Id;

    public bool P1InProgress;
    public bool P2InProgress;

    public TextMeshProUGUI p1Text;
    private string currentP1Text = "";

    public TextMeshProUGUI p2Text;
    private string currentP2Text = "";

    public Transform p1Transform;
    public Transform p2Transform;


    public PlayerInput playerInput;
    public bool skipButtonDown;


    private void OnEnable()
    {
        if(this.preFightDialogue != null)
        {
            if(this.p1Text != null && this.p2Text != null)
            {
                /*this.p1Text.text = "";
                this.p2Text.text = "";*/

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

        if (this.playerInput != null)
        {
            this.playerInput.PunchInput += this.Skip;
        }
    }

    private void OnDisable()
    {
        if (this.playerInput != null)
        {
            this.playerInput.PunchInput -= this.Skip;
        }
    }

    public void Skip(bool skipping)
    {
        if (skipping && this.inDialogue)
        {
            this.skipButtonDown = true;
        }
    }

    private IEnumerator DialogueCoroutine()
    {
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
            //float targetVolume = 0.1f;
            /*float targetPosition = 3.5f;
            float start = this.transform.position.y;*/
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

        /*this.StartCoroutine(this.ShowText("P1 fdfssfasda", 1));
        this.StartCoroutine(this.ShowText("P2 ddfss", 2));*/

        yield return this.DisplayText();

        this.inDialogue = false;

        /*this.StartCoroutine(this.ShowText("Fuck you J-Cap!", 1));

        while (this.P1InProgress || this.P2InProgress)
        {
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        this.ClearAllText();

        *//*this.StartCoroutine(this.ShowText("fuck you", 1));
        this.StartCoroutine(this.ShowText("ass", 2));*//*

        this.StartCoroutine(this.ShowText("Nice to meet you too Dark...", 2));*/

        if (this.p1Transform != null && this.p2Transform != null)
        {
            float currentTime = 0;
            float duration = 0.1f;
            //float targetVolume = 0.1f;
            /*float targetPosition = 3.5f;
            float start = this.transform.position.y;*/
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

        //Debug.Log("terwt");
    }

    private IEnumerator DisplayText()
    {
        /*if (this.p1Id == this.preFightDialogue.characterId1 || this.p1Id == this.preFightDialogue.characterId2 &&
            this.p2Id == this.preFightDialogue.characterId1 || this.p2Id == this.preFightDialogue.characterId2)
        {
            Debug.Log("Yes");
        }
        else
        {
            Debug.Log("No");
        }*/

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
                //Debug.Log(i);

                /*if (this.preFightDialogue.dialogueLines[i].charDialogue1.Length > 0)
                    this.StartCoroutine(this.ShowText(this.preFightDialogue.dialogueLines[i].charDialogue1, p1Index));

                if (this.preFightDialogue.dialogueLines[i].charDialogue2.Length > 0)
                    this.StartCoroutine(this.ShowText(this.preFightDialogue.dialogueLines[i].charDialogue2, p2Index));*/


                IEnumerator p1DialogueCoroutine = this.ShowText(this.preFightDialogue.dialogueLines[i].charDialogue1, p1Index);
                IEnumerator p2DialogueCoroutine = this.ShowText(this.preFightDialogue.dialogueLines[i].charDialogue2, p2Index);

                if (this.preFightDialogue.dialogueLines[i].charDialogue1.Length > 0)
                    this.StartCoroutine(p1DialogueCoroutine);

                if (this.preFightDialogue.dialogueLines[i].charDialogue2.Length > 0)
                    this.StartCoroutine(p2DialogueCoroutine);

                /*while (this.P1InProgress || this.P2InProgress)
                {
                    yield return null;
                }

                yield return new WaitForSeconds(1f);*/

                while (this.P1InProgress && !this.skipButtonDown || this.P2InProgress && !this.skipButtonDown)
                {
                    yield return null;
                }

                this.StopCoroutine(p1DialogueCoroutine);
                this.StopCoroutine(p2DialogueCoroutine);

                this.skipButtonDown = false;

                if (this.preFightDialogue.dialogueLines[i].charDialogue1.Length > 0)
                    this.ShowFullText(this.preFightDialogue.dialogueLines[i].charDialogue1, p1Index);

                if (this.preFightDialogue.dialogueLines[i].charDialogue2.Length > 0)
                    this.ShowFullText(this.preFightDialogue.dialogueLines[i].charDialogue2, p2Index);

                float currentTime = 0;
                float duration = 1f;
                while (currentTime < duration && !this.skipButtonDown)
                {
                    currentTime += Time.deltaTime;
                    yield return null;
                }

                this.skipButtonDown = false;

                //yield return new WaitForSeconds(1f);

                this.ClearAllText();
            }

            /*if (this.p1Id == this.p2Id)
            {
                for (int i = 0; i < this.preFightDialogue.dialogueLines.Length; i++)
                {
                    if (this.preFightDialogue.dialogueLines[i].charDialogue1.Length > 0)
                        this.StartCoroutine(this.ShowText(this.preFightDialogue.dialogueLines[i].charDialogue1, 1));

                    if (this.preFightDialogue.dialogueLines[i].charDialogue2.Length > 0)
                        this.StartCoroutine(this.ShowText(this.preFightDialogue.dialogueLines[i].charDialogue2, 2));

                    //Debug.Log(this.preFightDialogue.dialogueLines[i].charDialogue2.ToString());

                    while (this.P1InProgress || this.P2InProgress)
                    {
                        yield return null;
                    }

                    yield return new WaitForSeconds(1f);

                    this.ClearAllText();
                }
            }
            else
            {
                
            }*/

            
        }

        
        //yield return new WaitForSeconds(1f);
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

    IEnumerator ShowText(string fullText, int playerNumber)
    {
        //yield return new WaitForSeconds(0.2f);
        if (playerNumber == 2)
        {
            this.p2Text.text = "";
            this.currentP2Text = "";
            this.P2InProgress = true;
        }
        else
        {
            this.p1Text.text = "";
            this.currentP1Text = "";
            this.P1InProgress = true;
        }

        //yield return new WaitForSeconds(0.2f);

        float extraWaitTime = 0f;
        for (int i = 0; i < fullText.Length + 1; i++)
        {
            /*this.currentText = fullText.Substring(0, i);
            this.victoryQuoteText.text = this.currentText;
            yield return new WaitForSeconds(this.textTypeDelay);*/

            if (i < fullText.Length)
            {
                //Debug.Log(fullText.Substring(i, 1));
                //this.currentText = fullText.Substring(0, i);
                extraWaitTime = 0f;
                if (fullText.Substring(i, 1) != "§" && fullText.Substring(i, 1) != "½" /*&& fullText.Substring(i, 8) != "[DELETE]"*/)
                {
                    //this.currentP1Text = this.currentP1Text + fullText.Substring(i, 1);

                    if (playerNumber == 2)
                        this.currentP2Text = this.currentP2Text + fullText.Substring(i, 1);
                    else
                        this.currentP1Text = this.currentP1Text + fullText.Substring(i, 1);
                }
                /*else if (fullText.Substring(i, 8) == "[DELETE]")
                {
                    if (playerNumber == 2)
                    {
                        this.p2Text.text = "";
                        this.currentP2Text = "";
                    }
                    else
                    {
                        this.p1Text.text = "";
                        this.currentP1Text = "";
                    }
                }*/
                else
                {
                    if (fullText.Substring(i, 1) == "§")
                        extraWaitTime = 0.4f;
                    else
                        extraWaitTime = 0.05f;
                }

                //this.p1Text.text = this.currentP1Text;

                if (playerNumber == 2)
                    this.p2Text.text = this.currentP2Text;
                else
                    this.p1Text.text = this.currentP1Text;

                yield return new WaitForSeconds(0.05f + extraWaitTime);
            }


        }

        if (playerNumber == 2)
            this.P2InProgress = false;
        else
            this.P1InProgress = false;
    }

    

    public void ClearAllText()
    {
        if(this.p1Text != null)
            this.p1Text.text = "";

        if (this.p2Text != null)
            this.p2Text.text = "";
        
        this.currentP2Text = "";
        this.currentP1Text = "";
    }

    public void ShowFullText(string fullText, int playerNumber)
    {
        //string textt = Regex.Replace(fullText, "[§\\½]", "");
        string textt = Regex.Replace(fullText, "[§\\½]", "");
        //Debug.Log(textt);

        //string[] separators = new string[] { "§", "½" }; // still need to use the escape char to denote the quotations
        //string[] numbers = fullText.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        if (playerNumber == 2)
        {
            this.p2Text.text = textt;
            this.currentP2Text = textt;
            this.P2InProgress = false;
        }
        else
        {
            this.p1Text.text = textt;
            this.currentP1Text = textt;
            this.P1InProgress = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
