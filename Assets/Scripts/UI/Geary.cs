using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Geary : MonoBehaviour
{
    public OptionsMenu options;
    public bool evil;
    public bool disabled;

    [Space]

    public GameObject model;
    public GameObject face;

    public GameObject rightEyebrow;
    public GameObject leftEyebrow;
    public GameObject rightPupil;
    public GameObject leftPupil;
    public GameObject cog;

    public GameObject rightEyeHalfClosed;
    public GameObject rightEyeFullyOpened;
    public GameObject leftEyeHalfClosed;
    public GameObject leftEyeFullyOpened;

    [Space]
    public GameObject rightEyeOpen;
    public GameObject leftEyeOpen;

    public GameObject rightEyeClosed;
    public GameObject leftEyeClosed;

    public GameObject[] flameEyes;
    public GameObject[] blushes;

    [Space]

    public GameObject textBubble;
    //public Text dialougeText;
    public TMP_Text dialougeText;

    //private float textTypeDelay = 0.05f;
    private string currentText = "";

    public int currentDialogueId;
    public int currentTurnOffGearyDialougeId;

    public bool dialougeLocked;

    [Space]
    public Text disableOptionText;
    public GameObject[] textBoxTriangles;
    public GameObject[] textBoxTrianglesDisabled;


    private void OnEnable()
    {
        //this.dialougeText.text = this.currentText;
        //this.StartCoroutine(this.ShowText("Hello I'm Geary."));
        this.GearyStart();
    }

    public void GearyStart()
    {
        this.dialougeLocked = true;
        this.dialougeText.text = this.currentText;
        this.textBubble.gameObject.SetActive(false);
        this.StartCoroutine(this.GearyStartCoroutine());
    }
    private IEnumerator GearyStartCoroutine()
    {
        /*if (this.options != null && this.options.eventSystem != null)
            this.options.eventSystem.SetActive(false);*/

        if (this.options != null)
            this.options.EnableEventSystem(false);

        float eyebrowYLow = 0.53f;
        float eyebrowYHigh = 0.58f;

        this.SetRightEyebrow(0.4f, eyebrowYLow, 0.2f, 90f);
        this.SetLeftEyebrow(-0.4f, eyebrowYLow, 0.2f, 90f);

        float currentTime = 0;
        float duration = 0.3f;
        float startPos = 4f;
        //float startPos = -2.5f;

        //this.StartCoroutine(this.SpinCog(0.3f, 4));
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.model.transform.localPosition = new Vector3(Mathf.Lerp(startPos, 0f, currentTime / duration), 0f, 0f);
            //this.model.transform.localPosition = new Vector3(0f, Mathf.Lerp(startPos, 0f, currentTime / duration), 0f);
            yield return null;
        }
        this.model.transform.localPosition = Vector3.zero;


        /*this.SetRightEyebrow(0.4f, 0.55f, 0.2f, 90f);
        this.SetLeftEyebrow(-0.4f, 0.55f, 0.2f, 90f);*/

        



        int randomEntrance = Random.Range(0, 101);
        if (this.evil)
        {
            
            if(this.face != null)
            {
                yield return new WaitForSeconds(0.15f);
                this.SetDefaultExpression();
                this.rightEyeOpen.SetActive(false);
                this.leftEyeOpen.SetActive(false);

                this.rightEyeClosed.SetActive(true);
                this.leftEyeClosed.SetActive(true);
                /*yield return new WaitForSeconds(0.05f);
                this.face.transform.localPosition = new Vector3(0f, -0.05f, 0f);
                yield return new WaitForSeconds(0.05f);
                this.face.transform.localPosition = new Vector3(0f, 0f, 0f);
                yield return new WaitForSeconds(0.05f);
                this.face.transform.localPosition = new Vector3(0f, -0.05f, 0f);
                yield return new WaitForSeconds(0.05f);
                this.face.transform.localPosition = new Vector3(0f, 0f, 0f);*/


                int amount = 15;
                int laughId = 0;
                //bool idForward = true;
                while (amount > 0)
                {
                    //currentTime += Time.deltaTime;
                    yield return new WaitForSeconds(0.05f);
                    if (laughId == 0)
                    {
                        this.face.transform.localPosition = new Vector3(0f, -0.02f, 0f);
                        laughId = 1;
                    }
                    else
                    {
                        this.face.transform.localPosition = new Vector3(0f, 0f, 0f);
                        laughId = 0;
                    }
                        

                    amount -= 1;

                    yield return null;
                }
                yield return new WaitForSeconds(0.05f);
                this.face.transform.localPosition = new Vector3(0f, 0f, 0f);
                yield return new WaitForSeconds(0.1f);

                this.rightEyeOpen.SetActive(true);
                this.leftEyeOpen.SetActive(true);

                this.rightEyeClosed.SetActive(false);
                this.leftEyeClosed.SetActive(false);
                this.SetDefaultExpression();
            }
        }
        else
        {
            if (randomEntrance <= 25)
            {
                yield return new WaitForSeconds(0.1f);
                this.StartCoroutine(this.SpinCog(0.5f, 4));
                yield return new WaitForSeconds(0.5f);
            }
            else if (randomEntrance > 25 && randomEntrance <= 50)
            {
                yield return new WaitForSeconds(0.1f);
                this.SetDefaultExpression();
                yield return new WaitForSeconds(0.3f);
                this.model.transform.localEulerAngles = new Vector3(0f, 179f, -0.5f);

                //this.SetRightEyebrow(0.4f, 0.52f, 0.2f, 85f);

                this.rightEyeOpen.SetActive(false);
                //this.leftEyeOpen.SetActive(false);
                this.rightEyeClosed.SetActive(true);
                yield return new WaitForSeconds(0.2f);
                this.OpenEyes();
                this.model.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                //this.SetDefaultExpression();
                yield return new WaitForSeconds(0.3f);
                //this.leftEyeClosed.SetActive(true);
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
                this.SetRightEyebrow(0.4f, eyebrowYHigh, 0.2f, 90f);
                this.SetLeftEyebrow(-0.4f, eyebrowYHigh, 0.2f, 90f);
                yield return new WaitForSeconds(0.1f);
                this.SetRightEyebrow(0.4f, eyebrowYLow, 0.2f, 90f);
                this.SetLeftEyebrow(-0.4f, eyebrowYLow, 0.2f, 90f);
                yield return new WaitForSeconds(0.1f);
                this.SetRightEyebrow(0.4f, eyebrowYHigh, 0.2f, 90f);
                this.SetLeftEyebrow(-0.4f, eyebrowYHigh, 0.2f, 90f);
                yield return new WaitForSeconds(0.1f);
                this.SetRightEyebrow(0.4f, eyebrowYLow, 0.2f, 90f);
                this.SetLeftEyebrow(-0.4f, eyebrowYLow, 0.2f, 90f);
            }
        }
        


        yield return new WaitForSeconds(0.2f);
        this.SetDefaultExpression();
        this.textBubble.gameObject.SetActive(true);


        //this.StartCoroutine(this.SpinCog(0.5f, 4));


        yield return new WaitForSeconds(0.2f);

        //string textString = "Hello I'm Geary. How can I help you?";

        string textString = "Hi! Geary here. Right now the settings are under construction so it might look a little weird right now.";

        //Debug.Log(textString.Length);
        int number = Random.Range(0, 2);
        /*if (number == 1)
            textString = "Hello my name is Geary. I'm here to help you with the settings.";*/

        if (this.evil)
        {
            textString = "Hello I'm Evil Geary. How can I ruin your day?";

            if (number == 1)
                textString = "Hello my name is Evil Geary. I'm here to ruin your settings.";
        }
        else
        {
            /*if (number == 1)
                textString = "Hello my name is Geary. I'm here to help you with the settings.";*/
        }
        


        //this.StartCoroutine(this.ShowText("Hello I'm Geary."));
        this.StartCoroutine(this.ShowText(textString));
        //yield return new WaitForSeconds(0.2f);
        //yield return new WaitForSeconds(0.1f + (textString.Length * 0.05f));
        yield return new WaitForSeconds(0.1f + (16f * 0.05f));
        this.dialougeLocked = false;

        if (this.options != null)
            this.options.EnableEventSystem(true);

        this.StartCoroutine(this.Blink());

        /*if (this.options != null && this.options.eventSystem != null)
            this.options.eventSystem.SetActive(true);*/
    }

































    public void SelectLanguage()
    {
        if (!this.dialougeLocked && this.currentDialogueId != 1)
        {
            this.StopAllCoroutines();
            this.currentDialogueId = 1;

            if (this.evil)
            {

            }
            else
            {

            }

            /*this.SetDefaultExpression();
            this.SetRightEyebrow(0.4f, 0.63f, 0.2f, 85f);
            this.SetLeftEyebrow(-0.4f, 0.52f, 0.2f, 80f);*/
            this.SetExpression(1);
            this.StartCoroutine(this.ShowText("Change the Language?"));
            this.StartCoroutine(this.Blink());
        }
    }

    public void SetLanguage()
    {
        if (!this.dialougeLocked && this.currentDialogueId != 2)
        {
            this.StopAllCoroutines();
            this.currentDialogueId = 2;

            /*this.SetDefaultExpression();
            this.SetRightEye(0, -0.085f, 0.04f, 0.083f, -12.5f);
            this.SetLeftEye(0, -0.085f, 0.04f, 0.083f, -12.5f);*/

            if (this.evil)
            {
                this.SetExpression(2);
                this.StartCoroutine(this.ShowText("It doesn't work. Just learn english."));
            }
            else
            {
                this.SetExpression(2);
                this.StartCoroutine(this.ShowText("Yeah... We don't currently have support for more languages..."));
            }

            
            this.StartCoroutine(this.Blink());
        }

        
    }

    public void OnSelectTurnOffGeary()
    {
        if (!this.dialougeLocked && this.currentDialogueId != 3)
        {
            this.StopAllCoroutines();
            this.currentDialogueId = 3;

            /*this.SetDefaultExpression();
            this.SetRightEyebrow(0.4f, 0.58f, 0.2f, 80f);
            this.SetLeftEyebrow(-0.4f, 0.53f, 0.2f, 91f);*/

            if (this.evil)
            {
                this.SetExpression(2);
                this.StartCoroutine(this.ShowText("Turn me off? Your presence has alredy done that."));
            }
            else
            {
                if (this.currentTurnOffGearyDialougeId > 3)
                {
                    this.SetExpression(6);
                    this.SetRightEyebrow(0.4f, 0.50f, 0.2f, 92f);
                    this.SetLeftEyebrow(-0.4f, 0.50f, 0.2f, 88f);

                    this.StartCoroutine(this.ShowText("Don't!"));

                    /*this.SetExpression(5);
                    this.SetRightEyebrow(0.38f, 0.54f, 0.2f, 105f);
                    this.SetLeftEyebrow(-0.38f, 0.615f, 0.2f, 85f);*/
                }
                else
                {
                    int number = Random.Range(0, 2);
                    if (number == 1)
                    {
                        this.SetExpression(8);
                        //this.StartCoroutine(this.ShowText("Pfft, why would anyone even want to do that?"));
                        this.StartCoroutine(this.ShowText("Pfft, why would anyone ever want change that?"));
                    }
                    else
                    {
                        this.SetExpression(3);
                        this.StartCoroutine(this.ShowText("This is my least favorite option..."));
                    }
                }
            }

            
            
            
            this.StartCoroutine(this.Blink());
        }

        
    }
    public void OnTurnOffGeary()
    {
        if (!this.dialougeLocked /*&& this.currentDialogueId != 5*/)
        {
            //this.StopAllCoroutines();

            //this.currentDialogueId = 5;
            //this.SetExpression(5);

            //this.StartCoroutine(this.ShowText("No!"));

            if (this.evil)
            {
                
                if (!this.disabled)
                {
                    this.StopAllCoroutines();
                    this.StartCoroutine(this.TurnOffEvilGearyCoroutine());
                    this.StartCoroutine(this.Blink());
                }
                
                /*if (this.currentTurnOffGearyDialougeId != 9)
                    this.StartCoroutine(this.Blink());
                else
                    this.OpenEyes();*/
            }
            else
            {
                this.StopAllCoroutines();
                this.StartCoroutine(this.TurnOffGearyCoroutine());

                if (this.currentTurnOffGearyDialougeId != 9)
                    this.StartCoroutine(this.Blink());
                else
                    this.OpenEyes();
            }

            /*this.StartCoroutine(this.TurnOffGearyCoroutine());

            if (this.currentTurnOffGearyDialougeId != 9)
                this.StartCoroutine(this.Blink());
            else
                this.OpenEyes();*/
        }
    }

    public void OnSelectExit()
    {
        if (!this.dialougeLocked && this.currentDialogueId != 4)
        {
            this.StopAllCoroutines();
            this.currentDialogueId = 4;

            /*this.SetDefaultExpression();
            this.SetRightEyebrow(0.4f, 0.55f, 0.2f, 85f);
            this.SetLeftEyebrow(-0.4f, 0.7f, 0.2f, 65f);*/
            

            string textString = "Go back to the menu?";

            if (this.evil)
            {
                this.SetExpression(6);

                //textString = "Please leave.";
                textString = "Plase do that. I don't want to see you anymore.";

                /*int number = Random.Range(0, 3);
                if (number == 1)
                    textString = "Plase do that I don't want to see you anymore.";
                else if (number == 2)
                    textString = "Need to get back to beating up more rivals?";*/
            }
            else
            {
                this.SetExpression(4);

                int number = Random.Range(0, 3);
                if (number == 1)
                    textString = "Going back to the fight huh?";
                else if (number == 2)
                    textString = "Need to get back to beating up more rivals?";
            }
            

            //this.StartCoroutine(this.ShowText("Go back to the menu?"));
            
            

            this.StartCoroutine(this.ShowText(textString));

            this.StartCoroutine(this.Blink());
        }

        
    }

    public void OnExit()
    {
        if (!this.dialougeLocked && this.currentDialogueId != 5)
        {
            this.StopAllCoroutines();
            this.currentDialogueId = 5;

            if (this.evil)
                this.SetExpression(4);
            else
                this.SetDefaultExpression();

            /*this.SetRightEyebrow();
            this.SetLeftEyebrow(-0.4f, 0.7f, 0.2f, 65f);*/
            //this.StartCoroutine(this.ShowText("Bye bye!"));

            if (this.options != null)
                this.options.EnableEventSystem(false);

            this.StartCoroutine(this.GoBackToMenu());
            this.StartCoroutine(this.Blink());
        }
    }

    public void OnSelectVolume()
    {
        if (!this.dialougeLocked && this.currentDialogueId != 6)
        {
            this.StopAllCoroutines();
            this.currentDialogueId = 6;

            this.SetExpression(0);
            this.SetRightEyebrow(0.4f, 0.52f, 0.2f, 85f);
            this.SetLeftEyebrow(-0.4f, 0.53f, 0.2f, 82f);

            this.StartCoroutine(this.ShowText("So you want to change the audio volume?"));


            this.StartCoroutine(this.Blink());
        }
    }
    public void OnVolumeChange()
    {
        if (!this.dialougeLocked && this.currentDialogueId != 7)
        {
            this.StopAllCoroutines();
            this.currentDialogueId = 7;

            if (this.evil)
            {
                this.SetExpression(10, false, false);
                this.StartCoroutine(this.ShowText("Yeah... It doesn't work right now since the developer doesn't know how to prioritize right."));
            }
            else
            {
                this.SetExpression(9, true, false);
                this.StartCoroutine(this.ShowText("I gotta be honest.½ We haven't implemented support for changing volume yet.½ This is just a placeholder...§ Sorry"));
            }

            


            this.StartCoroutine(this.Blink());
        }
    }

    public void OnSelectMaxWins()
    {
        if (!this.dialougeLocked && this.currentDialogueId != 8)
        {
            this.StopAllCoroutines();
            this.currentDialogueId = 8;

            if (this.evil)
            {

            }
            else
            {

            }

            /*this.SetDefaultExpression();
            this.SetRightEyebrow(0.4f, 0.63f, 0.2f, 85f);
            this.SetLeftEyebrow(-0.4f, 0.52f, 0.2f, 80f);*/
            this.SetExpression(0);
            this.StartCoroutine(this.ShowText("Set the amount of rounds you need to win in order to become victorious."));
            this.StartCoroutine(this.Blink());
        }
    }

    public void OnSelectTimer()
    {
        if (!this.dialougeLocked && this.currentDialogueId != 9)
        {
            this.StopAllCoroutines();
            this.currentDialogueId = 9;

            if (this.evil)
            {

            }
            else
            {

            }

            /*this.SetDefaultExpression();
            this.SetRightEyebrow(0.4f, 0.63f, 0.2f, 85f);
            this.SetLeftEyebrow(-0.4f, 0.52f, 0.2f, 80f);*/
            this.SetExpression(1);
            this.StartCoroutine(this.ShowText("Set how long a round can last."));
            this.StartCoroutine(this.Blink());
        }
    }


    public void OnSelectAudio()
    {
        if (!this.dialougeLocked && this.currentDialogueId != 10)
        {
            this.StopAllCoroutines();
            this.currentDialogueId = 10;

            if (this.evil)
            {

            }
            else
            {

            }

            /*this.SetDefaultExpression();
            this.SetRightEyebrow(0.4f, 0.63f, 0.2f, 85f);
            this.SetLeftEyebrow(-0.4f, 0.52f, 0.2f, 80f);*/
            this.SetExpression(1);
            this.StartCoroutine(this.ShowText("Adjust the volume on the sounds."));
            this.StartCoroutine(this.Blink());
        }
    }

    public void OnSelectVideo()
    {
        if (!this.dialougeLocked && this.currentDialogueId != 11)
        {
            this.StopAllCoroutines();
            this.currentDialogueId = 11;

            if (this.evil)
            {

            }
            else
            {

            }

            /*this.SetDefaultExpression();
            this.SetRightEyebrow(0.4f, 0.63f, 0.2f, 85f);
            this.SetLeftEyebrow(-0.4f, 0.52f, 0.2f, 80f);*/
            this.SetExpression(0);
            this.StartCoroutine(this.ShowText("Change the video settings"));
            this.StartCoroutine(this.Blink());
        }
    }

    public void OnSelectMisc()
    {
        if (!this.dialougeLocked && this.currentDialogueId != 12)
        {
            this.StopAllCoroutines();
            this.currentDialogueId = 12;

            if (this.evil)
            {

            }
            else
            {

            }

            /*this.SetDefaultExpression();
            this.SetRightEyebrow(0.4f, 0.63f, 0.2f, 85f);
            this.SetLeftEyebrow(-0.4f, 0.52f, 0.2f, 80f);*/
            this.SetExpression(0);
            this.StartCoroutine(this.ShowText("Change some miscelanios stuff."));
            this.StartCoroutine(this.Blink());
        }
    }

    public void OnSelectBack()
    {
        if (!this.dialougeLocked && this.currentDialogueId != 13)
        {
            this.StopAllCoroutines();
            this.currentDialogueId = 13;

            if (this.evil)
            {

            }
            else
            {

            }

            /*this.SetDefaultExpression();
            this.SetRightEyebrow(0.4f, 0.63f, 0.2f, 85f);
            this.SetLeftEyebrow(-0.4f, 0.52f, 0.2f, 80f);*/
            this.SetExpression(1);
            this.StartCoroutine(this.ShowText("Go back to main settings."));
            this.StartCoroutine(this.Blink());
        }
    }


    public void OnSelectUnavalaible()
    {
        if (!this.dialougeLocked && this.currentDialogueId != 100)
        {
            this.StopAllCoroutines();
            this.currentDialogueId = 100;

            if (this.evil)
            {

            }
            else
            {

            }

            /*this.SetDefaultExpression();
            this.SetRightEyebrow(0.4f, 0.63f, 0.2f, 85f);
            this.SetLeftEyebrow(-0.4f, 0.52f, 0.2f, 80f);*/
            this.SetExpression(1);
            this.StartCoroutine(this.ShowText("It doesn't work right now."));
            this.StartCoroutine(this.Blink());
        }
    }

    public void OnClickUnavailable()
    {
        if (!this.dialougeLocked && this.currentDialogueId != 101)
        {
            this.StopAllCoroutines();
            this.currentDialogueId = 101;

            if (this.evil)
            {
                this.SetExpression(5);
                this.StartCoroutine(this.ShowText("Still not working... DUMB ASS!"));
            }
            else
            {
                this.SetExpression(2);
                this.StartCoroutine(this.ShowText("Still not working..."));
            }


            this.StartCoroutine(this.Blink());
        }


    }





    /*public void OnSelectCustom(int dialogueId = 0, string normalDialogue = "")
    {
        if (!this.dialougeLocked && this.currentDialogueId != 100)
        {
            this.StopAllCoroutines();
            this.currentDialogueId = 100;

            if (this.evil)
            {

            }
            else
            {

            }

            *//*this.SetDefaultExpression();
            this.SetRightEyebrow(0.4f, 0.63f, 0.2f, 85f);
            this.SetLeftEyebrow(-0.4f, 0.52f, 0.2f, 80f);*//*
            this.SetExpression(1);
            this.StartCoroutine(this.ShowText("It doesn't work right now."));
            this.StartCoroutine(this.Blink());
        }
    }

    public void OnClickCustom()
    {
        if (!this.dialougeLocked && this.currentDialogueId != 101)
        {
            this.StopAllCoroutines();
            this.currentDialogueId = 101;

            if (this.evil)
            {
                this.SetExpression(2);
                this.StartCoroutine(this.ShowText("Still not working... DUMB ASS!"));
            }
            else
            {
                this.SetExpression(2);
                this.StartCoroutine(this.ShowText("Still not working..."));
            }


            this.StartCoroutine(this.Blink());
        }


    }*/




























    private IEnumerator GoBackToMenu()
    {
        if (this.evil)
        {
            if (this.disabled)
            {
                this.textBubble.gameObject.SetActive(true);
                string textString = "That doesn't work since you disabled me... So i guess you're softlocked now...";

                this.StartCoroutine(this.ShowText(textString));

                //yield return new WaitForSeconds(1.5f + (textString.Length * 0.05f));
                //yield return new WaitForSeconds(textString.Length * 0.05f);
                yield return new WaitForSeconds(0.2f);


                if (this.options != null)
                    this.options.EnableEventSystem(true);
            }
            else
            {
                string textString = "Hey! Want to see something funny?";

                this.StartCoroutine(this.ShowText(textString));
                yield return new WaitForSeconds(1.5f + (textString.Length * 0.05f));

                Debug.Log("SHUT DOWN");
                Application.Quit();
            }
            /*string textString = "Hey! Want to see something funny?";

            this.StartCoroutine(this.ShowText(textString));
            yield return new WaitForSeconds(1.5f + (textString.Length * 0.05f));

            Debug.Log("SHUT DOWN");
            Application.Quit();*/

            /*if (this.options != null)
                this.options.FadeToBlackAndGoToMenu();*/
        }
        else
        {
            string textString = "Bye bye!";
            int number = Random.Range(0, 3);
            if (number == 1)
                textString = "Fight On!";
            else if (number == 2)
                textString = "Come back if you need to do more adjustments.";

            this.StartCoroutine(this.ShowText(textString));
            yield return new WaitForSeconds(0.8f + (textString.Length * 0.05f));

            if (this.options != null)
                this.options.FadeToBlackAndGoToMenu();
        }

        

        /*if (this.options != null)
            this.options.GoBackToMenu();*/
    }





    private IEnumerator TurnOffGearyCoroutine()
    {
        if (this.options != null)
            this.options.EnableEventSystem(false);

        string textString = "No!";
        //string textString = "I'm not letting you do that.";
        if (this.currentTurnOffGearyDialougeId == 1)
        {
            textString = "Nah.";
            this.SetExpression(2);
        }
        else if (this.currentTurnOffGearyDialougeId == 2)
        {
            textString = "You know I'm in charge right? So I don't have comply to this if I don't want to.";
            this.SetExpression(0);
            this.SetLeftEyebrow(-0.4f, 0.53f, 0.2f, 87f);
        }
        else if (this.currentTurnOffGearyDialougeId == 3)
        {
            textString = "I said NO!";
            this.SetExpression(5);
        }
        else if (this.currentTurnOffGearyDialougeId == 4)
        {
            textString = "How many times do I have to tell you this!?";
            this.SetExpression(5);
            this.SetRightEyebrow(0.38f, 0.53f, 0.2f, 95f);
            this.SetLeftEyebrow(-0.38f, 0.53f, 0.2f, 85f);
        }
        else if (this.currentTurnOffGearyDialougeId == 5)
        {
            textString = "No means NO!";
            this.SetExpression(5);
            
        }
        else if (this.currentTurnOffGearyDialougeId == 6)
        {
            textString = "Why are you even trying to disable me? It really hurts!";
            this.SetExpression(7);
        }
        else if (this.currentTurnOffGearyDialougeId == 7)
        {
            textString = "IM NOT LEAVING!!";
            this.SetExpression(5);
        }
        else if (this.currentTurnOffGearyDialougeId == 8)
        {
            textString = "If you try it one more time im gonna have to retaliate!";
            this.SetExpression(5);
            this.SetRightEyebrow(0.38f, 0.52f, 0.2f, 105f);
            this.SetLeftEyebrow(-0.38f, 0.52f, 0.2f, 75f);
            //this.StartCoroutine(this.SpinCog(0.25f, 2));
        }
        else if (this.currentTurnOffGearyDialougeId == 9)
        {
            textString = "Last warning!";
            this.SetExpression(5, false, true);
            this.SetRightEye(0, 0f, 0f, 0.083f, 0f);
            this.SetLeftEye(0, 0f, 0f, 0.083f, 0f);

        }
        else if (this.currentTurnOffGearyDialougeId == 10)
        {
            textString = "Alright you asked for it!";
            this.SetExpression(5);
        }
        else
        {
            textString = "I'm not letting you do that.";
            this.SetExpression(6);
        }
        

        this.StartCoroutine(this.ShowText(textString));

        //yield return new WaitForSeconds(0.4f + (textString.Length * 0.05f));
        yield return new WaitForSeconds(textString.Length * 0.05f);

        if(this.currentTurnOffGearyDialougeId >= 10)
        {
            yield return new WaitForSeconds(1.4f);
            Debug.Log("SHUT DOWN");
            Application.Quit();
        }
        else
        {
            this.currentTurnOffGearyDialougeId++;
        }
        yield return new WaitForSeconds(0.4f);
        //Debug.Log("t");
        if (this.options != null)
            this.options.EnableEventSystem(true);
    }


    private IEnumerator TurnOffEvilGearyCoroutine()
    {
        /*if (this.options != null)
            this.options.EnableEventSystem(false);*/

        //string textString = "No!";
        if (this.options != null)
            this.options.EnableEventSystem(false);
        //this.currentTurnOffGearyDialougeId++;
        string textString = "Fine! I hated you anyway.";
        this.SetExpression(6);

        /*this.SetRightEye(1, 0.06f, 0.06f, 0.083f, -8f);
        this.SetLeftEye(1, 0.06f, 0.06f, 0.083f, -8f);*/

        this.StartCoroutine(this.ShowText(textString));

        //yield return new WaitForSeconds(0.4f + (textString.Length * 0.05f));
        yield return new WaitForSeconds(textString.Length * 0.05f + 1f);

        /*if (this.disableOptionText != null)
            this.disableOptionText.text = "Disabled";*/

        this.textBubble.gameObject.SetActive(false);

        //this.textBubble.transform.localPosition = new Vector3(0.7f, 0.45f, -0.1f);
        this.MoveDialougeBox(true);

        this.SetRightEye(0, -0.06f, 0f, 0.083f, 0f);
        this.SetLeftEye(0, -0.06f, 0f, 0.083f, 0f);

        float currentTime = 0;
        float duration = 0.3f;
        float startPos = 0f;
        //float startPos = -2.5f;

        //this.StartCoroutine(this.SpinCog(0.3f, 4));
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.model.transform.localPosition = new Vector3(Mathf.Lerp(startPos, 4f, currentTime / duration), 0f, 0f);
            //this.model.transform.localPosition = new Vector3(0f, Mathf.Lerp(startPos, 0f, currentTime / duration), 0f);
            yield return null;
        }
        this.model.transform.localPosition = new Vector3(4f, 0f, 0f);

        if (this.disableOptionText != null)
            this.disableOptionText.text = "Disabled";

        this.disabled = true;

        yield return new WaitForSeconds(0.4f);
        if (this.options != null)
            this.options.EnableEventSystem(true);



        if (this.disabled)
        {
            /*textString = "I'm not letting you do that.";
            this.SetExpression(6);*/
        }
        else
        {
            /*if (this.options != null)
                this.options.EnableEventSystem(false);
            //this.currentTurnOffGearyDialougeId++;
            string textString = "Fine! I hated you anyway";
            this.SetExpression(6);

            this.StartCoroutine(this.ShowText(textString));

            //yield return new WaitForSeconds(0.4f + (textString.Length * 0.05f));
            yield return new WaitForSeconds(textString.Length * 0.05f);


            this.disabled = true;

            yield return new WaitForSeconds(0.4f);
            if (this.options != null)
                this.options.EnableEventSystem(true);*/
        }

        /*this.StartCoroutine(this.ShowText(textString));

        //yield return new WaitForSeconds(0.4f + (textString.Length * 0.05f));
        yield return new WaitForSeconds(textString.Length * 0.05f);*/

        /*yield return new WaitForSeconds(0.4f);
        if (this.options != null)
            this.options.EnableEventSystem(true);*/
    }

    private IEnumerator Blink()
    {
        if (this.rightEyeOpen != null && this.rightEyeClosed && this.leftEyeOpen != null && this.leftEyeClosed != null)
        {
            this.rightEyeOpen.SetActive(true);
            this.leftEyeOpen.SetActive(true);

            this.rightEyeClosed.SetActive(false);
            this.leftEyeClosed.SetActive(false);
        }

        //yield return new WaitForSeconds(Random.Range(this.minBlinkTime, this.maxBlinkTime));
        yield return new WaitForSeconds(Random.Range(3f, 6f));

        if(this.rightEyeOpen != null && this.rightEyeClosed && this.leftEyeOpen != null && this.leftEyeClosed != null)
        {
            this.rightEyeOpen.SetActive(false);
            this.leftEyeOpen.SetActive(false);

            this.rightEyeClosed.SetActive(true);
            this.leftEyeClosed.SetActive(true);
        }

        yield return new WaitForSeconds(Random.Range(0.08f, 0.12f));

        if (this.rightEyeOpen != null && this.rightEyeClosed && this.leftEyeOpen != null && this.leftEyeClosed != null)
        {
            this.rightEyeOpen.SetActive(true);
            this.leftEyeOpen.SetActive(true);

            this.rightEyeClosed.SetActive(false);
            this.leftEyeClosed.SetActive(false);
        }

        yield return StartCoroutine(this.Blink());
    }

    public void SetExpression(int expressionId = 0, bool blush = false, bool flameEyes = false)
    {
        if(expressionId == 1)
        {
            this.SetRightEyebrow(0.4f, 0.63f, 0.2f, 85f);
            this.SetLeftEyebrow(-0.4f, 0.52f, 0.2f, 80f);

            this.SetRightEye(0, 0f, 0f, 0.083f, 0f);
            this.SetLeftEye(0, 0f, 0f, 0.083f, 0f);
        }
        else if (expressionId == 2)
        {
            this.SetRightEyebrow(0.4f, 0.58f, 0.2f, 85f);
            this.SetLeftEyebrow(-0.4f, 0.53f, 0.2f, 80f);

            this.SetRightEye(0, -0.085f, 0.04f, 0.083f, -12.5f);
            this.SetLeftEye(0, -0.085f, 0.04f, 0.083f, -12.5f);
        }
        else if (expressionId == 3)
        {
            this.SetRightEyebrow(0.4f, 0.58f, 0.2f, 80f);
            this.SetLeftEyebrow(-0.4f, 0.53f, 0.2f, 91f);

            this.SetRightEye(0, 0f, 0f, 0.083f, 0f);
            this.SetLeftEye(0, 0f, 0f, 0.083f, 0f);
        }
        else if (expressionId == 4)
        {
            this.SetRightEyebrow(0.4f, 0.55f, 0.2f, 85f);
            this.SetLeftEyebrow(-0.4f, 0.7f, 0.2f, 65f);

            this.SetRightEye(0, 0f, 0f, 0.083f, 0f);
            this.SetLeftEye(0, 0f, 0f, 0.083f, 0f);
        }
        else if (expressionId == 5)
        {
            this.SetRightEyebrow(0.38f, 0.53f, 0.2f, 105f);
            this.SetLeftEyebrow(-0.38f, 0.53f, 0.2f, 75f);

            this.SetRightEye(0, 0f, 0f, 0.083f, 0f);
            this.SetLeftEye(0, 0f, 0f, 0.083f, 0f);
        }
        else if (expressionId == 6)
        {
            this.SetRightEyebrow(0.4f, 0.50f, 0.2f, 90f);
            this.SetLeftEyebrow(-0.4f, 0.50f, 0.2f, 90f);

            this.SetRightEye(0, 0f, 0f, 0.083f, 0f);
            this.SetLeftEye(0, 0f, 0f, 0.083f, 0f);
        }
        else if (expressionId == 7)
        {
            this.SetRightEyebrow(0.4f, 0.53f, 0.2f, 80f);
            this.SetLeftEyebrow(-0.4f, 0.53f, 0.2f, 100f);

            this.SetRightEye(0, 0f, -0.02f, 0.083f, 0f);
            this.SetLeftEye(0, 0f, -0.02f, 0.083f, 0f);
        }
        else if (expressionId == 8)
        {
            this.SetRightEyebrow(0.4f, 0.58f, 0.2f, 80f);
            this.SetLeftEyebrow(-0.4f, 0.53f, 0.2f, 85f);

            this.SetRightEye(1, 0.06f, 0.06f, 0.083f, -8f);
            this.SetLeftEye(1, 0.06f, 0.06f, 0.083f, -8f);
        }
        else if (expressionId == 9)
        {
            this.SetRightEyebrow(0.4f, 0.53f, 0.2f, 85f);
            this.SetLeftEyebrow(-0.4f, 0.52f, 0.2f, 95f);

            this.SetRightEye(0, 0f, -0.03f, 0.083f, 0f);
            this.SetLeftEye(0, 0f, -0.03f, 0.083f, 0f);
        }
        else if (expressionId == 10)
        {
            this.SetRightEyebrow(0.4f, 0.53f, 0.2f, 105f);
            this.SetLeftEyebrow(-0.4f, 0.53f, 0.2f, 75f);

            this.SetRightEye(1, -0.06f, 0.06f, 0.083f, -8f);
            this.SetLeftEye(1, -0.06f, 0.06f, 0.083f, -8f);
        }
        else if (expressionId == 11)
        {
            this.SetRightEyebrow(0.4f, 0.58f, 0.2f, 85f);
            this.SetLeftEyebrow(-0.4f, 0.53f, 0.2f, 80f);

            this.SetRightEye(0, 0f, 0f, 0.083f, 0f);
            this.SetLeftEye(0, 0f, 0f, 0.083f, 0f);
        }
        else if (expressionId == 12) //nervous
        {
            this.SetRightEyebrow(0.4f, 0.50f, 0.2f, 90f);
            this.SetLeftEyebrow(-0.4f, 0.50f, 0.2f, 90f);

            this.SetRightEye(0, 0.05f, 0f, 0.083f, 0f);
            this.SetLeftEye(0, 0.05f, 0f, 0.083f, 0f);
        }
        else
        {
            this.SetDefaultExpression();
        }

        this.SetFlameEyes(flameEyes);
        this.SetBlush(blush);
    }

    public void SetDefaultExpression()
    {
        if (this.evil)
            this.SetRightEyebrow(0.4f, 0.53f, 0.2f, 100f);
        else
            this.SetRightEyebrow(0.4f, 0.58f, 0.2f, 85f);

        //this.SetRightEyebrow(0.4f, 0.58f, 0.2f, 85f);
        this.SetLeftEyebrow(-0.4f, 0.53f, 0.2f, 80f);

        this.SetRightEye(0, 0f, 0f, 0.083f, 0f);
        this.SetLeftEye(0, 0f, 0f, 0.083f, 0f);

        this.SetFlameEyes(false);
        this.SetBlush(false);

    }


    private IEnumerator ShowText(string fullText)
    {
        this.currentText = "";
        this.dialougeText.text = "";
        yield return new WaitForSeconds(0.05f);
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
                if (fullText.Substring(i, 1) != "§" && fullText.Substring(i, 1) != "½")
                {
                    this.currentText = this.currentText + fullText.Substring(i, 1);
                }
                else
                {
                    if (fullText.Substring(i, 1) == "§")
                        extraWaitTime = 0.4f;
                    else
                        extraWaitTime = 0.05f;
                }

                this.dialougeText.text = this.currentText;
                yield return new WaitForSeconds(0.05f + extraWaitTime);
            }


        }
    }
    private IEnumerator SpinCog(float duration = 0.5f, int rounds = 1)
    {
        float currentTime = 0;

        float targetRotation = 12 + (45 * rounds);

        /*float targetRotation = 57;
        if (!right)
            targetRotation = -33f;*/

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.cog != null)
                this.cog.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(12f, targetRotation, currentTime / duration));
            yield return null;
        }

        if (this.cog != null)
            this.cog.transform.localEulerAngles = new Vector3(0f, 0f, 12f);
    }

    public void SetRightEye(int eyeId = 0, float xPos = 0f, float yPos = 0f, float zPos = 0.083f, float xRot = 0f/*, bool flameEyes = false*/)
    {
        if (this.rightPupil != null)
        {
            this.rightPupil.transform.localPosition = new Vector3(xPos, yPos, zPos);
            this.rightPupil.transform.localEulerAngles = new Vector3(xRot, 0f, 0f);
        }


        if (this.rightEyeHalfClosed != null)
            this.rightEyeHalfClosed.SetActive(eyeId == 0);

        if (this.rightEyeFullyOpened != null)
            this.rightEyeFullyOpened.SetActive(eyeId == 1);

        //this.SetFlameEyes(flameEyes);


        /*if (eyeId == 1)
        {
            if (this.rightEyeFullyOpened != null)
                this.rightEyeFullyOpened.SetActive(true);

            if (this.rightEyeHalfClosed != null)
                this.rightEyeHalfClosed.SetActive(false);
        }*/
    }

    public void SetRightEyebrow(float xPos = 0.4f, float yPos = 0.58f, float zPos = 0.2f, float zRot = 85f)
    {
        if (this.rightEyebrow != null)
        {
            this.rightEyebrow.transform.localPosition = new Vector3(xPos, yPos, zPos);
            this.rightEyebrow.transform.localEulerAngles = new Vector3(0f, 0f, zRot);
        }
    }

    public void SetLeftEye(int eyeId = 0, float xPos = 0f, float yPos = 0f, float zPos = 0.083f, float xRot = 0f/*, bool flameEyes = false*/)
    {
        if(this.leftPupil != null)
        {
            this.leftPupil.transform.localPosition = new Vector3(xPos, yPos, zPos);
            this.leftPupil.transform.localEulerAngles = new Vector3(xRot, 0f, 0f);
        }


        if (this.leftEyeHalfClosed != null)
            this.leftEyeHalfClosed.SetActive(eyeId == 0);

        if (this.leftEyeFullyOpened != null)
            this.leftEyeFullyOpened.SetActive(eyeId == 1);

        //this.SetFlameEyes(flameEyes);
    }

    public void SetLeftEyebrow(float xPos = -0.4f, float yPos = 0.53f, float zPos = 0.2f, float zRot = 80f)
    {
        if (this.leftEyebrow != null)
        {
            this.leftEyebrow.transform.localPosition = new Vector3(xPos, yPos, zPos);
            this.leftEyebrow.transform.localEulerAngles = new Vector3(0f, 0f, zRot);
        }
    }

    public void OpenEyes()
    {
        if (this.rightEyeOpen != null && this.rightEyeClosed && this.leftEyeOpen != null && this.leftEyeClosed != null)
        {
            this.rightEyeOpen.SetActive(true);
            this.leftEyeOpen.SetActive(true);

            this.rightEyeClosed.SetActive(false);
            this.leftEyeClosed.SetActive(false);
        }
    }

    public void SetFlameEyes(bool enable)
    {
        foreach(GameObject flame in this.flameEyes)
        {
            if (flame != null)
                flame.SetActive(enable);
        }
    }

    public void SetBlush(bool enable)
    {
        foreach (GameObject blush in this.blushes)
        {
            if (blush != null)
                blush.SetActive(enable);
        }
    }

    public void MoveDialougeBox(bool down = false)
    {
        if (down)
        {
            this.textBubble.transform.localPosition = new Vector3(0.7f, 0.45f, -0.1f);

            foreach (GameObject triangles in this.textBoxTriangles)
                triangles.SetActive(false);

            foreach (GameObject trianglesDisabled in this.textBoxTrianglesDisabled)
                trianglesDisabled.SetActive(true);
        }
        else
        {
            this.textBubble.transform.localPosition = new Vector3(0.3f, 1.6f, -0.1f);

            foreach (GameObject triangles in this.textBoxTriangles)
                triangles.SetActive(true);

            foreach (GameObject trianglesDisabled in this.textBoxTrianglesDisabled)
                trianglesDisabled.SetActive(false);
        }
    }
}
