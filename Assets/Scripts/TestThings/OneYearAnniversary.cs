using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OneYearAnniversary : MonoBehaviour
{
    public GameObject geary;

    public GameObject textBubble;
    //public Text dialougeText;
    public TMP_Text dialougeText;

    private string currentText = "";

    public Button playMvcButton;
    public Button getMvcButton;

    public Button playButton;
    public Button buyButton;

    public GameObject eventSystem;

    [Space]
    public GameObject handShake;
    public GameObject ryu;
    public GameObject cyclops;
    public Material handShakeSkybox1;
    public Material handShakeSkybox2;

    public AudioSource handShakeSfx;

    public RotationToOtherRotation[] rotationToOtherRotations;

    [Space]
    public GameObject rightEyeOpen;
    public GameObject leftEyeOpen;

    public GameObject rightEyeClosed;
    public GameObject leftEyeClosed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        this.ClearText();
        this.textBubble.gameObject.SetActive(false);
        this.StartCoroutine(this.Blink());
        this.StartCoroutine(this.GearyStartCoroutine());
    }

    private IEnumerator GearyStartCoroutine()
    {
        float currentTime = 0;
        float duration = 0.4f;
        float startPos = 4.5f;
        //float startPos = -2.5f;

        //this.StartCoroutine(this.SpinCog(0.3f, 4));
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.geary.transform.localPosition = new Vector3(Mathf.Lerp(startPos, 0.1f, currentTime / duration), 2.157f, -0.3f);
            //this.model.transform.localPosition = new Vector3(0f, Mathf.Lerp(startPos, 0f, currentTime / duration), 0f);
            yield return null;
        }
        this.geary.transform.localPosition = new Vector3(0.1f, 2.157f, -0.3f);

        yield return new WaitForSeconds(0.3f);

        //this.Handshake();

        this.textBubble.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        string textString = "Hello there!½ Today is the one year anniversary of the first ever build of Fight Game!§ (If you are playing this in 12th of September)";
        //string textString = "Hello!½ Today is the 1 year anniversary of the first ever build of Fightgame!§ If you are playing this in 12th of september. (Close one!)";
        this.StartCoroutine(this.ShowText(textString));

        yield return new WaitForSeconds(3.4f + (textString.Length * 0.05f));

        textString = "How fast time flies...";
        //string textString = "Hello!½ Today is the 1 year anniversary of the first ever build of Fightgame!§ If you are playing this in 12th of september. (Close one!)";
        this.StartCoroutine(this.ShowText(textString));

        yield return new WaitForSeconds(2.1f + (textString.Length * 0.05f));

        textString = "But today is also the release date of Marvel vs. Capcom Fighting Collection!";
        this.StartCoroutine(this.ShowText(textString));

        yield return new WaitForSeconds(3f + (textString.Length * 0.05f));

        textString = "So what are you doing here?½ Go play that game instead!";
        this.StartCoroutine(this.ShowText(textString));

        yield return new WaitForSeconds(2.1f + (textString.Length * 0.05f));






        //this.ClearText();

        if (this.playMvcButton != null)
            this.playMvcButton.gameObject.SetActive(true);

        if (this.getMvcButton != null)
            this.getMvcButton.gameObject.SetActive(true);

        this.SelectPlayMvcButton();


    }

    public void ClearText()
    {
        this.currentText = "";
        this.dialougeText.text = "";
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

    public void SelectPlayMvcButton()
    {
        if (this.playMvcButton != null)
            this.playMvcButton.Select();
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

        if (this.rightEyeOpen != null && this.rightEyeClosed && this.leftEyeOpen != null && this.leftEyeClosed != null)
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

    public void Handshake(bool giveUrl = false)
    {
        if (this.handShakeSkybox1 != null)
            RenderSettings.skybox = this.handShakeSkybox1;

        if (this.geary != null)
            this.geary.SetActive(false);

        if (this.playMvcButton != null)
            this.playMvcButton.gameObject.SetActive(false);

        if (this.getMvcButton != null)
            this.getMvcButton.gameObject.SetActive(false);

        if (this.handShake != null)
            this.handShake.SetActive(true);

        this.StartCoroutine(this.HandshakeCoroutine(giveUrl));
    }

    private IEnumerator HandshakeCoroutine(bool giveUrl = false)
    {
        /*float currentTime = 0;
        float duration = 0.2f;

        float startPosRyu = -4f;
        float endPosRyu = -2f;

        float startPosCyclops = 4f;
        float endPosCyclops = 2f;*/


        float currentTime = 0;
        float duration = 0.4f;

        float startPosRyu = -2f;
        float endPosRyu = -1.1f;

        float startPosCyclops = 2f;
        float endPosCyclops = 1.1f;
        //float startPos = -2.5f;

        //this.StartCoroutine(this.SpinCog(0.3f, 4));
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            if (this.ryu != null)
                this.ryu.transform.localPosition = new Vector3(0f, 0f, Mathf.Lerp(startPosRyu, endPosRyu, currentTime / duration));

            if (this.cyclops != null)
                this.cyclops.transform.localPosition = new Vector3(0f, 0f, Mathf.Lerp(startPosCyclops, endPosCyclops, currentTime / duration));

            //this.geary.transform.localPosition = new Vector3(0f, 0f, Mathf.Lerp(startPos, 0.1f, currentTime / duration));
            //this.model.transform.localPosition = new Vector3(0f, Mathf.Lerp(startPos, 0f, currentTime / duration), 0f);
            yield return null;
        }

        /*currentTime = 0;
        duration = 0.4f;

        startPosRyu = -2f;
        endPosRyu = -1.1f;

        startPosCyclops = 2f;
        endPosCyclops = 1.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            if (this.ryu != null)
                this.ryu.transform.localPosition = new Vector3(0f, 0f, Mathf.Lerp(startPosRyu, endPosRyu, currentTime / duration));

            if (this.cyclops != null)
                this.cyclops.transform.localPosition = new Vector3(0f, 0f, Mathf.Lerp(startPosCyclops, endPosCyclops, currentTime / duration));

            //this.geary.transform.localPosition = new Vector3(0f, 0f, Mathf.Lerp(startPos, 0.1f, currentTime / duration));
            //this.model.transform.localPosition = new Vector3(0f, Mathf.Lerp(startPos, 0f, currentTime / duration), 0f);
            yield return null;
        }*/

        if (this.handShakeSfx != null)
            this.handShakeSfx.Play();

        //yield return new WaitForSeconds(0.2f);

        if (this.handShakeSkybox1 != null)
            RenderSettings.skybox = this.handShakeSkybox2;

        /*foreach (RotationToOtherRotation rotationToOtherRotation in this.rotationToOtherRotations)
        {
            if (rotationToOtherRotation != null)
                rotationToOtherRotation.enabled = true;
        }*/


        yield return new WaitForSeconds(3f);

        if (this.eventSystem != null)
            this.eventSystem.SetActive(true);

        if (giveUrl)
        {

            if (this.buyButton != null)
            {
                this.buyButton.gameObject.SetActive(true);
                this.buyButton.Select();
            }
                

            /*Application.OpenURL("https://store.steampowered.com/app/2634890/MARVEL_vs_CAPCOM_Fighting_Collection_Arcade_Classics/");
            Application.Quit();
            Debug.Log("SHUT DOWN!");*/
        }
        else
        {
            if (this.playButton != null)
            {
                this.playButton.gameObject.SetActive(true);
                this.playButton.Select();
            }

            /*Application.Quit();
            Debug.Log("SHUT DOWN!");*/
        }
    }

    public void GetMvcCollection()
    {
        /*Application.OpenURL("https://store.steampowered.com/app/2634890/MARVEL_vs_CAPCOM_Fighting_Collection_Arcade_Classics/");
        Application.Quit();*/

        if (this.eventSystem != null)
            this.eventSystem.SetActive(false);
        this.StartCoroutine(this.GetMvcCollectionCoroutine());
    }

    public void PlayMvcCollection()
    {
        //Application.Quit();

        if (this.eventSystem != null)
            this.eventSystem.SetActive(false);
        this.StartCoroutine(this.PlayMvcCollectionCoroutine());
    }

    public void QuitAndPlay()
    {
        if (this.eventSystem != null)
            this.eventSystem.SetActive(false);

        this.StartCoroutine(this.QuitAndPlayCoroutine());

        /*Application.Quit();
        Debug.Log("SHUT DOWN!");*/
    }

    public void QuitAndBuy()
    {
        if (this.eventSystem != null)
            this.eventSystem.SetActive(false);

        this.StartCoroutine(this.QuitAndBuyCoroutine());

        /*Application.OpenURL("https://store.steampowered.com/app/2634890/MARVEL_vs_CAPCOM_Fighting_Collection_Arcade_Classics/");
        Application.Quit();
        Debug.Log("SHUT DOWN!");*/
    }


    private IEnumerator QuitAndBuyCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        Application.OpenURL("https://store.steampowered.com/app/2634890/MARVEL_vs_CAPCOM_Fighting_Collection_Arcade_Classics/");
        Application.Quit();
        Debug.Log("SHUT DOWN!");
    }

    private IEnumerator QuitAndPlayCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        Application.Quit();
        Debug.Log("SHUT DOWN!");
    }

    private IEnumerator GetMvcCollectionCoroutine()
    {
        string textString = "Then buy it!";
        this.StartCoroutine(this.ShowText(textString));

        yield return new WaitForSeconds(2f + (textString.Length * 0.05f));

        //yield return new WaitForSeconds(0.2f);

        this.Handshake(true);

        /*Application.OpenURL("https://store.steampowered.com/app/2634890/MARVEL_vs_CAPCOM_Fighting_Collection_Arcade_Classics/");
        Application.Quit();*/
    }

    private IEnumerator PlayMvcCollectionCoroutine()
    {
        string textString = "Have fun!";
        this.StartCoroutine(this.ShowText(textString));

        yield return new WaitForSeconds(2f + (textString.Length * 0.05f));

        //yield return new WaitForSeconds(0.2f);

        this.Handshake();

        //Application.Quit();
    }
}
